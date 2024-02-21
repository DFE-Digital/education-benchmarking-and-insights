using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class WhenFunctionReceivesQueryTrustsRequest : TrustsFunctionsTestBase
{
    [Fact]
    public void ShouldReturn200OnValidRequest()
    {
        var result = Functions.QueryTrustsAsync(CreateRequest()) as OkResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}