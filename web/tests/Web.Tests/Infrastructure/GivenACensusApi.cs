using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Xunit;
using Xunit.Abstractions;
namespace Web.Tests.Infrastructure;

public class GivenACensusApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new CensusApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task GetSchoolCensusHistoryShouldCallCorrectUrl()
    {
        var api = new CensusApi(HttpClient);

        await api.SchoolHistory("123213");

        VerifyCall(HttpMethod.Get, "api/census/123213/history");
    }

    [Fact]
    public async Task GetSchoolsCensusShouldCallCorrectUrl()
    {
        var query = new ApiQuery().AddIfNotNull("Name", "Foo");
        var api = new CensusApi(HttpClient);

        await api.Query(query);

        VerifyCall(HttpMethod.Get, "api/census?Name=Foo");
    }

    [Fact]
    public async Task GetSchoolCensusShouldCallCorrectUrl()
    {
        var api = new CensusApi(HttpClient);

        await api.Get("123213");

        VerifyCall(HttpMethod.Get, "api/census/123213");
    }
}