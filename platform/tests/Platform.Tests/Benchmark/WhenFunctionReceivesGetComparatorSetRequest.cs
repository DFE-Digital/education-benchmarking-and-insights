using System.Net;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Xunit;
namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesGetComparatorSetRequest : ComparatorSetsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.DefaultSchoolAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetSchool());

        var result =
            await Functions.DefaultSchoolComparatorSetAsync(CreateHttpRequestData(), "12313");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.DefaultSchoolAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions
            .DefaultSchoolComparatorSetAsync(CreateHttpRequestData(), "12313");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}