using Web.App.Infrastructure.Apis;
using Xunit;
using Xunit.Abstractions;
namespace Web.Tests.Infrastructure;

public class GivenAnInsightApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new InsightApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task GetFinanceYearsShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetCurrentReturnYears();

        VerifyCall(HttpMethod.Get, "api/current-return-years");
    }
}