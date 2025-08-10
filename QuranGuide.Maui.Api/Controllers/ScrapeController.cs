using Microsoft.AspNetCore.Mvc;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Services;
using QuranGuide.Maui.Shared;
using System.Linq;

namespace QuranGuide.Maui.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScrapeController : ControllerBase
    {
        private readonly IScriptScrapingService _scraper;
        private readonly IScrapeIngestionService _ingestion;
        private readonly ILogger<ScrapeController> _logger;

        public ScrapeController(IScriptScrapingService scraper, IScrapeIngestionService ingestion, ILogger<ScrapeController> logger)
        {
            _scraper = scraper;
            _ingestion = ingestion;
            _logger = logger;
        }

        [HttpPost("seed")]
        public async Task<ActionResult<ApiResponse<object>>> Seed([FromQuery] string edition = "quran-uthmani", [FromQuery] int sampleNumber = 1)
        {
            try
            {
                var editions = await _scraper.GetEditionsAsync();
                var languages = await _scraper.GetLanguagesAsync();
                var types = await _scraper.GetTypesAsync();
                var formats = await _scraper.GetFormatsAsync();
                var surahs = await _scraper.GetSurahsAsync();
                var quran = await _scraper.GetQuranByEditionAsync(edition);

                var juz = await _scraper.GetJuzAyahsAsync(sampleNumber, edition);
                var manzil = await _scraper.GetManzilAyahsAsync(sampleNumber, edition);
                var ruku = await _scraper.GetRukuAyahsAsync(sampleNumber, edition);
                var page = await _scraper.GetPageAyahsAsync(sampleNumber, edition);
                var hizb = await _scraper.GetHizbQuarterAyahsAsync(sampleNumber, edition);
                var sajdas = await _scraper.GetSajdasAsync(edition);

                var summary = new
                {
                    Edition = edition,
                    Counts = new
                    {
                        Editions = editions.Count(),
                        Languages = languages.Count(),
                        Types = types.Count(),
                        Formats = formats.Count(),
                        Surahs = surahs.Count(),
                        QuranSurahCount = quran?.Surahs?.Count() ?? 0,
                        JuzAyahs = juz.Count(),
                        ManzilAyahs = manzil.Count(),
                        RukuAyahs = ruku.Count(),
                        PageAyahs = page.Count(),
                        HizbAyahs = hizb.Count(),
                        Sajdas = sajdas.Count()
                    }
                };

                return Ok(new ApiResponse<object>
                {
                    Code = 200,
                    Status = "OK",
                    Data = summary
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running scraping seed for edition {Edition}", edition);
                return StatusCode(500, new ApiResponse<object>
                {
                    Code = 500,
                    Status = "Internal Server Error",
                    Error = "Failed to run scraping seed"
                });
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult<ApiResponse<object>>> Save([FromQuery] string edition = "quran-uthmani")
        {
            try
            {
                var result = await _ingestion.SaveAllAsync(edition);
                return Ok(new ApiResponse<object> { Code = 200, Status = "OK", Data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving scraped data for edition {Edition}", edition);
                return StatusCode(500, new ApiResponse<object> { Code = 500, Status = "Internal Server Error", Error = "Failed to save data" });
            }
        }

        [HttpGet("quran/{edition}")]
        public async Task<ActionResult<ApiResponse<QuranEditionData?>>> GetQuran(string edition)
        {
            var data = await _scraper.GetQuranByEditionAsync(edition);
            return Ok(new ApiResponse<QuranEditionData?> { Code = 200, Status = "OK", Data = data });
        }

        [HttpGet("surah/{surahNumber}/editions")]
        public async Task<ActionResult<ApiResponse<IEnumerable<SurahEditionData>>>> GetSurahEditions(int surahNumber, [FromQuery] string editions)
        {
            var list = (editions ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var data = await _scraper.GetSurahEditionsAsync(surahNumber, list);
            return Ok(new ApiResponse<IEnumerable<SurahEditionData>> { Code = 200, Status = "OK", Data = data });
        }

        [HttpGet("juz/{number}/{edition}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AyahDto>>>> GetJuz(int number, string edition)
        {
            var data = await _scraper.GetJuzAyahsAsync(number, edition);
            return Ok(new ApiResponse<IEnumerable<AyahDto>> { Code = 200, Status = "OK", Data = data });
        }

        [HttpGet("manzil/{number}/{edition}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AyahDto>>>> GetManzil(int number, string edition)
        {
            var data = await _scraper.GetManzilAyahsAsync(number, edition);
            return Ok(new ApiResponse<IEnumerable<AyahDto>> { Code = 200, Status = "OK", Data = data });
        }

        [HttpGet("ruku/{number}/{edition}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AyahDto>>>> GetRuku(int number, string edition)
        {
            var data = await _scraper.GetRukuAyahsAsync(number, edition);
            return Ok(new ApiResponse<IEnumerable<AyahDto>> { Code = 200, Status = "OK", Data = data });
        }

        [HttpGet("page/{number}/{edition}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AyahDto>>>> GetPage(int number, string edition)
        {
            var data = await _scraper.GetPageAyahsAsync(number, edition);
            return Ok(new ApiResponse<IEnumerable<AyahDto>> { Code = 200, Status = "OK", Data = data });
        }

        [HttpGet("hizbQuarter/{number}/{edition}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AyahDto>>>> GetHizb(int number, string edition)
        {
            var data = await _scraper.GetHizbQuarterAyahsAsync(number, edition);
            return Ok(new ApiResponse<IEnumerable<AyahDto>> { Code = 200, Status = "OK", Data = data });
        }

        [HttpGet("sajda/{edition}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<SajdaItem>>>> GetSajdas(string edition)
        {
            var data = await _scraper.GetSajdasAsync(edition);
            return Ok(new ApiResponse<IEnumerable<SajdaItem>> { Code = 200, Status = "OK", Data = data });
        }
    }
}


