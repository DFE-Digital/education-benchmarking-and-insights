using Microsoft.Azure.Functions.Worker;
using Moq;
using Platform.Api.Content.Features.News;
using Platform.Api.Content.Features.News.Handlers;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Content.Tests.News.Functions;

public class GetNewsArticleFunctionTests : FunctionsTestBase
{
    private readonly Mock<IVersionedHandlerDispatcher<IGetNewsArticleHandler>> _dispatcher;
    private readonly GetNewsArticleFunction _function;
    private readonly Mock<IGetNewsArticleHandler> _handler;

    public GetNewsArticleFunctionTests()
    {
        _handler = new Mock<IGetNewsArticleHandler>();
        _dispatcher = new Mock<IVersionedHandlerDispatcher<IGetNewsArticleHandler>>();
        _function = new GetNewsArticleFunction(_dispatcher.Object);
    }

    [Theory]
    [InlineData("version")]
    [InlineData(null)]
    public async Task ShouldReturnHandlerResponseOnValidOrMissingVersion(string? version = null)
    {
        const string slug = nameof(slug);

        var response = new MockHttpResponseData(Mock.Of<FunctionContext>());

        var request = CreateHttpRequestData(null, CreateVersionedHeader(version));
        _dispatcher
            .Setup(d => d.GetHandler(version))
            .Returns(_handler.Object);
        _handler
            .Setup(h => h.HandleAsync(request, slug, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _function.RunAsync(request, slug);

        Assert.Equal(response, result);
    }
}