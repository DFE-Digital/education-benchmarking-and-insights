using EducationBenchmarking.Platform.Domain.Responses.Characteristics;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class WhenFunctionReceivesGetCharacteristicsRequest : ComparatorSetFunctionsTestBase
{
    [Fact]
    public void ShouldReturn200OnValidRequest()
    {
        var result = Functions.GetCharacteristics(CreateRequest()) as JsonContentResult;
        
        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
        Assert.Equal(Characteristics.All.ToJson(), result?.Content);
    }
}