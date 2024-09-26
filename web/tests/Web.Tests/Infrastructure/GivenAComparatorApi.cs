using Newtonsoft.Json;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
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
        var request = new PostSchoolComparatorsRequest("laName", new UserDefinedSchoolCharacteristicViewModel());
        const string urn = "urn";

        await api.CreateSchoolsAsync(urn, request);

        VerifyCall(HttpMethod.Post, $"api/school/{urn}/comparators", request.ToJson(Formatting.None));
    }

    [Fact]
    public async Task CreateTrustsAsyncShouldCallCorrectUrl()
    {
        var api = new ComparatorApi(HttpClient);
        var request = new PostTrustComparatorsRequest(new UserDefinedTrustCharacteristicViewModel());
        const string companyNumber = "companyNumber";

        await api.CreateTrustsAsync(companyNumber, request);

        VerifyCall(HttpMethod.Post, $"api/trust/{companyNumber}/comparators", request.ToJson(Formatting.None));
    }
}