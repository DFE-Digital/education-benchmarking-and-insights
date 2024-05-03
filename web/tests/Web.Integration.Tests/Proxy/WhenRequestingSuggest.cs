using System.Net;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingSuggest(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    [Theory]
    [InlineData("school")]
    [InlineData("trust")]
    [InlineData("local-authority")]
    public async Task CanReturnInternalServerError(string suggestType)
    {
        var response = await client.SetupEstablishmentWithException()
            .Get(Paths.ApiSuggest("12323", suggestType));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}