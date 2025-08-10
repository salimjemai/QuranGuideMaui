using Microsoft.AspNetCore.Mvc;
using QuranGuide.Maui.Core.Models;
using QuranGuide.Maui.Core.Services;
using QuranGuide.Maui.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuranGuide.Maui.Api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class HadithController : ControllerBase
    {
        private readonly IHadithService _hadithService;
        private readonly ILogger<HadithController> _logger;

        public HadithController(IHadithService hadithService, ILogger<HadithController> logger)
        {
            _hadithService = hadithService;
            _logger = logger;
        }

        [HttpGet("books")]
        public async Task<ActionResult<IEnumerable<HadithBook>>> GetAllBooks()
        {
            try
            {
                var books = await _hadithService.GetAllBooksAsync();
                return Ok(new ApiResponse<IEnumerable<HadithBook>>
                {
                    Code = 200,
                    Status = "OK",
                    Data = books
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching hadith books");
                return StatusCode(500, new ApiResponse<object>
                {
                    Code = 500,
                    Status = "Internal Server Error",
                    Error = "Failed to fetch hadith books"
                });
            }
        }

        [HttpGet("books/{bookSlug}/chapters")]
        public async Task<ActionResult<IEnumerable<HadithChapter>>> GetChapters(string bookSlug)
        {
            try
            {
                var chapters = await _hadithService.GetChaptersAsync(bookSlug);
                return Ok(new ApiResponse<IEnumerable<HadithChapter>>
                {
                    Code = 200,
                    Status = "OK",
                    Data = chapters
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching chapters for book {BookSlug}", bookSlug);
                return StatusCode(500, new ApiResponse<object>
                {
                    Code = 500,
                    Status = "Internal Server Error",
                    Error = "Failed to fetch chapters"
                });
            }
        }
    }
}
