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

public class GetNewsArticleV1HandlerTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetNewsArticleV1Handler _handler;
    private readonly Mock<INewsService> _service;

    public GetNewsArticleV1HandlerTests()
    {
        _service = new Mock<INewsService>();
        _fixture = new Fixture();
        _handler = new GetNewsArticleV1Handler(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        const string slug = nameof(slug);

        var model = _fixture.Create<Api.Content.Features.News.Models.News>();

        _service
            .Setup(d => d.GetNewsArticleOrDefault(slug, It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var result = await _handler.HandleAsync(CreateHttpRequestData(), slug, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<Api.Content.Features.News.Models.News>();
        Assert.NotNull(body);
    }
}