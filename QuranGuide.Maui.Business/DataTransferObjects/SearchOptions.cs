using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.DataTransferObjects
{
    public class SearchOptions
    {
        public string Keyword { get; set; }
        public string Surah { get; set; }
        public string Edition { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public int? Limit { get; set; }
    }
}
