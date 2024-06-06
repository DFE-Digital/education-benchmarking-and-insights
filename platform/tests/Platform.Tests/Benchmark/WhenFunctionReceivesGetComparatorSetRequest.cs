using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesGetComparatorSetRequest : ComparatorSetsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.DefaultSchoolAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetDefaultSchool());

        var result =
            await Functions.DefaultSchoolComparatorSetAsync(CreateRequest(), "12313") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }



    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.DefaultSchoolAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions
            .DefaultSchoolComparatorSetAsync(CreateRequest(), "12313") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}