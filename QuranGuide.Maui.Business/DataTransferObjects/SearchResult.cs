using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.DataTransferObjects
{
    public class SearchResult
    {
        public int Number { get; set; }
        public string Text { get; set; }
        public int NumberInSurah { get; set; }
        public SurahDto Surah { get; set; }
        public string Edition { get; set; }
        public string Language { get; set; }
    }
}
