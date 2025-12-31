using Newtonsoft.Json;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.ViewModels;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenASchoolApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new SchoolApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task SingleShouldCallCorrectUrl()
    {
        var api = new SchoolApi(HttpClient);

        await api.SingleAsync("12343");

        VerifyCall(HttpMethod.Get, "api/schools/12343");
    }


    [Fact]
    public async Task SuggestShouldCallCorrectUrl()
    {
        var api = new SchoolApi(HttpClient);

        await api.SuggestAsync("term");

        VerifyCall(
            HttpMethod.Post,
            "api/schools/suggest",
            "{\"searchText\":\"term\",\"size\":10}");
    }

    [Fact]
    public async Task SuggestWithExclusionShouldCallCorrectUrl()
    {
        var api = new SchoolApi(HttpClient);

        const string exclude = "exclude";
        await api.SuggestAsync("term", [exclude]);

        VerifyCall(
            HttpMethod.Post,
            "api/schools/suggest",
            "{\"searchText\":\"term\",\"size\":10,\"exclude\":[\"exclude\"]}");
    }

    [Fact]
    public async Task SuggestWithFinancialExclusionShouldCallCorrectUrl()
    {
        var api = new SchoolApi(HttpClient);

        await api.SuggestAsync("term", null, true);

        VerifyCall(
            HttpMethod.Post,
            "api/schools/suggest",
            "{\"searchText\":\"term\",\"size\":10,\"excludeMissingFinancialData\":true}");
    }

    [Fact]
    public async Task SearchShouldCallCorrectUrl()
    {
        var api = new SchoolApi(HttpClient);
        var request = new SearchRequest();

        await api.SearchAsync(request);

        VerifyCall(HttpMethod.Post, "api/schools/search", request.ToJson(Formatting.None));
    }

    [Fact]
    public async Task CreateComparatorsAsyncShouldCallCorrectUrl()
    {
        var api = new SchoolApi(HttpClient);
        var request = new PostSchoolComparatorsRequest("laName", new UserDefinedSchoolCharacteristicViewModel());
        const string urn = "urn";

        await api.CreateComparatorsAsync(urn, request);

        VerifyCall(HttpMethod.Post, $"api/schools/{urn}/comparators", request.ToJson(Formatting.None));
    }
}