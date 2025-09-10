using Newtonsoft.Json;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenAnEstablishmentApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new EstablishmentApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task GetSchoolShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.GetSchool("12343");

        VerifyCall(HttpMethod.Get, "api/school/12343");
    }

    [Fact]
    public async Task GetTrustShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.GetTrust("12343");

        VerifyCall(HttpMethod.Get, "api/trust/12343");
    }

    [Fact]
    public async Task GetLocalAuthorityShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.GetLocalAuthority("12343");

        VerifyCall(HttpMethod.Get, "api/local-authority/12343");
    }

    [Fact]
    public async Task SuggestSchoolsShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.SuggestSchools("term");

        VerifyCall(
            HttpMethod.Post,
            "api/schools/suggest",
            "{\"searchText\":\"term\",\"size\":10}");
    }

    [Fact]
    public async Task SuggestSchoolsWithExclusionShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        const string exclude = "exclude";
        await api.SuggestSchools("term", [exclude]);

        VerifyCall(
            HttpMethod.Post,
            "api/schools/suggest",
            "{\"searchText\":\"term\",\"size\":10,\"exclude\":[\"exclude\"]}");
    }

    [Fact]
    public async Task SuggestSchoolsWithFinancialExclusionShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.SuggestSchools("term", null, true);

        VerifyCall(
            HttpMethod.Post,
            "api/schools/suggest",
            "{\"searchText\":\"term\",\"size\":10,\"excludeMissingFinancialData\":true}");
    }

    [Fact]
    public async Task SuggestTrustsShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.SuggestTrusts("term");

        VerifyCall(
            HttpMethod.Post,
            "api/trusts/suggest",
            "{\"searchText\":\"term\",\"size\":10}");
    }

    [Fact]
    public async Task SuggestSuggestLocalAuthoritiesShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        const string exclude = "exclude";
        await api.SuggestLocalAuthorities("term", [exclude]);

        VerifyCall(
            HttpMethod.Post,
            "api/local-authorities/suggest",
            "{\"searchText\":\"term\",\"size\":10,\"exclude\":[\"exclude\"]}");
    }

    [Theory]
    [InlineData("asc", "api/local-authorities/national-rank?sort=asc")]
    [InlineData("desc", "api/local-authorities/national-rank?sort=desc")]
    [InlineData(null, "api/local-authorities/national-rank")]
    public async Task GetLocalAuthoritiesNationalRankShouldCallCorrectUrl(string? sort, string expected)
    {
        var api = new EstablishmentApi(HttpClient);

        await api.GetLocalAuthoritiesNationalRank(string.IsNullOrWhiteSpace(sort) ? [] : [new QueryParameter("sort", sort)]);

        VerifyCall(HttpMethod.Get, expected);
    }

    [Fact]
    public async Task GetLocalAuthorityStatisticalNeighboursShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);
        const string identifier = nameof(identifier);

        await api.GetLocalAuthorityStatisticalNeighbours(identifier);

        VerifyCall(HttpMethod.Get, $"api/local-authority/{identifier}/statistical-neighbours");
    }

    [Fact]
    public async Task SearchSchoolsShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);
        var request = new SearchRequest();

        await api.SearchSchools(request);

        VerifyCall(HttpMethod.Post, "api/schools/search", request.ToJson(Formatting.None));
    }

    [Fact]
    public async Task SearchTrustsShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);
        var request = new SearchRequest();

        await api.SearchTrusts(request);

        VerifyCall(HttpMethod.Post, "api/trusts/search", request.ToJson(Formatting.None));
    }

    [Fact]
    public async Task SearchLocalAuthoritiesShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);
        var request = new SearchRequest();

        await api.SearchLocalAuthorities(request);

        VerifyCall(HttpMethod.Post, "api/local-authorities/search", request.ToJson(Formatting.None));
    }
}