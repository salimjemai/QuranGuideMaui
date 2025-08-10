using Microsoft.AspNetCore.Mvc;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Models;
using QuranGuide.Maui.Core.Services;
using QuranGuide.Maui.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuranGuide.Maui.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuranController : ControllerBase
    {
        private readonly IQuranService _quranService;
        private readonly ILogger<QuranController> _logger;
        private readonly ISearchService _searchService;

        public QuranController(IQuranService quranService, ISearchService searchService, ILogger<QuranController> logger)
        {
            _quranService = quranService;
            _searchService = searchService;
            _logger = logger;
        }

        [HttpGet("surah")]
        public async Task<ActionResult<IEnumerable<Surah>>> GetAllSurahs()
        {
            try
            {
                var surahs = await _quranService.GetAllSurahsAsync();
                return Ok(new ApiResponse<IEnumerable<Surah>>
                {
                    Code = 200,
                    Status = "OK",
                    Data = surahs
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all surahs");
                return StatusCode(500, new ApiResponse<object>
                {
                    Code = 500,
                    Status = "Internal Server Error",
                    Error = "Failed to fetch surahs"
                });
            }
        }

        [HttpGet("surah/{number}")]
        public async Task<ActionResult<SurahWithAyahs>> GetSurah(int number, [FromQuery] string edition = "quran-uthmani")
        {
            try
            {
                var surah = await _quranService.GetSurahWithAyahsAsync(number, edition);
                if (surah == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Code = 404,
                        Status = "Not Found",
                        Error = "Surah not found"
                    });
                }

                return Ok(new ApiResponse<SurahWithAyahs>
                {
                    Code = 200,
                    Status = "OK",
                    Data = surah
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching surah {Number}", number);
                return StatusCode(500, new ApiResponse<object>
                {
                    Code = 500,
                    Status = "Internal Server Error",
                    Error = "Failed to fetch surah"
                });
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SearchResult>>> Search(
            [FromQuery] string keyword,
            [FromQuery] string surah = "all",
            [FromQuery] string edition = "en")
        {
            try
            {
                var results = await _searchService.SearchAyahsAsync(keyword, surah, edition);
                return Ok(new ApiResponse<IEnumerable<SearchResult>>
                {
                    Code = 200,
                    Status = "OK",
                    Data = results
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching for keyword: {Keyword}", keyword);
                return StatusCode(500, new ApiResponse<object>
                {
                    Code = 500,
                    Status = "Internal Server Error",
                    Error = "Failed to perform search"
                });
            }
        }
    }
}
