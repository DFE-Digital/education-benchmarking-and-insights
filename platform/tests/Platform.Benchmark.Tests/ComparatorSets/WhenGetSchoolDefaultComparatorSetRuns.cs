using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.ComparatorSets;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets;

public class WhenGetSchoolDefaultComparatorSetRuns : FunctionsTestBase
{
    private readonly GetSchoolDefaultComparatorSetFunction _function;
    private readonly Mock<IComparatorSetsService> _service = new();

    public WhenGetSchoolDefaultComparatorSetRuns()
    {
        _function = new GetSchoolDefaultComparatorSetFunction(_service.Object);
    }

    [Fact]
    public async Task DefaultShouldBeOkOnValidRequest()
    {
        _service
            .Setup(d => d.DefaultSchoolAsync(It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetSchool());

        var response =
            await _function.RunAsync(CreateHttpRequestData(), "12313");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}