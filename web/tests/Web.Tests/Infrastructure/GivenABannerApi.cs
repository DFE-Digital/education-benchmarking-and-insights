using Web.App.Infrastructure.Apis.Content;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenABannerApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new BannerApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task GetBannerShouldCallCorrectUrl()
    {
        var api = new BannerApi(HttpClient);
        const string target = nameof(target);

        await api.GetBanner(target);

        VerifyCall(HttpMethod.Get, $"api/banner/{target}");
    }
}