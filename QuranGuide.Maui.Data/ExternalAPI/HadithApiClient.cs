using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuranGuide.Maui.Infrastructure.ExternalAPI
{
    public class HadithApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public HadithApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["HadithApi:ApiKey"];
            _baseUrl = configuration["HadithApi:BaseUrl"];
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var url = $"{_baseUrl}/{endpoint}?apiKey={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
