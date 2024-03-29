using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesSearchTrustsRequest : TrustsFunctionsTestBase
{
    [Fact]
    public void ShouldReturn200OnValidRequest()
    {
        var result = Functions.SearchTrustsAsync(CreateRequest()) as OkResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}