using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuranGuide.Maui.Core.Models;

namespace QuranGuide.Maui.Core.Services
{
    public interface IOfflineService
    {
        Task<bool> IsOfflineModeEnabledAsync();
        Task EnableOfflineModeAsync();
        Task DisableOfflineModeAsync();
        Task<IEnumerable<DownloadedContent>> GetDownloadedContentAsync();
        Task<bool> DownloadSurahAsync(int surahNumber, string edition);
        Task<bool> DownloadAudioAsync(string edition, int surahNumber);
        Task<bool> IsAudioDownloadedAsync(string edition, int surahNumber);
        Task<bool> RemoveDownloadedContentAsync(string contentId);
    }
}
