using Newtonsoft.Json;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenAnTrustApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new TrustApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task SingleShouldCallCorrectUrl()
    {
        var api = new TrustApi(HttpClient);

        await api.SingleAsync("12343");

        VerifyCall(HttpMethod.Get, "api/trusts/12343");
    }

    [Fact]
    public async Task SuggestShouldCallCorrectUrl()
    {
        var api = new TrustApi(HttpClient);

        await api.SuggestAsync("term");

        VerifyCall(
            HttpMethod.Post,
            "api/trusts/suggest",
            "{\"searchText\":\"term\",\"size\":10}");
    }

    [Fact]
    public async Task SearchShouldCallCorrectUrl()
    {
        var api = new TrustApi(HttpClient);
        var request = new SearchRequest();

        await api.SearchAsync(request);

        VerifyCall(HttpMethod.Post, "api/trusts/search", request.ToJson(Formatting.None));
    }
}