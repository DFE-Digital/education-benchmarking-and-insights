using Web.App.Infrastructure.Apis.Content;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenAYearsApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new YearsApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task GetFinanceYearsShouldCallCorrectUrl()
    {
        var api = new YearsApi(HttpClient);

        await api.GetCurrentReturnYears();

        VerifyCall(HttpMethod.Get, "api/current-return-years");
    }
}