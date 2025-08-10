using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QuranGuide.Maui.Core.Models;
using QuranGuide.Maui.Core.Services;
using System.Text.Json.Serialization;

namespace QuranGuide.Maui.Infrastructure.ExternalServices
{
    public class HadithService : IHadithService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public HadithService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["HadithApi:ApiKey"];
            _baseUrl = configuration["HadithApi:BaseUrl"] ?? "https://hadithapi.com/api";
        }

        public async Task<IEnumerable<HadithBook>> GetAllBooksAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/books?apiKey={_apiKey}");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<BooksResponse>();
            return apiResponse?.Books ?? Enumerable.Empty<HadithBook>();
        }
        public async Task<IEnumerable<HadithChapter>> GetChaptersAsync(string bookSlug)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/chapters?book={bookSlug}&apiKey={_apiKey}");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<ChaptersResponse>();
            return apiResponse?.Chapters ?? Enumerable.Empty<HadithChapter>();
        }

        public async Task<IEnumerable<Hadith>> GetHadithsAsync(string bookSlug, string chapterId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/hadiths?book={bookSlug}&chapter={chapterId}&apiKey={_apiKey}");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<HadithsResponse>();
            return apiResponse?.Hadiths ?? Enumerable.Empty<Hadith>();
        }

        public async Task<Hadith> GetHadithAsync(string bookSlug, string hadithNumber)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/hadiths/{bookSlug}/{hadithNumber}?apiKey={_apiKey}");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<HadithResponse>();
            return apiResponse?.Hadith!;
        }
    }
}
