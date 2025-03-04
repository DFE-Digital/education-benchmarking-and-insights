using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities;

public class GetLocalAuthorityStatisticalNeighboursFunctionTests : FunctionsTestBase
{
    private readonly GetLocalAuthorityStatisticalNeighboursFunction _function;
    private readonly string _laCode;
    private readonly LocalAuthorityStatisticalNeighbours _neighbours;
    private readonly Mock<ILocalAuthoritiesService> _service;

    public GetLocalAuthorityStatisticalNeighboursFunctionTests()
    {
        _service = new Mock<ILocalAuthoritiesService>();
        _function = new GetLocalAuthorityStatisticalNeighboursFunction(_service.Object);

        var fixture = new Fixture();
        _laCode = fixture.Create<string>();
        _neighbours = fixture
            .Build<LocalAuthorityStatisticalNeighbours>()
            .With(l => l.Code, _laCode)
            .Create();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.GetStatisticalNeighboursAsync(_laCode))
            .ReturnsAsync(_neighbours);

        var result = await _function.RunAsync(CreateHttpRequestData(), _laCode);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<LocalAuthorityStatisticalNeighbours>();
        Assert.NotNull(body);
        Assert.Equal(_laCode, body.Code);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        _service
            .Setup(d => d.GetAsync(_laCode))
            .ReturnsAsync((LocalAuthority?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), _laCode);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}