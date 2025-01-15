using System.Net;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Xunit;
namespace Platform.Benchmark.Tests;

public class WhenFunctionReceivesRemoveComparatorSetRequest : ComparatorSetsFunctionsTestBase
{
    [Fact]
    public async Task RemoveUserDefinedShouldRemoveSuccessfully()
    {
        ComparatorSetsService
            .Setup(
                d => d.UserDefinedSchoolAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetUserDefinedSchool());

        ComparatorSetsService
            .Setup(
                d => d.DeleteSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await Functions.RemoveUserDefinedSchoolComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        ComparatorSetsService.Verify(
            x => x.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
    }

    [Fact]
    public async Task RemoveUserDefinedShouldBeNotFoundWhenInvalid()
    {
        ComparatorSetsService
            .Setup(
                d => d.UserDefinedSchoolAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((ComparatorSetUserDefinedSchool?)null);

        ComparatorSetsService
            .Setup(
                d => d.DeleteSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await Functions.RemoveUserDefinedSchoolComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        ComparatorSetsService.Verify(
            x => x.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Never());
    }

    [Fact]
    public async Task RemoveUserDefinedShouldBe500OnError()
    {
        ComparatorSetsService
            .Setup(
                d => d.UserDefinedSchoolAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        ComparatorSetsService
            .Setup(
                d => d.DeleteSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await Functions.RemoveUserDefinedSchoolComparatorSetAsync(CreateHttpRequestData(), "12313",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task RemoveUserDefinedTrustShouldRemoveSuccessfully()
    {
        ComparatorSetsService
            .Setup(
                d => d.UserDefinedTrustAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetUserDefinedTrust());

        ComparatorSetsService
            .Setup(
                d => d.DeleteTrustAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>()));

        var response =
            await Functions.RemoveUserDefinedTrustComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        ComparatorSetsService.Verify(
            x => x.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Once);
    }

    [Fact]
    public async Task RemoveUserDefinedTrustShouldBeNotFound()
    {
        ComparatorSetsService
            .Setup(
                d => d.UserDefinedTrustAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((ComparatorSetUserDefinedTrust?)null);

        ComparatorSetsService
            .Setup(
                d => d.DeleteTrustAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>()));

        var response =
            await Functions.RemoveUserDefinedTrustComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        ComparatorSetsService.Verify(
            x => x.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Never);
    }

    [Fact]
    public async Task RemoveUserDefinedTrustShouldError()
    {
        ComparatorSetsService
            .Setup(
                d => d.UserDefinedTrustAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        ComparatorSetsService
            .Setup(
                d => d.DeleteTrustAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>()));

        var response =
            await Functions.RemoveUserDefinedTrustComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        ComparatorSetsService.Verify(
            x => x.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Never);
    }
}