using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Services;
using QuranGuide.Maui.Shared;

namespace QuranGuide.Maui.Infrastructure.ExternalServices
{
    public class QuranSearchService : ISearchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public QuranSearchService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["QuranApi:BaseUrl"] ?? "http://localhost:3001/api";
        }

        public async Task<IEnumerable<SearchResult>> SearchAyahsAsync(string keyword, string surah = "all", string edition = "en")
        {
            var endpoint = $"{_baseUrl}/search?keyword={Uri.EscapeDataString(keyword)}&surah={Uri.EscapeDataString(surah)}&edition={Uri.EscapeDataString(edition)}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<SearchResult[]>>();
            return apiResponse?.Data ?? Enumerable.Empty<SearchResult>();
        }

        public Task<IEnumerable<SearchResult>> SearchAdvancedAsync(SearchOptions options)
        {
            // Stub for now
            return Task.FromResult(Enumerable.Empty<SearchResult>());
        }

        public Task<IEnumerable<SearchResult>> GetSearchHistoryAsync()
        {
            // Stub for now
            return Task.FromResult(Enumerable.Empty<SearchResult>());
        }

        public Task SaveSearchQueryAsync(string query)
        {
            // Stub for now
            return Task.CompletedTask;
        }
    }
}


