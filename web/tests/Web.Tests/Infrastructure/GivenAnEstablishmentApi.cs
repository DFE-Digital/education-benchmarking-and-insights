using Web.App.Infrastructure.Apis;
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
            "{\"searchText\":\"term\",\"size\":10,\"suggesterName\":\"school-suggester\"}");
    }

    [Fact]
    public async Task SuggestSchoolsWithExclusionShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        const string exclude = "exclude";
        await api.SuggestSchools("term", new ApiQuery().AddIfNotNull("urns", exclude));

        VerifyCall(
            HttpMethod.Post,
            "api/schools/suggest?urns=" + exclude,
            "{\"searchText\":\"term\",\"size\":10,\"suggesterName\":\"school-suggester\"}");
    }

    [Fact]
    public async Task SuggestTrustsShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.SuggestTrusts("term");

        VerifyCall(
            HttpMethod.Post,
            "api/trusts/suggest",
            "{\"searchText\":\"term\",\"size\":10,\"suggesterName\":\"trust-suggester\"}");
    }

    [Fact]
    public async Task SuggestSuggestLocalAuthoritiesShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        const string exclude = "exclude";
        await api.SuggestLocalAuthorities("term", new ApiQuery().AddIfNotNull("names", exclude));

        VerifyCall(
            HttpMethod.Post,
            "api/local-authorities/suggest?names=" + exclude,
            "{\"searchText\":\"term\",\"size\":10,\"suggesterName\":\"local-authority-suggester\"}");
    }
}