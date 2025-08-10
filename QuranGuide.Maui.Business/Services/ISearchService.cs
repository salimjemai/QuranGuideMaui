using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuranGuide.Maui.Core.DataTransferObjects;

namespace QuranGuide.Maui.Core.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchResult>> SearchAyahsAsync(string keyword, string surah = "all", string edition = "en");
        Task<IEnumerable<SearchResult>> SearchAdvancedAsync(SearchOptions options);
        Task<IEnumerable<SearchResult>> GetSearchHistoryAsync();
        Task SaveSearchQueryAsync(string query);
    }
}
