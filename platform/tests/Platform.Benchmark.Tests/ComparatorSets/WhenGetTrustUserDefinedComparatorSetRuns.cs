using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.ComparatorSets;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets;

public class WhenGetTrustUserDefinedComparatorSetRuns : FunctionsTestBase
{
    private readonly GetTrustUserDefinedComparatorSetFunction _function;
    private readonly Mock<IComparatorSetsService> _service = new();

    public WhenGetTrustUserDefinedComparatorSetRuns()
    {
        _function = new GetTrustUserDefinedComparatorSetFunction(_service.Object);
    }

    [Fact]
    public async Task UserDefinedTrustShouldBeOkOnValidRequest()
    {
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);
        const string runType = "default";

        _service
            .Setup(d => d.UserDefinedTrustAsync(urn, identifier, runType))
            .ReturnsAsync(new ComparatorSetUserDefinedTrust());

        var response =
            await _function.RunAsync(CreateHttpRequestData(), urn,
                identifier);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UserDefinedTrustShouldBeNotFoundOnInvalidRequest()
    {
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);
        const string runType = "default";

        _service
            .Setup(d => d.UserDefinedTrustAsync(urn, identifier, runType))
            .ReturnsAsync((ComparatorSetUserDefinedTrust?)null);

        var response =
            await _function.RunAsync(CreateHttpRequestData(), urn,
                identifier);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}