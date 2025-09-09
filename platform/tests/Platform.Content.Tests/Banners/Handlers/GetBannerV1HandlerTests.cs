using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Content.Features.Banners.Handlers;
using Platform.Api.Content.Features.Banners.Models;
using Platform.Api.Content.Features.Banners.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Content.Tests.Banners.Handlers;

public class GetBannerV1HandlerTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetBannerV1Handler _handler;
    private readonly Mock<IBannersService> _service;

    public GetBannerV1HandlerTests()
    {
        _service = new Mock<IBannersService>();
        _fixture = new Fixture();
        _handler = new GetBannerV1Handler(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        const string target = nameof(target);

        var model = _fixture.Create<Banner>();

        _service
            .Setup(d => d.GetBannerOrDefault(target, It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var result = await _handler.HandleAsync(CreateHttpRequestData(), target, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<Banner>();
        Assert.NotNull(body);
    }
}