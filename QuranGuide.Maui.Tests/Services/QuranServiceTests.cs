using System.Net.Http;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Models;
using QuranGuide.Maui.Infrastructure.ExternalServices;
using QuranGuide.Maui.Shared;
using RichardSzalay.MockHttp;

namespace QuranGuide.Maui.Tests.Services;

public class QuranServiceTests
{
    private static IConfiguration CreateConfig(string baseUrl) => new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string?> { ["QuranApi:BaseUrl"] = baseUrl })
        .Build();

    [Fact]
    public async Task GetAllSurahsAsync_Returns_Surahs()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Get, "https://api.alquran.cloud/v1/surah")
                .Respond("application/json", "{\"code\":200,\"status\":\"OK\",\"data\":[{},{ }]} ");

        var httpClient = new HttpClient(mockHttp);
        var sut = new QuranService(httpClient, CreateConfig("https://api.alquran.cloud/v1"));

        var result = await sut.GetAllSurahsAsync();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetSurahWithAyahsAsync_Returns_Data()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Get, "https://api.alquran.cloud/v1/surah/1/quran-uthmani")
                .Respond("application/json", "{\"code\":200,\"status\":\"OK\",\"data\":{\"number\":1,\"numberOfAyahs\":7,\"ayahs\":[]}} ");

        var httpClient = new HttpClient(mockHttp);
        var sut = new QuranService(httpClient, CreateConfig("https://api.alquran.cloud/v1"));

        var result = await sut.GetSurahWithAyahsAsync(1);
        result.Should().NotBeNull();
        result!.Number.Should().Be(1);
        result.NumberOfAyahs.Should().Be(7);
    }
}


