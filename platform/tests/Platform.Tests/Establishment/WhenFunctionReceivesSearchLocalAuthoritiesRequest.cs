using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesSearchLocalAuthoritiesRequest : LocalAuthoritiesFunctionsTestBase
{
    [Fact]
    public void ShouldReturn200OnValidRequest()
    {
        var result = Functions.SearchLocalAuthoritiesAsync(CreateRequest()) as OkResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}