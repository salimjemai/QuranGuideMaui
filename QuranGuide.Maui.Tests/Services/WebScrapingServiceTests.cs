using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using FluentAssertions;
using QuranGuide.Maui.Core.Services;
using QuranGuide.Maui.Infrastructure.ExternalServices;
using RichardSzalay.MockHttp;

namespace QuranGuide.Maui.Tests.Services;

public class WebScrapingServiceTests
{
    [Fact]
    public async Task GetStringAsync_Returns_Response_String()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Get, "https://example.com/hello")
                .Respond("text/plain", "world");

        var httpClient = new HttpClient(mockHttp);
        var sut = new WebScrapingService(httpClient);

        var response = await sut.GetStringAsync("https://example.com/hello");

        response.Should().Be("world");
    }

    [Fact]
    public async Task GetJsonAsync_Deserializes_Json()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Get, "https://api.example.com/item/1")
                .Respond("application/json", "{\"id\":1,\"name\":\"Alpha\"}");

        var httpClient = new HttpClient(mockHttp);
        var sut = new WebScrapingService(httpClient);

        var item = await sut.GetJsonAsync<TestItem>("https://api.example.com/item/1");

        item.Should().NotBeNull();
        item!.Id.Should().Be(1);
        item.Name.Should().Be("Alpha");
    }

    [Fact]
    public async Task PostJsonAsync_Sends_Payload_And_Parses_Response()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Post, "https://api.example.com/item")
                .Respond("application/json", "{\"id\":2,\"name\":\"Created\"}");

        var httpClient = new HttpClient(mockHttp);
        var sut = new WebScrapingService(httpClient);

        var created = await sut.PostJsonAsync<TestItem, TestItem>("https://api.example.com/item", new TestItem { Id = 0, Name = "New" });

        created.Should().NotBeNull();
        created!.Id.Should().Be(2);
        created.Name.Should().Be("Created");
    }

    [Fact]
    public async Task BulkGetJsonAsync_Fetches_All_In_Parallel()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(HttpMethod.Get, "https://api.example.com/item/1")
                .Respond("application/json", "{\"id\":1,\"name\":\"One\"}");
        mockHttp.When(HttpMethod.Get, "https://api.example.com/item/2")
                .Respond("application/json", "{\"id\":2,\"name\":\"Two\"}");

        var httpClient = new HttpClient(mockHttp);
        var sut = new WebScrapingService(httpClient);

        var results = await sut.BulkGetJsonAsync<TestItem>(new[]
        {
            "https://api.example.com/item/1",
            "https://api.example.com/item/2"
        });

        results.Should().HaveCount(2);
        results[0]!.Id.Should().Be(1);
        results[1]!.Id.Should().Be(2);
    }

    private sealed class TestItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}


