using System.Net;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingExpenditure(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "12345";
        var response = await client
            .SetupComparatorSetApiWithException()
            .Get(Paths.ApiExpenditure("school", urn, "dummy", "dummy"));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}