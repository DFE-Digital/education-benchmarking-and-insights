using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.ComparatorSets;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets;

public class WhenDeleteTrustUserDefinedComparatorSetRuns : FunctionsTestBase
{
    private readonly DeleteTrustUserDefinedComparatorSetFunction _function;
    private readonly Mock<IComparatorSetsService> _service = new();

    public WhenDeleteTrustUserDefinedComparatorSetRuns()
    {
        _function = new DeleteTrustUserDefinedComparatorSetFunction(_service.Object);
    }

    [Fact]
    public async Task RemoveUserDefinedTrustShouldRemoveSuccessfully()
    {
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);
        const string runType = "default";

        _service
            .Setup(d => d.UserDefinedTrustAsync(urn, identifier, runType))
            .ReturnsAsync(new ComparatorSetUserDefinedTrust());

        _service
            .Setup(d => d.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        var response =
            await _function.RunAsync(CreateHttpRequestData(), urn, identifier);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _service.Verify(
            x => x.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Once);
    }

    [Fact]
    public async Task RemoveUserDefinedTrustShouldBeNotFound()
    {
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);
        const string runType = "default";

        _service
            .Setup(d => d.UserDefinedTrustAsync(urn, identifier, runType))
            .ReturnsAsync((ComparatorSetUserDefinedTrust?)null);

        _service
            .Setup(d => d.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        var response =
            await _function.RunAsync(CreateHttpRequestData(), urn, identifier);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        _service.Verify(
            x => x.DeleteTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Never);
    }
}