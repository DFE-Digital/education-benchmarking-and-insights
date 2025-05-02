using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.ComparatorSets;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets;

public class WhenDeleteSchoolUserDefinedComparatorSetRuns : FunctionsTestBase
{
    private readonly DeleteSchoolUserDefinedComparatorSetFunction _function;
    private readonly Mock<IComparatorSetsService> _service = new();

    public WhenDeleteSchoolUserDefinedComparatorSetRuns()
    {
        _function = new DeleteSchoolUserDefinedComparatorSetFunction(_service.Object);
    }

    [Fact]
    public async Task RemoveUserDefinedShouldRemoveSuccessfully()
    {
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);
        const string runType = "default";

        _service
            .Setup(d => d.UserDefinedSchoolAsync(urn, identifier, runType, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ComparatorSetUserDefinedSchool());

        _service
            .Setup(d => d.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await _function.RunAsync(CreateHttpRequestData(), urn, identifier);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _service.Verify(
            x => x.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
    }

    [Fact]
    public async Task RemoveUserDefinedShouldBeNotFoundWhenInvalid()
    {
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);
        const string runType = "default";

        _service
            .Setup(d => d.UserDefinedSchoolAsync(urn, identifier, runType, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ComparatorSetUserDefinedSchool?)null);

        _service
            .Setup(d => d.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await _function.RunAsync(CreateHttpRequestData(), urn, identifier);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        _service.Verify(
            x => x.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Never());
    }
}