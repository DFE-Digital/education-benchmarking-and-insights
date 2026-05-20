using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.LocalAuthority.Features.Details.Handlers;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Details.Handlers;

public class WhenGetLocalAuthorityV1HandlerHandles : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ILocalAuthorityDetailsService> _service = new();
    private readonly GetLocalAuthorityV1Handler _handler;

    public WhenGetLocalAuthorityV1HandlerHandles()
    {
        _handler = new GetLocalAuthorityV1Handler(_service.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var id = "LA1";
        var response = _fixture.Create<LocalAuthorityResponse>();
        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, CancellationToken.None, id);

        _service.Setup(s => s.GetAsync(id, context.Token)).ReturnsAsync(response);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<LocalAuthorityResponse>();
        Assert.NotNull(body);
        Assert.Equal(response.Code, body.Code);
    }

    [Fact]
    public async Task ShouldReturn404WhenNotFound()
    {
        var id = "LA1";
        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, CancellationToken.None, id);

        _service.Setup(s => s.GetAsync(id, context.Token)).ReturnsAsync((LocalAuthorityResponse?)null);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
