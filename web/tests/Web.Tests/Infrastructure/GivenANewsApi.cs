using Web.App.Infrastructure.Apis.Content;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenANewsApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new NewsApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task GetNewsArticleShouldCallCorrectUrl()
    {
        var api = new NewsApi(HttpClient);
        const string slug = nameof(slug);

        await api.GetNewsArticle(slug);

        VerifyCall(HttpMethod.Get, $"api/news/{slug}");
    }
}