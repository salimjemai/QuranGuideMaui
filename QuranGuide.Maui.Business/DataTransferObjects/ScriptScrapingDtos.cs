using System.Collections.Generic;

namespace QuranGuide.Maui.Core.DataTransferObjects
{
    public class QuranEditionData
    {
        public IEnumerable<SurahWithAyahs> Surahs { get; set; }
    }

    public class SurahEditionData
    {
        public string Edition { get; set; }
        public IEnumerable<AyahDto> Ayahs { get; set; }
    }

    public class SajdaItem
    {
        public int Number { get; set; }
        public SurahDto Surah { get; set; }
    }

    public class AyahContainer
    {
        public IEnumerable<AyahDto> Ayahs { get; set; }
    }
}


