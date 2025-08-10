using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.DataTransferObjects
{
    public class AudioTrack
    {
        public int AyahNumber { get; set; }
        public int SurahNumber { get; set; }
        public string EditionIdentifier { get; set; }
        public string AudioUrl { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
