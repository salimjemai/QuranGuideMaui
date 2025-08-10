using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuranGuide.Maui.Core.DataTransferObjects;

namespace QuranGuide.Maui.Core.Services
{
    public interface IAudioService
    {
        Task<IEnumerable<AudioEdition>> GetAudioEditionsAsync();
        Task<AudioTrack> GetAyahAudioAsync(string edition, int ayahNumber);
        Task<AudioTrack> GetSurahAudioAsync(string edition, int surahNumber);
        Task<bool> DownloadAudioAsync(string edition, int surahNumber, string filePath);
        Task<bool> IsAudioDownloadedAsync(string edition, int surahNumber);
    }
}
