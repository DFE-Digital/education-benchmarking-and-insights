using System.Net;
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
        _function = new GetSchoolCustomComparatorSetFunction(_service.Object);
    }

    [Fact]
    public async Task CustomShouldBeOkOnValidRequest()
    {
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);

        _service
            .Setup(d => d.CustomSchoolAsync(identifier, urn, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ComparatorSetSchool());

        var response =
            await _function.RunAsync(CreateHttpRequestData(), urn, identifier);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}