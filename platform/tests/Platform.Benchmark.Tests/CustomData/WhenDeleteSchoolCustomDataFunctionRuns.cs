using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.CustomData;
using Platform.Api.Benchmark.Features.CustomData.Models;
using Platform.Api.Benchmark.Features.CustomData.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.CustomData;

public class WhenDeleteSchoolCustomDataFunctionRuns : FunctionsTestBase
{
    private readonly DeleteSchoolCustomDataFunction _functions;
    private readonly Mock<ICustomDataService> _service = new();

    public WhenDeleteSchoolCustomDataFunctionRuns()
    {
        _functions = new DeleteSchoolCustomDataFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var data = new CustomDataSchool();
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);

        _service
            .Setup(d => d.CustomDataSchoolAsync(urn, identifier))
            .ReturnsAsync(data)
            .Verifiable();

        _service
            .Setup(d => d.DeleteSchoolAsync(data))
            .Verifiable();

        var result = await _functions.RunAsync(CreateHttpRequestData(), urn, identifier);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        _service.Verify();
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        CustomDataSchool? data = null;
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);

        _service
            .Setup(d => d.CustomDataSchoolAsync(urn, identifier))
            .ReturnsAsync(data)
            .Verifiable();

        var result = await _functions.RunAsync(CreateHttpRequestData(), urn, identifier);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        _service.Verify();
    }
}