using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuranGuide.Maui.Core.Services;

namespace QuranGuide.Maui.Infrastructure.ExternalServices
{
    public class ScrapeIngestionService : IScrapeIngestionService
    {
        private readonly IScriptScrapingService _scraper;
        private readonly QuranDbContext _db;
        private readonly ILogger<ScrapeIngestionService> _logger;

        public ScrapeIngestionService(IScriptScrapingService scraper, QuranDbContext db, ILogger<ScrapeIngestionService> logger)
        {
            _scraper = scraper;
            _db = db;
            _logger = logger;
        }

        public async Task<object> SaveAllAsync(string edition, CancellationToken cancellationToken = default)
        {
            // Editions
            _logger.LogInformation("Ingestion: fetching editions");
            var editions = await _scraper.GetEditionsAsync(cancellationToken);
            foreach (var e in editions)
            {
                var exists = await _db.Editions.AnyAsync(x => x.Identifier == e.Identifier, cancellationToken);
                if (!exists)
                {
                    _logger.LogDebug("Adding edition {Identifier}", e.Identifier);
                    _db.Editions.Add(e);
                }
            }
            await _db.SaveChangesAsync(cancellationToken);

            // Surahs basic list
            _logger.LogInformation("Ingestion: fetching surahs");
            var surahs = await _scraper.GetSurahsAsync(cancellationToken);
            foreach (var s in surahs)
            {
                var existing = await _db.Surahs.FirstOrDefaultAsync(x => x.Number == s.Number, cancellationToken);
                if (existing == null)
                {
                    _logger.LogDebug("Adding surah {Number} {Name}", s.Number, s.EnglishName);
                    _db.Surahs.Add(s);
                }
            }
            await _db.SaveChangesAsync(cancellationToken);

            // Quran by edition
            _logger.LogInformation("Ingestion: fetching quran by edition {Edition}", edition);
            var quran = await _scraper.GetQuranByEditionAsync(edition, cancellationToken);
            var editionEntity = await _db.Editions.FirstOrDefaultAsync(x => x.Identifier == edition, cancellationToken);
            var insertedAyahs = 0;
            if (quran?.Surahs != null && editionEntity != null)
            {
                foreach (var surahWithAyahs in quran.Surahs)
                {
                    var surah = await _db.Surahs.FirstOrDefaultAsync(x => x.Number == surahWithAyahs.Number, cancellationToken);
                    if (surah == null) continue;

                    foreach (var a in surahWithAyahs.Ayahs)
                    {
                        var exists = await _db.Ayahs.AnyAsync(x => x.SurahId == surah.Id && x.NumberInSurah == a.NumberInSurah && x.EditionId == editionEntity.Id, cancellationToken);
                        if (!exists)
                        {
                            _db.Ayahs.Add(new Core.Models.Ayah
                            {
                                Number = a.Number,
                                Text = a.Text,
                                NumberInSurah = a.NumberInSurah,
                                Juz = a.Juz,
                                Manzil = a.Manzil,
                                Page = a.Page,
                                Ruku = a.Ruku,
                                HizbQuarter = a.HizbQuarter,
                                SajdaType = a.SajdaType,
                                SajdaNumber = a.SajdaNumber,
                                SurahId = surah.Id,
                                EditionId = editionEntity.Id
                            });
                            insertedAyahs++;
                        }
                    }
                }
                await _db.SaveChangesAsync(cancellationToken);
            }

            _logger.LogInformation("Ingestion completed: {Editions} editions, {Surahs} surahs, {Ayahs} ayahs inserted/ensured", editions.Count(), surahs.Count(), insertedAyahs);
            return new { Editions = editions.Count(), Surahs = surahs.Count(), InsertedAyahs = insertedAyahs };
        }
    }
}


