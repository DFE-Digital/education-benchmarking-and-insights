using AutoFixture;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Functions;
using Platform.Test.Mocks;

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

/*public abstract class VersionedFunctionTestBase<THandler> : FunctionsTestBase where THandler : class, IVersionedHandler
{
    protected readonly Mock<IVersionedHandlerDispatcher<THandler>> Dispatcher;
    protected readonly Mock<THandler> Handler;
    protected readonly Fixture Fixture = new();

    protected VersionedFunctionTestBase()
    {
        Handler = new Mock<THandler>();
        Dispatcher = new Mock<IVersionedHandlerDispatcher<THandler>>();

        Dispatcher
            .Setup(d => d.GetHandler("1.0"))
            .Returns(Handler.Object);

        Dispatcher
            .Setup(d => d.GetHandler("9.9"))
            .Returns((THandler?)null);

        Dispatcher
            .Setup(d => d.GetHandler(null))
            .Returns(Handler.Object);
    }
}*/