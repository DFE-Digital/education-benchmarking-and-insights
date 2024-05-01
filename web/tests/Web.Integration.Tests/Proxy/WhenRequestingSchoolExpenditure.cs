using System.Net;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingSchoolExpenditure(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "12345";
        var response = await client.SetupBenchmarkWithException()
            .Get(Paths.ApiEstablishmentExpenditure("school", urn));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}