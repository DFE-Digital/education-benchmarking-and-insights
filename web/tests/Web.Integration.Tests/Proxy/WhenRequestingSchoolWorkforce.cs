using System.Net;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingSchoolWorkforce(BenchmarkingWebAppClient client) : IClassFixture<BenchmarkingWebAppClient>
{
    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "12345";
        var response = await client.SetupBenchmarkWithException()
            .Get(Paths.ApiEstablishmentWorkforce("school", urn));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}