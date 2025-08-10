using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.DataTransferObjects
{
    public class UserPreferences
    {
        public string SelectedEdition { get; set; } = "quran-uthmani";
        public string SelectedLanguage { get; set; } = "en";
        public string SelectedType { get; set; } = "translation";
        public string SelectedFormat { get; set; } = "text";
        public bool DarkMode { get; set; } = false;
        public int FontSize { get; set; } = 16;
        public bool AutoPlayAudio { get; set; } = false;
        public bool OfflineMode { get; set; } = false;
    }
}
