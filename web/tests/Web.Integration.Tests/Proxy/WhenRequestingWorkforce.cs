using System.Net;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingWorkforce(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "12345";
        var response = await client.SetupWorkforceWithException()
            .Get(Paths.ApiWorkforce(urn, "school", "workforce-fte", "Total"));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}