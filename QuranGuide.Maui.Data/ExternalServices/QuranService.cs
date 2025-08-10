using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Models;
using QuranGuide.Maui.Core.Services;
using QuranGuide.Maui.Shared;

namespace QuranGuide.Maui.Infrastructure.ExternalServices
{
    public class QuranService : IQuranService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public QuranService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["QuranApi:BaseUrl"] ?? "http://localhost:3001/api";
        }

        public async Task<IEnumerable<Surah>> GetAllSurahsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/surah");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Surah[]>>();
            return apiResponse?.Data ?? Enumerable.Empty<Surah>();
        }

        public async Task<SurahWithAyahs> GetSurahWithAyahsAsync(int number, string edition = "quran-uthmani")
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/surah/{number}/{edition}");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<SurahWithAyahs>>();
            return apiResponse?.Data;
        }

        public async Task<Surah> GetSurahAsync(int number)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/surah/{number}");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Surah>>();
            return apiResponse?.Data!;
        }

        public async Task<Ayah> GetAyahAsync(string reference, string edition = "quran-uthmani")
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ayah/{reference}/{edition}");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Ayah>>();
            return apiResponse?.Data!;
        }

        public async Task<IEnumerable<Ayah>> GetJuzAsync(int number, string edition = "quran-uthmani")
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/juz/{number}/{edition}");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Ayah[]>>();
            return apiResponse?.Data ?? Enumerable.Empty<Ayah>();
        }

        public async Task<IEnumerable<Ayah>> GetSajdaAyahsAsync(string edition = "quran-uthmani")
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/sajda/{edition}");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Ayah[]>>();
            return apiResponse?.Data ?? Enumerable.Empty<Ayah>();
        }
    }
}
