using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Models;
using QuranGuide.Maui.Core.Services;
using QuranGuide.Maui.Shared;

namespace QuranGuide.Maui.Infrastructure.ExternalServices
{
    public class ScriptScrapingService : IScriptScrapingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<ScriptScrapingService> _logger;

        public ScriptScrapingService(HttpClient httpClient, IConfiguration configuration, ILogger<ScriptScrapingService> logger)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["QuranApi:BaseUrl"] ?? "https://api.alquran.cloud/v1";
            _logger = logger;
        }

        // https://api.alquran.cloud/v1/edition
        public async Task<IEnumerable<Edition>> GetEditionsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching editions from {Url}", $"{_baseUrl}/edition");
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<Edition[]>>($"{_baseUrl}/edition", cancellationToken);
            return res?.Data ?? [];
        }

        // https://api.alquran.cloud/v1/edition/language
        public async Task<IEnumerable<LanguageDto>> GetLanguagesAsync(CancellationToken cancellationToken = default)
        {
            // Primary: codes only
            _logger.LogInformation("Fetching language codes from {Url}", $"{_baseUrl}/edition/language");
            var codes = await _httpClient.GetFromJsonAsync<ApiResponse<string[]>>($"{_baseUrl}/edition/language", cancellationToken);
            var mapped = (codes?.Data ?? [])
                .Select(code => new LanguageDto { Code = code, Name = code, NativeName = code, Direction = "ltr" });
            var list = mapped.ToList();
            if (list.Count > 0) return list;

            // Fallback: if a detailed endpoint exists
            _logger.LogInformation("Fetching languages from {Url}", $"{_baseUrl}/language");
            var detailed = await _httpClient.GetFromJsonAsync<ApiResponse<LanguageDto[]>>($"{_baseUrl}/language", cancellationToken);
            return detailed?.Data ?? [];
        }

        // TODO Fix this next 
        // https://api.alquran.cloud/v1/edition/type
        public async Task<IEnumerable<string>> GetTypesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching types from {Url}", $"{_baseUrl}/type");
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<string[]>>($"{_baseUrl}/type", cancellationToken);
            if (res?.Data is { Length: > 0 }) return res.Data;
            _logger.LogInformation("Fetching types from {Url}", $"{_baseUrl}/edition/type");
            var res2 = await _httpClient.GetFromJsonAsync<ApiResponse<string[]>>($"{_baseUrl}/edition/type", cancellationToken);
            return res2?.Data ?? [];
        }

        // https://api.alquran.cloud/v1/edition?format=audio&language=fr&type=versebyverse
        // https://api.alquran.cloud/v1/edition/format
        public async Task<IEnumerable<string>> GetFormatsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching formats from {Url}", $"{_baseUrl}/format");
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<string[]>>($"{_baseUrl}/format", cancellationToken);
            if (res?.Data is { Length: > 0 }) return res.Data;
            _logger.LogInformation("Fetching formats from {Url}", $"{_baseUrl}/edition/format");
            var res2 = await _httpClient.GetFromJsonAsync<ApiResponse<string[]>>($"{_baseUrl}/edition/format", cancellationToken);
            return res2?.Data ?? [];
        }

        public async Task<IEnumerable<Surah>> GetSurahsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching surahs from {Url}", $"{_baseUrl}/surah");
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<Surah[]>>($"{_baseUrl}/surah", cancellationToken);
            return res?.Data ?? [];
        }

        public async Task<object?> GetMetaAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching meta from {Url}", $"{_baseUrl}/meta");
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<object>>($"{_baseUrl}/meta", cancellationToken);
            return res?.Data;
        }


        public async Task<QuranEditionData?> GetQuranByEditionAsync(string edition, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching quran by edition {Edition} from {Url}", edition, $"{_baseUrl}/quran/{edition}");
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<QuranEditionData>>($"{_baseUrl}/quran/{edition}", cancellationToken);
            return res?.Data;
        }

        public async Task<IEnumerable<SurahEditionData>> GetSurahEditionsAsync(int surahNumber, IEnumerable<string> editions, CancellationToken cancellationToken = default)
        {
            var editionsCsv = string.Join(',', editions);
            _logger.LogInformation("Fetching surah {Surah} editions {Editions}", surahNumber, editionsCsv);
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<SurahEditionData[]>>($"{_baseUrl}/surah/{surahNumber}/editions/{editionsCsv}", cancellationToken);
            return res?.Data ?? [];
        }

        public async Task<IEnumerable<AyahDto>> GetJuzAyahsAsync(int juzNumber, string edition, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching juz {Juz} for {Edition}", juzNumber, edition);
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<AyahDto[]>>($"{_baseUrl}/juz/{juzNumber}/{edition}", cancellationToken);
            return res?.Data ?? [];
        }

        public async Task<IEnumerable<AyahDto>> GetManzilAyahsAsync(int manzilNumber, string edition, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching manzil {Manzil} for {Edition}", manzilNumber, edition);
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<AyahDto[]>>($"{_baseUrl}/manzil/{manzilNumber}/{edition}", cancellationToken);
            return res?.Data ?? [];
        }

        public async Task<IEnumerable<AyahDto>> GetRukuAyahsAsync(int rukuNumber, string edition, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching ruku {Ruku} for {Edition}", rukuNumber, edition);
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<AyahDto[]>>($"{_baseUrl}/ruku/{rukuNumber}/{edition}", cancellationToken);
            return res?.Data ?? [];
        }

        public async Task<IEnumerable<AyahDto>> GetPageAyahsAsync(int pageNumber, string edition, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching page {Page} for {Edition}", pageNumber, edition);
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<AyahDto[]>>($"{_baseUrl}/page/{pageNumber}/{edition}", cancellationToken);
            return res?.Data ?? [];
        }

        public async Task<IEnumerable<AyahDto>> GetHizbQuarterAyahsAsync(int hizbNumber, string edition, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching hizbQuarter {Hizb} for {Edition}", hizbNumber, edition);
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<AyahDto[]>>($"{_baseUrl}/hizbQuarter/{hizbNumber}/{edition}", cancellationToken);
            return res?.Data ?? [];
        }

        public async Task<IEnumerable<SajdaItem>> GetSajdasAsync(string edition, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching sajdas for {Edition}", edition);
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<SajdaItem[]>>($"{_baseUrl}/sajda/{edition}", cancellationToken);
            return res?.Data ?? [];
        }
    }
}

// http://api.alquran.cloud/v1/language
// https://api.alquran.cloud/v1/edition/language/en
// https://api.alquran.cloud/v1/edition/type
// https://api.alquran.cloud/v1/edition/type/translation
// https://api.alquran.cloud/v1/edition/format
// http://api.alquran.cloud/v1/format/text
// http://api.alquran.cloud/v1/quran/en.asad
// http://api.alquran.cloud/v1/quran/quran-uthmani
// audio : http://api.alquran.cloud/v1/quran/ar.alafasy

