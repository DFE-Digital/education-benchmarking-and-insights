using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain.Responses;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesDefaultPupilComparatorSetRequest : ComparatorSetFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSet());

        var result =
            await Functions.DefaultPupilComparatorSetAsync(CreateRequest(), "12313") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }



    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions
            .DefaultPupilComparatorSetAsync(CreateRequest(), "12313") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}