using Microsoft.AspNetCore.Mvc;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Services;
using QuranGuide.Maui.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuranGuide.Maui.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AudioController : ControllerBase
    {
        private readonly IAudioService _audioService;
        private readonly ILogger<AudioController> _logger;

        public AudioController(IAudioService audioService, ILogger<AudioController> logger)
        {
            _audioService = audioService;
            _logger = logger;
        }

        [HttpGet("editions")]
        public async Task<ActionResult<IEnumerable<AudioEdition>>> GetAudioEditions()
        {
            try
            {
                var editions = await _audioService.GetAudioEditionsAsync();
                return Ok(new ApiResponse<IEnumerable<AudioEdition>>
                {
                    Code = 200,
                    Status = "OK",
                    Data = editions
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching audio editions");
                return StatusCode(500, new ApiResponse<object>
                {
                    Code = 500,
                    Status = "Internal Server Error",
                    Error = "Failed to fetch audio editions"
                });
            }
        }

        [HttpGet("surah/{edition}/{surahNumber}")]
        public async Task<ActionResult<AudioTrack>> GetSurahAudio(string edition, int surahNumber)
        {
            try
            {
                var audio = await _audioService.GetSurahAudioAsync(edition, surahNumber);
                if (audio == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Code = 404,
                        Status = "Not Found",
                        Error = "Audio not found"
                    });
                }

                return Ok(new ApiResponse<AudioTrack>
                {
                    Code = 200,
                    Status = "OK",
                    Data = audio
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching surah audio for {Edition} surah {Number}", edition, surahNumber);
                return StatusCode(500, new ApiResponse<object>
                {
                    Code = 500,
                    Status = "Internal Server Error",
                    Error = "Failed to fetch audio"
                });
            }
        }
    }
}
