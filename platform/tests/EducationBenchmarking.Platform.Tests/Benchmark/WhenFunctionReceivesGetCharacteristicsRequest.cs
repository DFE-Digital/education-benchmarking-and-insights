using EducationBenchmarking.Platform.Api.Benchmark.Models;
using EducationBenchmarking.Platform.Api.Benchmark.Models.Characteristics;
using EducationBenchmarking.Platform.Shared;
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