using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Business
{
    public class AudioBusinessService
    {
        private readonly IAudioService _audioService;
        private readonly IOfflineService _offlineService;

        public AudioBusinessService(IAudioService audioService, IOfflineService offlineService)
        {
            _audioService = audioService;
            _offlineService = offlineService;
        }

        public async Task<AudioTrack> GetAudioWithOfflineFallbackAsync(string edition, int surahNumber)
        {
            if (await _offlineService.IsOfflineModeEnabledAsync())
            {
                // Check if audio is downloaded
                if (await _offlineService.IsAudioDownloadedAsync(edition, surahNumber))
                {
                    // Return local audio file path
                    return new AudioTrack { /* local file path */ };
                }
                throw new InvalidOperationException("Audio not available offline");
            }

            return await _audioService.GetSurahAudioAsync(edition, surahNumber);
        }
    }
}
