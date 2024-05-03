using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Tests.Infrastructure;

public class GivenAnCensusApi : ApiClientTestBase
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

        await api.History("123213");

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
}