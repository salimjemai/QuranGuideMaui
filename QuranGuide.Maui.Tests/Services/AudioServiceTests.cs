using System.Net.Http;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using QuranGuide.Maui.Core.DataTransferObjects;
using QuranGuide.Maui.Core.Models;
using QuranGuide.Maui.Infrastructure.ExternalServices;
using QuranGuide.Maui.Shared;
using RichardSzalay.MockHttp;

namespace QuranGuide.Maui.Tests.Services;

public class AudioServiceTests
{
    private static IConfiguration CreateConfig(string baseUrl) => new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string?> { ["QuranApi:BaseUrl"] = baseUrl })
        .Build();

    [Fact]
    public async Task GetAudioEditionsAsync_Maps_Editions()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Get, "https://api.alquran.cloud/v1/edition?format=audio")
                .Respond("application/json", "{\"code\":200,\"status\":\"OK\",\"data\":[{},{ }]} ");

        var httpClient = new HttpClient(mockHttp);
        var sut = new AudioService(httpClient, CreateConfig("https://api.alquran.cloud/v1"));

        var result = await sut.GetAudioEditionsAsync();
        result.Should().HaveCount(2);
    }
}


