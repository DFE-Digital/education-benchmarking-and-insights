using Microsoft.Azure.Functions.Worker;
using Moq;
using Platform.Api.Content.Features.Years;
using Platform.Api.Content.Features.Years.Handlers;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Content.Tests.Years.Functions;

public class GetCurrentReturnYearsFunctionTests : FunctionsTestBase
{
    private readonly Mock<IVersionedHandlerDispatcher<IGetCurrentReturnYearsHandler>> _dispatcher;
    private readonly GetCurrentReturnYearsFunction _function;
    private readonly Mock<IGetCurrentReturnYearsHandler> _handler;

    public GetCurrentReturnYearsFunctionTests()
    {
        _handler = new Mock<IGetCurrentReturnYearsHandler>();
        _dispatcher = new Mock<IVersionedHandlerDispatcher<IGetCurrentReturnYearsHandler>>();
        _function = new GetCurrentReturnYearsFunction(_dispatcher.Object);
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

        var result = await _function.RunAsync(request, CancellationToken.None);

        Assert.Equal(response, result);
    }
}