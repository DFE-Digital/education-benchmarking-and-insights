using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Content.Features.News.Handlers;
using Platform.Api.Content.Features.News.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Content.Tests.News.Handlers;

public class GetNewsV1HandlerTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetNewsV1Handler _handler;
    private readonly Mock<INewsService> _service;

    public GetNewsV1HandlerTests()
    {
        _service = new Mock<INewsService>();
        _fixture = new Fixture();
        _handler = new GetNewsV1Handler(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var models = _fixture.Build<Api.Content.Features.News.Models.News>().CreateMany().ToArray();

        _service
            .Setup(d => d.GetNews(It.IsAny<CancellationToken>()))
            .ReturnsAsync(models);

        var result = await _handler.HandleAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<Api.Content.Features.News.Models.News[]>();
        Assert.NotNull(body);
    }
}