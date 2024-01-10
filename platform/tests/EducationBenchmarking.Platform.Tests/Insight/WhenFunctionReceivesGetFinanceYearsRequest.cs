using EducationBenchmarking.Platform.Functions;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class WhenFunctionReceivesGetFinanceYearsRequest : MiscFunctionsTestBase
{
    [Fact]
    public void ShouldReturn200OnValidRequest()
    {
        
        var result = Functions.GetFinanceYears(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }
}