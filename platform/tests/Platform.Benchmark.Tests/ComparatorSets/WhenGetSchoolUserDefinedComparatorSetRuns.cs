using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.ComparatorSets;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets;

public class WhenGetSchoolUserDefinedComparatorSetRuns : FunctionsTestBase
{
    private readonly GetSchoolUserDefinedComparatorSetFunction _function;
    private readonly Mock<IComparatorSetsService> _service = new();

    public WhenGetSchoolUserDefinedComparatorSetRuns()
    {
        _function = new GetSchoolUserDefinedComparatorSetFunction(_service.Object);
    }

    [Fact]
    public async Task UserDefinedShouldBeOkOnValidRequest()
    {
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);
        const string runType = "default";

        _service
            .Setup(d => d.UserDefinedSchoolAsync(urn, identifier, runType, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ComparatorSetUserDefinedSchool());

        var response =
            await _function.RunAsync(CreateHttpRequestData(), urn, identifier);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}