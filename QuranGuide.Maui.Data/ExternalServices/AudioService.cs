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
    public class AudioService : IAudioService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public AudioService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["QuranApi:BaseUrl"] ?? "http://localhost:3001/api";
        }

        public async Task<IEnumerable<AudioEdition>> GetAudioEditionsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/edition?format=audio");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Edition[]>>();
            return apiResponse?.Data?.Select(e => new AudioEdition
            {
                Identifier = e.Identifier,
                Name = e.EnglishName,
                Language = e.Language,
                Bitrates = new[] { 32, 40, 48, 64, 128, 192 }
            }) ?? Enumerable.Empty<AudioEdition>();
        }
        public async Task<AudioTrack> GetAyahAudioAsync(string edition, int ayahNumber)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ayah/{ayahNumber}/ar/{edition}");
            response.EnsureSuccessStatusCode();
            // This endpoint shape is hypothetical; adjust to your API schema
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<AudioTrack>>();
            return apiResponse?.Data!;
        }

        public async Task<AudioTrack> GetSurahAudioAsync(string edition, int surahNumber)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/surah/{surahNumber}/audio/{edition}");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<AudioTrack>>();
            return apiResponse?.Data!;
        }

        public Task<bool> DownloadAudioAsync(string edition, int surahNumber, string filePath)
        {
            // Stub implementation for now
            return Task.FromResult(false);
        }

        public Task<bool> IsAudioDownloadedAsync(string edition, int surahNumber)
        {
            // Stub implementation for now
            return Task.FromResult(false);
        }
    }
}
