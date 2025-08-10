namespace QuranGuide.Maui.Core.Models
{
    public class DownloadedContent
    {
        public string Id { get; set; }
        public string Type { get; set; } // e.g., "surah", "audio"
        public string Identifier { get; set; } // e.g., "surah-1", "audio-ar.abdulbasit-1"
        public string LocalPath { get; set; }
        public long SizeBytes { get; set; }
    }
}



