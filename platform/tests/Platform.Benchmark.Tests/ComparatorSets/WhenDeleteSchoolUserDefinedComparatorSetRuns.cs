using System.Net;
using Microsoft.Extensions.Logging.Abstractions;
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
        _function = new DeleteSchoolUserDefinedComparatorSetFunction(_service.Object, new NullLogger<DeleteSchoolUserDefinedComparatorSetFunction>());
    }

    [Fact]
    public async Task RemoveUserDefinedShouldRemoveSuccessfully()
    {
        _service
            .Setup(d => d.UserDefinedSchoolAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetUserDefinedSchool());

        _service
            .Setup(d => d.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await _function.RunAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _service.Verify(
            x => x.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
    }

    [Fact]
    public async Task RemoveUserDefinedShouldBeNotFoundWhenInvalid()
    {
        _service
            .Setup(d => d.UserDefinedSchoolAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((ComparatorSetUserDefinedSchool?)null);

        _service
            .Setup(d => d.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await _function.RunAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        _service.Verify(
            x => x.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Never());
    }

    [Fact]
    public async Task RemoveUserDefinedShouldBe500OnError()
    {
        _service
            .Setup(d => d.UserDefinedSchoolAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        _service
            .Setup(d => d.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await _function.RunAsync(CreateHttpRequestData(), "12313",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}