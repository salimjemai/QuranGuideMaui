using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Services
{
    public interface IWebScrapingService
    {
        // Raw fetch (useful for non-JSON endpoints or diagnostics)
        Task<string> GetStringAsync(string url, CancellationToken cancellationToken = default);

        // JSON helpers for API aggregation
        Task<T?> GetJsonAsync<T>(string url, CancellationToken cancellationToken = default);
        Task<TResponse?> PostJsonAsync<TRequest, TResponse>(string url, TRequest payload, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T?>> BulkGetJsonAsync<T>(IEnumerable<string> urls, CancellationToken cancellationToken = default);
    }
}
