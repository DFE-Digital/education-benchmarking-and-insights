using System.Net;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark.Features.ComparatorSets;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets;

public class WhenGetSchoolCustomComparatorSetRuns : FunctionsTestBase
{
    private readonly GetSchoolCustomComparatorSetFunction _function;
    private readonly Mock<IComparatorSetsService> _service = new();

    public WhenGetSchoolCustomComparatorSetRuns()
    {
        _function = new GetSchoolCustomComparatorSetFunction(_service.Object, new NullLogger<GetSchoolCustomComparatorSetFunction>());
    }

    [Fact]
    public async Task CustomShouldBeOkOnValidRequest()
    {
        _service
            .Setup(d => d.CustomSchoolAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetSchool());

        var response =
            await _function.RunAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CustomShouldBe500OnError()
    {
        _service
            .Setup(d => d.CustomSchoolAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var response = await _function
            .RunAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}