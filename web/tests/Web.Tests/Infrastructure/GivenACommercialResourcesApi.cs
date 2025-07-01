using Web.App.Infrastructure.Apis.Content;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenACommercialResourcesApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new CommercialResourcesApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task GetCommercialResourcesShouldCallCorrectUrl()
    {
        var api = new CommercialResourcesApi(HttpClient);

        await api.GetCommercialResources();

        VerifyCall(HttpMethod.Get, "api/commercial-resources");
    }
}