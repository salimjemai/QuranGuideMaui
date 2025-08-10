using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using QuranGuide.Maui.Core.Services;

namespace QuranGuide.Maui.Infrastructure.ExternalServices
{
    public class WebScrapingService : IWebScrapingService
    {
        private readonly HttpClient _httpClient;

        public WebScrapingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetStringAsync(string url, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetStringAsync(url, cancellationToken);
        }

        public async Task<T?> GetJsonAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<T>(url, cancellationToken);
        }

        public async Task<TResponse?> PostJsonAsync<TRequest, TResponse>(string url, TRequest payload, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync(url, payload, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<T?>> BulkGetJsonAsync<T>(IEnumerable<string> urls, CancellationToken cancellationToken = default)
        {
            var tasks = urls.Select(u => _httpClient.GetFromJsonAsync<T>(u, cancellationToken));
            var results = await Task.WhenAll(tasks);
            return results;
        }
    }
}


