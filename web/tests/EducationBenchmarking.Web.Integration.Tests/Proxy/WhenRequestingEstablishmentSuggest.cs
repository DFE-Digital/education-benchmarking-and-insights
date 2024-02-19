using System.Net;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Proxy;

public class WhenRequestingEstablishmentSuggest(BenchmarkingWebAppClient client) : IClassFixture<BenchmarkingWebAppClient>
{
    [Theory]
    [InlineData("school")]
    [InlineData("trust")]
    [InlineData("organisation")]
    public async Task CanReturnInternalServerError(string suggestType)
    {
        var response = await client.SetupEstablishmentWithException()
            .Get(Paths.ApiEstablishmentSuggest("12323", suggestType));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}