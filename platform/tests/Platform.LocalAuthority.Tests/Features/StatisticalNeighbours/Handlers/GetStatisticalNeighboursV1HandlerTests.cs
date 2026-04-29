using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Handlers;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Models;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.StatisticalNeighbours.Handlers;

public class GetStatisticalNeighboursV1HandlerTests : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IStatisticalNeighboursService> _service = new();
    private readonly GetStatisticalNeighboursV1Handler _handler;

    public GetStatisticalNeighboursV1HandlerTests()
    {
        _handler = new GetStatisticalNeighboursV1Handler(_service.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var data = _fixture.CreateMany<StatisticalNeighbourDto>().ToArray();
        var id = _fixture.Create<string>();
        var token = CancellationToken.None;
        var request = CreateHttpRequestData();
        var context = new IdContext(request, token, id);

        _service
            .Setup(s => s.GetAsync(id, token))
            .ReturnsAsync(data);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<StatisticalNeighboursResponse>();
        Assert.NotNull(body);
        Assert.Equal(data[0].LaCode, body.Code);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        var id = _fixture.Create<string>();
        var token = CancellationToken.None;
        var request = CreateHttpRequestData();
        var context = new IdContext(request, token, id);

        _service
            .Setup(s => s.GetAsync(id, token))
            .ReturnsAsync(Enumerable.Empty<StatisticalNeighbourDto>());

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}