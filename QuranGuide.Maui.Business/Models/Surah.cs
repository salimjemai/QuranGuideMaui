using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Models
{
    public class Surah
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string EnglishNameTranslation { get; set; }
        public string RevelationType { get; set; }
        public int NumberOfAyahs { get; set; }
        public int Juz { get; set; }
        public int HizbQuarter { get; set; }
        public string SajdaType { get; set; }
        public int SajdaNumber { get; set; }
        public int Ruku { get; set; }
        public int Page { get; set; }
        public int Manzil { get; set; }

        // Domain methods
        public bool IsMeccan => RevelationType.Equals("Meccan", StringComparison.OrdinalIgnoreCase);
        public bool IsMedinan => RevelationType.Equals("Medinan", StringComparison.OrdinalIgnoreCase);
        public bool HasSajda => !string.IsNullOrEmpty(SajdaType);
    }
}
