using System.Threading;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Services
{
    public interface IScrapeIngestionService
    {
        Task<object> SaveAllAsync(string edition, CancellationToken cancellationToken = default);
    }
}


