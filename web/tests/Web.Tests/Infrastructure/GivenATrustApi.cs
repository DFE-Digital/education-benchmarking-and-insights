using Newtonsoft.Json;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.ViewModels;
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

    [Fact]
    public async Task CreateComparatorsAsyncShouldCallCorrectUrl()
    {
        var api = new TrustApi(HttpClient);
        var request = new PostTrustComparatorsRequest(new UserDefinedTrustCharacteristicViewModel());
        const string companyNumber = "companyNumber";

        await api.CreateComparatorsAsync(companyNumber, request);

        VerifyCall(HttpMethod.Post, $"api/trusts/{companyNumber}/comparators", request.ToJson(Formatting.None));
    }

    [Fact]
    public async Task QueryItSpendingShouldCallCorrectUrl()
    {
        var api = new TrustApi(HttpClient);

        var query = new ApiQuery()
            .AddIfNotNull("companyNumber", "12345678")
            .AddIfNotNull("companyNumber", "87654321");

        await api.QueryItSpendingAsync(query);

        VerifyCall(HttpMethod.Get, "api/trusts/budget-forecast/it-spending?companyNumber=12345678&companyNumber=87654321");
    }

    [Fact]
    public async Task ItSpendingForecastShouldCallCorrectUrl()
    {
        var api = new TrustApi(HttpClient);

        const string companyNumber = "12345678";

        await api.ItSpendingForecastAsync(companyNumber);

        VerifyCall(HttpMethod.Get, "api/trusts/12345678/budget-forecast/it-spending/forecast");
    }
}