using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Functions.Tests;

public class VersionedFunctionBaseTests
{
    // ReSharper disable once MemberCanBePrivate.Global
    public class TestHandler : IVersionedHandler
    {
        public string Version => "1.0";
    }

    private class TestFunction(IVersionedHandlerDispatcher<TestHandler> dispatcher) : VersionedFunctionBase<TestHandler>(dispatcher)
    {
        public Task<HttpResponseData> CallWithHandlerAsync(
            HttpRequestData request,
            Func<TestHandler, Task<HttpResponseData>> invoker,
            CancellationToken token)
        {
            return WithHandlerAsync(request, invoker, token);
        }
    }

    [Fact]
    public async Task WhenHandlerExists()
    {
        var handler = new TestHandler();
        var dispatcher = new Mock<IVersionedHandlerDispatcher<TestHandler>>();
        var kvp = new KeyValuePair<string, string>("x-api-version", "1.0");
        var request = MockHttpRequestData.Create(null, new HttpHeadersCollection([kvp]));
        var response = request.CreateResponse(HttpStatusCode.OK);
        var cancellationToken = CancellationToken.None;

        dispatcher
            .Setup(d => d.GetHandler("1.0"))
            .Returns(handler);

        var function = new TestFunction(dispatcher.Object);
        var result = await function.CallWithHandlerAsync(request, HandlerInvoker, cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        return;

        Task<HttpResponseData> HandlerInvoker(TestHandler h)
        {
            return Task.FromResult(response);
        }
    }

    [Fact]
    public async Task WhenHandlerNotExists()
    {
        var kvp = new KeyValuePair<string, string>("x-api-version", "9.9");
        var request = MockHttpRequestData.Create(null, new HttpHeadersCollection([kvp]));
        var dispatcher = new Mock<IVersionedHandlerDispatcher<TestHandler>>();
        var cancellationToken = CancellationToken.None;

        dispatcher
            .Setup(d => d.GetHandler("9.9"))
            .Returns((TestHandler?)null);

        var function = new TestFunction(dispatcher.Object);
        var result = await function.CallWithHandlerAsync(
            request,
            _ => throw new Exception("This should not be called"),
            cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}