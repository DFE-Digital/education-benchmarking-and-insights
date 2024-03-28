using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight;

public class WhenFunctionReceivesGetFinanceYearsRequest : MiscFunctionsTestBase
{
    [Fact]
    public void ShouldReturn200OnValidRequest()
    {

        var result = Functions.SingleCurrentReturnYearsAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}