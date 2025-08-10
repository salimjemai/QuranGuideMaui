using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Models;

namespace QuranGuide.Maui.Core.Services
{
    public interface IScriptScrapingService
    {
        Task<IEnumerable<Edition>> GetEditionsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<LanguageDto>> GetLanguagesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> GetTypesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> GetFormatsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Surah>> GetSurahsAsync(CancellationToken cancellationToken = default);
        Task<object?> GetMetaAsync(CancellationToken cancellationToken = default);

        Task<QuranEditionData?> GetQuranByEditionAsync(string edition, CancellationToken cancellationToken = default);
        Task<IEnumerable<SurahEditionData>> GetSurahEditionsAsync(int surahNumber, IEnumerable<string> editions, CancellationToken cancellationToken = default);

        Task<IEnumerable<AyahDto>> GetJuzAyahsAsync(int juzNumber, string edition, CancellationToken cancellationToken = default);
        Task<IEnumerable<AyahDto>> GetManzilAyahsAsync(int manzilNumber, string edition, CancellationToken cancellationToken = default);
        Task<IEnumerable<AyahDto>> GetRukuAyahsAsync(int rukuNumber, string edition, CancellationToken cancellationToken = default);
        Task<IEnumerable<AyahDto>> GetPageAyahsAsync(int pageNumber, string edition, CancellationToken cancellationToken = default);
        Task<IEnumerable<AyahDto>> GetHizbQuarterAyahsAsync(int hizbNumber, string edition, CancellationToken cancellationToken = default);
        Task<IEnumerable<SajdaItem>> GetSajdasAsync(string edition, CancellationToken cancellationToken = default);
    }
}


