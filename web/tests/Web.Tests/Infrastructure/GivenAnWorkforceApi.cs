using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Tests.Infrastructure;

public class GivenAnWorkforceApi : ApiClientTestBase
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new WorkforceApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task GetSchoolWorkforceHistoryShouldCallCorrectUrl()
    {
        var api = new WorkforceApi(HttpClient);

        await api.History("123213");

        VerifyCall(HttpMethod.Get, "api/workforce/123213/history");
    }

    [Fact]
    public async Task GetSchoolsWorkforceShouldCallCorrectUrl()
    {
        var query = new ApiQuery().AddIfNotNull("Name", "Foo");
        var api = new WorkforceApi(HttpClient);

        await api.Query(query);

        VerifyCall(HttpMethod.Get, "api/workforce?Name=Foo");
    }
}