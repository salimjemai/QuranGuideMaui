using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuranGuide.Maui.Core.DataTransferObjects;

namespace QuranGuide.Maui.Core.Services
{
    public interface ITafseerService
    {
        Task<IEnumerable<TafseerEdition>> GetTafseerEditionsAsync();
        Task<TafseerData> GetTafseerAsync(int surahNumber, int ayahNumber, string edition);
        Task<IEnumerable<TafseerData>> GetTafseerForAyahAsync(int surahNumber, int ayahNumber);
    }
}
