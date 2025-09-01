using Microsoft.Azure.Functions.Worker;
using Moq;
using Platform.Api.Content.Features.News;
using Platform.Api.Content.Features.News.Handlers;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Content.Tests.News.Functions;

public class GetNewsFunctionTests : FunctionsTestBase
{
    private readonly Mock<IVersionedHandlerDispatcher<IGetNewsHandler>> _dispatcher;
    private readonly GetNewsFunction _function;
    private readonly Mock<IGetNewsHandler> _handler;

    public GetNewsFunctionTests()
    {
        _handler = new Mock<IGetNewsHandler>();
        _dispatcher = new Mock<IVersionedHandlerDispatcher<IGetNewsHandler>>();
        _function = new GetNewsFunction(_dispatcher.Object);
    }

    [Theory]
    [InlineData("version")]
    [InlineData(null)]
    public async Task ShouldReturnHandlerResponseOnValidOrMissingVersion(string? version = null)
    {
        var response = new MockHttpResponseData(Mock.Of<FunctionContext>());

        var request = CreateHttpRequestData(null, CreateVersionedHeader(version));
        _dispatcher
            .Setup(d => d.GetHandler(version))
            .Returns(_handler.Object);
        _handler
            .Setup(h => h.HandleAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _function.RunAsync(request);

        Assert.Equal(response, result);
    }
}