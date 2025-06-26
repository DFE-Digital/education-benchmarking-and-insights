using Microsoft.Azure.Functions.Worker;
using Moq;
using Platform.Api.Content.Features.Banners;
using Platform.Api.Content.Features.Banners.Handlers;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Content.Tests.Banners.Functions;

public class GetBannerFunctionTests : FunctionsTestBase
{
    private readonly Mock<IVersionedHandlerDispatcher<IGetBannerHandler>> _dispatcher;
    private readonly GetBannerFunction _function;
    private readonly Mock<IGetBannerHandler> _handler;

    public GetBannerFunctionTests()
    {
        _handler = new Mock<IGetBannerHandler>();
        _dispatcher = new Mock<IVersionedHandlerDispatcher<IGetBannerHandler>>();
        _function = new GetBannerFunction(_dispatcher.Object);
    }

    [Theory]
    [InlineData("version")]
    [InlineData(null)]
    public async Task ShouldReturnHandlerResponseOnValidOrMissingVersion(string? version = null)
    {
        const string target = nameof(target);

        var response = new MockHttpResponseData(Mock.Of<FunctionContext>());

        var request = CreateHttpRequestData(null, CreateVersionedHeader(version));
        _dispatcher
            .Setup(d => d.GetHandler(version))
            .Returns(_handler.Object);
        _handler
            .Setup(h => h.HandleAsync(request, target, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _function.RunAsync(request, target);

        Assert.Equal(response, result);
    }
}