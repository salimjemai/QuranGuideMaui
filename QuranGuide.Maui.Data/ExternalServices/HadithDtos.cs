using System.Collections.Generic;
using System.Text.Json.Serialization;
using QuranGuide.Maui.Core.Models;

namespace QuranGuide.Maui.Infrastructure.ExternalServices
{
    public class BooksResponse
    {
        [JsonPropertyName("books")] public List<HadithBook> Books { get; set; }
    }

    public class ChaptersResponse
    {
        [JsonPropertyName("chapters")] public List<HadithChapter> Chapters { get; set; }
    }

    public class HadithsResponse
    {
        [JsonPropertyName("hadiths")] public List<Hadith> Hadiths { get; set; }
    }

    public class HadithResponse
    {
        [JsonPropertyName("hadith")] public Hadith Hadith { get; set; }
    }
}


