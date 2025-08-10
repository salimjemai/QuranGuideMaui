using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuranGuide.Maui.Core.DataTransferObjects;

namespace QuranGuide.Maui.Core.Services
{
    public interface IUserPreferencesService
    {
        Task<UserPreferences> GetUserPreferencesAsync();
        Task SaveUserPreferencesAsync(UserPreferences preferences);
        Task<string> GetSelectedEditionAsync();
        Task SetSelectedEditionAsync(string edition);
        Task<string> GetSelectedLanguageAsync();
        Task SetSelectedLanguageAsync(string language);
    }
}
