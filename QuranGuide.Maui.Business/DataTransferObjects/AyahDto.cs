using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.DataTransferObjects
{
    public class AyahDto
    {
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
        public SurahDto Surah { get; set; }
    }
}
