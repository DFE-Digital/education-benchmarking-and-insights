using Newtonsoft.Json;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenALocalAuthorityApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new LocalAuthorityApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task SingleShouldCallCorrectUrl()
    {
        var api = new LocalAuthorityApi(HttpClient);

        await api.SingleAsync("12343");

        VerifyCall(HttpMethod.Get, "api/local-authorities/12343");
    }

    [Fact]
    public async Task SuggestShouldCallCorrectUrl()
    {
        var api = new LocalAuthorityApi(HttpClient);

        const string exclude = "exclude";
        await api.SuggestAsync("term", [exclude]);

        VerifyCall(
            HttpMethod.Post,
            "api/local-authorities/suggest",
            "{\"searchText\":\"term\",\"size\":10,\"exclude\":[\"exclude\"]}");
    }

    [Fact]
    public async Task StatisticalNeighboursShouldCallCorrectUrl()
    {
        var api = new LocalAuthorityApi(HttpClient);
        const string identifier = nameof(identifier);

        await api.StatisticalNeighboursAsync(identifier);

        VerifyCall(HttpMethod.Get, $"api/local-authorities/{identifier}/statistical-neighbours");
    }

    [Fact]
    public async Task SearchShouldCallCorrectUrl()
    {
        var api = new LocalAuthorityApi(HttpClient);
        var request = new SearchRequest();

        await api.SearchAsync(request);

        VerifyCall(HttpMethod.Post, "api/local-authorities/search", request.ToJson(Formatting.None));
    }
}