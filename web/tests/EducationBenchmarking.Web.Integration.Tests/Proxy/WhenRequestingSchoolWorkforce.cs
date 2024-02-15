using System.Net;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Proxy;

public class WhenRequestingSchoolWorkforce(BenchmarkingWebAppClient client) : IClassFixture<BenchmarkingWebAppClient>
{
    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "12345";
        var response = await client.SetupBenchmarkWithException()
            .Get(Paths.ApiSchoolSchoolWorkforce(urn));
        
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}