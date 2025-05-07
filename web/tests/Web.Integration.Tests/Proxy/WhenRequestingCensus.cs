using System.Net;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingCensus(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "123456";
        var response = await client
            .SetupComparatorSetApiWithException()
            .SetupCensusWithException()
            .Get(Paths.ApiCensus(urn, "school", "workforce-fte", "Total"));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}