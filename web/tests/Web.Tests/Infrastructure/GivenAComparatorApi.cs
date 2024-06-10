using Newtonsoft.Json;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.ViewModels;
using Xunit;
using Xunit.Abstractions;
namespace Web.Tests.Infrastructure;

public class GivenAComparatorApi(ITestOutputHelper testOutputHelper) : ApiClientTestBase(testOutputHelper)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new ComparatorApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task CreateSchoolsAsyncShouldCallCorrectUrl()
    {
        var api = new ComparatorApi(HttpClient);

        var request = new PostSchoolComparatorsRequest("urn", "laName", new UserDefinedCharacteristicViewModel());

        await api.CreateSchoolsAsync(request);

        VerifyCall(HttpMethod.Post, "api/comparators/schools", request.ToJson(Formatting.None));
    }
}