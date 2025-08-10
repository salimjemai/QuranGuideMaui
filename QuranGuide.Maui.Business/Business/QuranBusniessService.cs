using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Business
{
    public class QuranBusinessService
    {
        private readonly IQuranService _quranService;
        private readonly ISearchService _searchService;
        private readonly IUserPreferencesService _userPreferencesService;

        public QuranBusinessService(IQuranService quranService, ISearchService searchService, IUserPreferencesService userPreferencesService)
        {
            _quranService = quranService;
            _searchService = searchService;
            _userPreferencesService = userPreferencesService;
        }

        public async Task<SurahWithAyahs> GetSurahWithUserPreferencesAsync(int number)
        {
            var preferences = await _userPreferencesService.GetUserPreferencesAsync();
            return await _quranService.GetSurahWithAyahsAsync(number, preferences.SelectedEdition);
        }

        public async Task<IEnumerable<SearchResult>> SearchWithUserPreferencesAsync(string keyword)
        {
            var preferences = await _userPreferencesService.GetUserPreferencesAsync();
            return await _searchService.SearchAyahsAsync(keyword, "all", preferences.SelectedEdition);
        }
    }
}
