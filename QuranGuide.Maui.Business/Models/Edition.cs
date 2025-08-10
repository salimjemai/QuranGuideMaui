using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Models
{
    public class Edition
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Format { get; set; }
        public string Type { get; set; }
        public string Direction { get; set; }

        // Domain methods
        public bool IsArabic => Language.Equals("ar", StringComparison.OrdinalIgnoreCase);
        public bool IsAudio => Format.Equals("audio", StringComparison.OrdinalIgnoreCase);
        public bool IsText => Format.Equals("text", StringComparison.OrdinalIgnoreCase);
        public bool IsTranslation => Type.Equals("translation", StringComparison.OrdinalIgnoreCase);
    }
}
