using Microsoft.Azure.Functions.Worker;
using Moq;
using Platform.Api.Content.Features.Files;
using Platform.Api.Content.Features.Files.Handlers;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Content.Tests.Files.Functions;

public class GetTransparencyFilesFunctionTests : FunctionsTestBase
{
    private readonly Mock<IVersionedHandlerDispatcher<IGetTransparencyFilesHandler>> _dispatcher;
    private readonly GetTransparencyFilesFunction _function;
    private readonly Mock<IGetTransparencyFilesHandler> _handler;

    public GetTransparencyFilesFunctionTests()
    {
        _handler = new Mock<IGetTransparencyFilesHandler>();
        _dispatcher = new Mock<IVersionedHandlerDispatcher<IGetTransparencyFilesHandler>>();
        _function = new GetTransparencyFilesFunction(_dispatcher.Object);
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