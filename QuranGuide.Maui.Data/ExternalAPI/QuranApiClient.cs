using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QuranGuide.Maui.Shared;

namespace QuranGuide.Maui.Infrastructure.ExternalAPI
{
    public class QuranApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public QuranApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["QuranApi:BaseUrl"];
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        }
    }
}
