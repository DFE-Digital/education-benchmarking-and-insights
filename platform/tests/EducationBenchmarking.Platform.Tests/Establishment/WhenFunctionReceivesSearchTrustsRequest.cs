using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class WhenFunctionReceivesSearchTrustsRequest : TrustsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var result = await Functions.SearchTrustsAsync(CreateRequest()) as OkResult;
        
        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }
}