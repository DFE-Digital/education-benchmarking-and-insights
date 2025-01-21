using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Census;
using Platform.Api.Insight.Features.Census.Models;
using Platform.Api.Insight.Features.Census.Responses;
using Platform.Api.Insight.Features.Census.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Census;

public class GetCensusFunctionTests : FunctionsTestBase
{
    private const string Urn = "URN";
    private readonly GetCensusFunction _function;
    private readonly Mock<ICensusService> _service;
    private readonly Fixture _fixture;

    public GetCensusFunctionTests()
    {
        _service = new Mock<ICensusService>();
        _fixture = new Fixture();
        _function = new GetCensusFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<CensusSchoolModel>().Create();

        _service
            .Setup(d => d.GetAsync(Urn))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData(), Urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<CensusSchoolResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _service
            .Setup(d => d.GetAsync(Urn))
            .ReturnsAsync((CensusSchoolModel?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), Urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}