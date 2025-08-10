using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Models
{
    public class Ayah
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Text { get; set; }
        public int NumberInSurah { get; set; }
        public int Juz { get; set; }
        public int Manzil { get; set; }
        public int Page { get; set; }
        public int Ruku { get; set; }
        public int HizbQuarter { get; set; }
        public string SajdaType { get; set; }
        public int SajdaNumber { get; set; }
        public int SurahId { get; set; }
        public int EditionId { get; set; }

        // Domain methods
        public bool RequiresSajda => !string.IsNullOrEmpty(SajdaType);
        public string GetReference() => $"{SurahId}:{NumberInSurah}";
    }
}
