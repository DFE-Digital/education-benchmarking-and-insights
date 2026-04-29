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

public class QueryLocalAuthoritiesV1HandlerTests : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ILocalAuthorityDetailsService> _service = new();
    private readonly QueryLocalAuthoritiesV1Handler _handler;

    public QueryLocalAuthoritiesV1HandlerTests()
    {
        _handler = new QueryLocalAuthoritiesV1Handler(_service.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var response = _fixture.CreateMany<LocalAuthorityResponse>().ToList();
        var request = MockHttpRequestData.Create();
        var context = new BasicContext(request, CancellationToken.None);

        _service.Setup(s => s.QueryAsync(context.Token)).ReturnsAsync(response);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<IEnumerable<LocalAuthorityResponse>>();
        Assert.NotNull(body);
        Assert.Equal(response.Count, body.Count());
    }
}