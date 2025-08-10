using QuranGuide.Maui.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Services
{
    public interface IHadithService
    {
        Task<IEnumerable<HadithBook>> GetAllBooksAsync();
        Task<IEnumerable<HadithChapter>> GetChaptersAsync(string bookSlug);
        Task<IEnumerable<Hadith>> GetHadithsAsync(string bookSlug, string chapterId = null);
        Task<Hadith> GetHadithAsync(string bookSlug, string hadithNumber);
    }
}
