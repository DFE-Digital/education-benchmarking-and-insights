using System.Net;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesRemoveComparatorSetRequest : ComparatorSetsFunctionsTestBase
{
    [Fact]
    public async Task RemoveUserDefinedShouldRemoveSuccessfully()
    {
        Service
            .Setup(
                d => d.UserDefinedSchoolAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetUserDefinedSchool());

        Service
            .Setup(
                d => d.DeleteSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await Functions.RemoveUserDefinedSchoolComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Service.Verify(
            x => x.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
    }

    [Fact]
    public async Task RemoveUserDefinedShouldBeNotFoundWhenInvalid()
    {
        Service
            .Setup(
                d => d.UserDefinedSchoolAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((ComparatorSetUserDefinedSchool?)null);

        Service
            .Setup(
                d => d.DeleteSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        var response =
            await Functions.RemoveUserDefinedSchoolComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Service.Verify(
            x => x.DeleteSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Never());
    }

    [Fact]
    public async Task RemoveUserDefinedShouldBe500OnError()
    {
        Service
            .Setup(
                d => d.UserDefinedSchoolAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        Service
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
        Service
            .Setup(
                d => d.UserDefinedTrustAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetUserDefinedTrust());

        Service
            .Setup(
                d => d.DeleteTrustAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>()));

        var response =
            await Functions.RemoveUserDefinedTrustComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Service.Verify(
            x => x.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Once);
    }

    [Fact]
    public async Task RemoveUserDefinedTrustShouldBeNotFound()
    {
        Service
            .Setup(
                d => d.UserDefinedTrustAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((ComparatorSetUserDefinedTrust?)null);

        Service
            .Setup(
                d => d.DeleteTrustAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>()));

        var response =
            await Functions.RemoveUserDefinedTrustComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Service.Verify(
            x => x.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Never);
    }

    [Fact]
    public async Task RemoveUserDefinedTrustShouldError()
    {
        Service
            .Setup(
                d => d.UserDefinedTrustAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        Service
            .Setup(
                d => d.DeleteTrustAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>()));

        var response =
            await Functions.RemoveUserDefinedTrustComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Service.Verify(
            x => x.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Never);
    }
}