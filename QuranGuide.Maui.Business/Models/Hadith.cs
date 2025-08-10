using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Models
{
    public class Hadith
    {
        public int Id { get; private set; }
        public string HadithNumber { get; private set; }
        public string EnglishNarrator { get; private set; }
        public string HadithEnglish { get; private set; }
        public string HadithArabic { get; private set; }
        public string HeadingEnglish { get; private set; }
        public string HeadingArabic { get; private set; }
        public string ChapterId { get; private set; }
        public string BookSlug { get; private set; }
        public string Volume { get; private set; }
        public string Status { get; private set; }

        // Domain methods
        public bool IsSahih => Status.Equals("sahih", StringComparison.OrdinalIgnoreCase);
        public string GetFullReference() => $"{BookSlug} - {HadithNumber}";
    }
}
