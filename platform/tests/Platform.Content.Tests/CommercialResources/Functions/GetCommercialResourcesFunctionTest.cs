using Microsoft.Azure.Functions.Worker;
using Moq;
using Platform.Api.Content.Features.CommercialResources;
using Platform.Api.Content.Features.CommercialResources.Handlers;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Content.Tests.CommercialResources.Functions;

public class GetCommercialResourcesFunctionTests : FunctionsTestBase
{
    private readonly Mock<IVersionedHandlerDispatcher<IGetCommercialResourcesHandler>> _dispatcher;
    private readonly GetCommercialResourcesFunction _function;
    private readonly Mock<IGetCommercialResourcesHandler> _handler;

    public GetCommercialResourcesFunctionTests()
    {
        _handler = new Mock<IGetCommercialResourcesHandler>();
        _dispatcher = new Mock<IVersionedHandlerDispatcher<IGetCommercialResourcesHandler>>();
        _function = new GetCommercialResourcesFunction(_dispatcher.Object);
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