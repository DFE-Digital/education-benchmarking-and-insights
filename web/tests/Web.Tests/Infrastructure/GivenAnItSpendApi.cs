using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenAnItSpendApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new ItSpendApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task QuerySchoolsShouldCallCorrectUrl()
    {
        var api = new ItSpendApi(HttpClient);

        var query = new ApiQuery()
            .AddIfNotNull("urn", "123456")
            .AddIfNotNull("urn", "654321");

        await api.QuerySchools(query);

        VerifyCall(HttpMethod.Get, "api/it-spend/schools?urn=123456&urn=654321");
    }
}