using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.DataTransferObjects
{
    public class SurahDto
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string EnglishNameTranslation { get; set; }
        public string RevelationType { get; set; }
        public int NumberOfAyahs { get; set; }
    }
}



