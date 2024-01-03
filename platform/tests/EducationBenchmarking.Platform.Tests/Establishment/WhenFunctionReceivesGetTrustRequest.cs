using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class WhenFunctionReceivesGetTrustRequest : TrustsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var result = await Functions.GetTrustAsync(CreateRequest(), "1") as OkResult;
        
        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }
}