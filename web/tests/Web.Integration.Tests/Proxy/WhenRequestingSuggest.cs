using System.Net;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingSuggest(BenchmarkingWebAppClient client) : IClassFixture<BenchmarkingWebAppClient>
{
    [Theory]
    [InlineData("school")]
    [InlineData("trust")]
    public async Task CanReturnInternalServerError(string suggestType)
    {
        var response = await client.SetupEstablishmentWithException()
            .Get(Paths.ApiSuggest("12323", suggestType));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}