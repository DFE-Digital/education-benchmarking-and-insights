using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Primitives;
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
}