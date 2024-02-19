using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class WhenFunctionReceivesGetTrustRequest : TrustsFunctionsTestBase
{
    [Fact]
    public void ShouldReturn200OnValidRequest()
    {
        var result = Functions.SingleTrustAsync(CreateRequest(), "1") as OkResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}