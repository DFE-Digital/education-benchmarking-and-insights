using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class WhenFunctionReceivesGetLocalAuthorityRequest : LocalAuthoritiesFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var result = await Functions.GetLocalAuthorityAsync(CreateRequest(), "1") as OkResult;
        
        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }
}