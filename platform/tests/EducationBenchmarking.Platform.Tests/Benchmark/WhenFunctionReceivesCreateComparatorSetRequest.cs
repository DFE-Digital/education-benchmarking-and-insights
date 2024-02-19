using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class WhenFunctionReceivesCreateComparatorSetRequest : ComparatorSetFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.CreateSet())
            .ReturnsAsync(new ComparatorSet());

        var result =
            await Functions.CreateComparatorSetAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }



    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.CreateSet())
            .Throws(new Exception());

        var result = await Functions
            .CreateComparatorSetAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}