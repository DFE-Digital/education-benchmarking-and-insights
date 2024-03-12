using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Tests.Infrastructure;

public class GivenAnEstablishmentApi : ApiClientTestBase
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
    public async Task SuggestSchoolsShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.SuggestSchools("");

        VerifyCall(HttpMethod.Post, "api/schools/suggest");
    }

    [Fact]
    public async Task SuggestTrustsShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.SuggestTrusts("");

        VerifyCall(HttpMethod.Post, "api/trusts/suggest");
    }

    [Fact]
    public async Task SuggestOrganisationsShouldCallCorrectUrl()
    {
        var api = new EstablishmentApi(HttpClient);

        await api.SuggestOrganisations("");

        VerifyCall(HttpMethod.Post, "api/organisations/suggest");
    }
}