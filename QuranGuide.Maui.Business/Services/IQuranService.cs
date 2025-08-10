using QuranGuide.Maui.Core.Models;
using QuranGuide.Maui.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Core.Services
{
    public interface IQuranService
    {
        Task<IEnumerable<Surah>> GetAllSurahsAsync();
        Task<Surah> GetSurahAsync(int number);
        Task<SurahWithAyahs> GetSurahWithAyahsAsync(int number, string edition = "quran-uthmani");
        Task<Ayah> GetAyahAsync(string reference, string edition = "quran-uthmani");
        Task<IEnumerable<Ayah>> GetJuzAsync(int number, string edition = "quran-uthmani");
        Task<IEnumerable<Ayah>> GetSajdaAyahsAsync(string edition = "quran-uthmani");
    }
}
