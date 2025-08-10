using System.Net.Http;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Infrastructure.ExternalServices;
using QuranGuide.Maui.Shared;
using RichardSzalay.MockHttp;

namespace QuranGuide.Maui.Tests.Services;

public class ScriptScrapingServiceTests
{
    private readonly Mock<ILogger<ScriptScrapingService>> _logger;

    public ScriptScrapingServiceTests()
    {
        _logger = new Mock<ILogger<ScriptScrapingService>>();
    }

    private static IConfiguration Config => new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string?> { ["QuranApi:BaseUrl"] = "https://api.alquran.cloud/v1" })
        .Build();

    [Fact]
    public async Task GetEditionsAsync_Returns_List()
    {
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, "https://api.alquran.cloud/v1/edition")
            .Respond("application/json", "{\"code\":200,\"status\":\"OK\",\"data\":[{},{ }]} ");

        var client = new HttpClient(mock);
        var sut = new ScriptScrapingService(client, Config, _logger.Object);

        var editions = await sut.GetEditionsAsync();
        editions.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetLanguagesAsync_Returns_List()
    {
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, "https://api.alquran.cloud/v1/language")
            .Respond("application/json", "{\"code\":200,\"status\":\"OK\",\"data\":[{\"code\":\"en\",\"name\":\"English\",\"nativeName\":\"English\",\"direction\":\"ltr\"}]} ");

        var client = new HttpClient(mock);
        var sut = new ScriptScrapingService(client, Config, _logger.Object);

        var languages = await sut.GetLanguagesAsync();
        languages.Should().ContainSingle(l => l.Code == "en");
    }

    [Fact]
    public async Task GetQuranByEditionAsync_Returns_Surahs()
    {
        var payload = await File.ReadAllTextAsync(Path.Combine("TestData", "quran-by-edition.json"));
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, "https://api.alquran.cloud/v1/quran/quran-uthmani")
            .Respond("application/json", payload);

        var client = new HttpClient(mock);
        var sut = new ScriptScrapingService(client, Config, _logger.Object);
        var data = await sut.GetQuranByEditionAsync("quran-uthmani");
        data.Should().NotBeNull();
        data!.Surahs.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetSurahEditionsAsync_Returns_Aggregate()
    {
        var payload = await File.ReadAllTextAsync(Path.Combine("TestData", "surah-editions.json"));
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, "https://api.alquran.cloud/v1/surah/1/editions/en.pickthall,quran-uthmani")
            .Respond("application/json", payload);

        var client = new HttpClient(mock);
        var sut = new ScriptScrapingService(client, Config, _logger.Object);
        var data = await sut.GetSurahEditionsAsync(1, new[] { "en.pickthall", "quran-uthmani" });
        data.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetJuzAyahsAsync_Parses_Data()
    {
        var payload = await File.ReadAllTextAsync(Path.Combine("TestData", "juz-1.json"));
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, "https://api.alquran.cloud/v1/juz/1/quran-uthmani").Respond("application/json", payload);
        var sut = new ScriptScrapingService(new HttpClient(mock), Config, _logger.Object);
        var result = await sut.GetJuzAyahsAsync(1, "quran-uthmani");
        result.Should().HaveCount(2);
        result.First().Surah.EnglishNameTranslation.Should().Be("The Opening");
    }

    [Fact]
    public async Task GetManzilAyahsAsync_Parses_Data()
    {
        var payload = await File.ReadAllTextAsync(Path.Combine("TestData", "manzil-1.json"));
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, "https://api.alquran.cloud/v1/manzil/1/quran-uthmani").Respond("application/json", payload);
        var sut = new ScriptScrapingService(new HttpClient(mock), Config, _logger.Object);
        var result = await sut.GetManzilAyahsAsync(1, "quran-uthmani");
        result.Should().HaveCount(1);
        result.First().Surah.Name.Should().Be("Al-Baqarah");
    }

    [Fact]
    public async Task GetRukuAyahsAsync_Parses_Data()
    {
        var payload = await File.ReadAllTextAsync(Path.Combine("TestData", "ruku-1.json"));
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, "https://api.alquran.cloud/v1/ruku/1/quran-uthmani").Respond("application/json", payload);
        var sut = new ScriptScrapingService(new HttpClient(mock), Config, _logger.Object);
        var result = await sut.GetRukuAyahsAsync(1, "quran-uthmani");
        result.Should().HaveCount(1);
        result.First().NumberInSurah.Should().Be(3);
    }

    [Fact]
    public async Task GetPageAyahsAsync_Parses_Data()
    {
        var payload = await File.ReadAllTextAsync(Path.Combine("TestData", "page-1.json"));
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, "https://api.alquran.cloud/v1/page/1/quran-uthmani").Respond("application/json", payload);
        var sut = new ScriptScrapingService(new HttpClient(mock), Config, _logger.Object);
        var result = await sut.GetPageAyahsAsync(1, "quran-uthmani");
        result.Should().HaveCount(1);
        result.First().Page.Should().Be(1);
    }

    [Fact]
    public async Task GetHizbQuarterAyahsAsync_Parses_Data()
    {
        var payload = await File.ReadAllTextAsync(Path.Combine("TestData", "hizb-1.json"));
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, "https://api.alquran.cloud/v1/hizbQuarter/1/quran-uthmani").Respond("application/json", payload);
        var sut = new ScriptScrapingService(new HttpClient(mock), Config, _logger.Object);
        var result = await sut.GetHizbQuarterAyahsAsync(1, "quran-uthmani");
        result.Should().HaveCount(1);
        result.First().HizbQuarter.Should().Be(1);
    }

    [Fact]
    public async Task GetSajdasAsync_Returns_Items()
    {
        var payload = await File.ReadAllTextAsync(Path.Combine("TestData", "sajdas.json"));
        var mock = new MockHttpMessageHandler();
        mock.When(HttpMethod.Get, $"https://api.alquran.cloud/v1/sajda/quran-uthmani")
            .Respond("application/json", payload);

        var client = new HttpClient(mock);
        var sut = new ScriptScrapingService(client, Config, _logger.Object);
        var items = await sut.GetSajdasAsync("quran-uthmani");
        items.Should().NotBeNull();
        items.First().Surah.EnglishNameTranslation.Should().Be("The Prostration");
    }
}


