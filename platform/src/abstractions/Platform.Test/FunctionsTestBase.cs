using System.Net;
using System.Reflection;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Functions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Test;

public class FunctionsTestBase
{
    protected static HttpRequestData CreateHttpRequestDataWithBody<T>(T item, Dictionary<string, StringValues>? query = null) where T : class
    {
        var reqMock = MockHttpRequestData.Create(item, query);
        return reqMock;
    }

    protected static HttpRequestData CreateHttpRequestData(Dictionary<string, StringValues>? query = null, HttpHeadersCollection? headers = null)
    {
        var reqMock = MockHttpRequestData.Create(query, headers);
        return reqMock;
    }

    protected static HttpHeadersCollection CreateVersionedHeader(string? version)
    {
        if (string.IsNullOrWhiteSpace(version))
        {
            return [];
        }

        var kvp = new KeyValuePair<string, string>("x-api-version", version);
        return new HttpHeadersCollection([kvp]);
    }
}

public abstract class FunctionRunAsyncReflectionTestsBase<TFunction, THandler, TContext> : FunctionsTestBase
    where TFunction : class
    where THandler : class, IVersionedHandler<TContext>
    where TContext : HandlerContext
{
    protected abstract TFunction CreateFunction(IEnumerable<THandler> handlers);

    protected abstract object[] GetRunAsyncArguments(HttpRequestData request);

    private static Mock<THandler> CreateHandler(string version, HttpResponseData response)
    {
        var mock = new Mock<THandler>();
        mock.SetupGet(h => h.Version).Returns(version);
        mock.Setup(h => h.HandleAsync(It.IsAny<TContext>()))
            .ReturnsAsync(response);
        return mock;
    }

    private static MethodInfo GetRunAsyncMethod()
    {
        var method = typeof(TFunction)
            .GetMethods()
            .Single(m => m.Name == "RunAsync" && typeof(Task<HttpResponseData>).IsAssignableFrom(m.ReturnType));

        return method;
    }

    [Fact]
    public async Task RunAsync_NoVersion_UsesLatestHandler()
    {
        var request = CreateHttpRequestData();
        var expectedResponse = HttpResponseDataFactory.Create();

        var v1Handler = CreateHandler("1.0", HttpResponseDataFactory.Create());
        var v2Handler = CreateHandler("2.0", expectedResponse);

        var function = CreateFunction([v1Handler.Object, v2Handler.Object]);
        var args = GetRunAsyncArguments(request);
        var method = GetRunAsyncMethod();

        var task = (Task<HttpResponseData>)method.Invoke(function, args)!;
        var response = await task;

        Assert.Same(expectedResponse, response);
        v2Handler.Verify(h => h.HandleAsync(It.IsAny<TContext>()), Times.Once);
        v1Handler.Verify(h => h.HandleAsync(It.IsAny<TContext>()), Times.Never);
    }

    [Fact]
    public async Task RunAsync_UnsupportedVersion_ReturnsUnsupportedVersionResponse()
    {
        var kvp = new KeyValuePair<string, string>("x-api-version", "99.0");
        var request = CreateHttpRequestData(null, new HttpHeadersCollection([kvp]));

        var handler = CreateHandler("1.0", HttpResponseDataFactory.Create());
        var function = CreateFunction([handler.Object]);
        var args = GetRunAsyncArguments(request);
        var method = GetRunAsyncMethod();

        var task = (Task<HttpResponseData>)method.Invoke(function, args)!;
        var response = await task;

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        handler.Verify(h => h.HandleAsync(It.IsAny<TContext>()), Times.Never);
    }

    [Fact]
    public async Task RunAsync_HandlerResponse_IsReturnedUnmodified()
    {
        var kvp = new KeyValuePair<string, string>("x-api-version", "1.0");
        var request = CreateHttpRequestData(null, new HttpHeadersCollection([kvp]));
        var expectedResponse = HttpResponseDataFactory.Create();

        var handler = CreateHandler("1.0", expectedResponse);
        var function = CreateFunction([handler.Object]);
        var args = GetRunAsyncArguments(request);
        var method = GetRunAsyncMethod();

        var task = (Task<HttpResponseData>)method.Invoke(function, args)!;
        var response = await task;

        Assert.Same(expectedResponse, response);
        handler.Verify(h => h.HandleAsync(It.IsAny<TContext>()), Times.Once);
    }
}