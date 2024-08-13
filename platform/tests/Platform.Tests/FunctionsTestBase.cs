using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Primitives;
using Platform.Tests.Mocks;
namespace Platform.Tests;

public class FunctionsTestBase
{
    protected static HttpRequestData CreateHttpRequestDataWithBody<T>(T item, Dictionary<string, StringValues>? query = null) where T : class
    {
        var reqMock = MockHttpRequestData.Create(item);
        return reqMock;
    }

    protected static HttpRequestData CreateHttpRequestData(Dictionary<string, StringValues>? query = null)
    {
        var reqMock = MockHttpRequestData.Create();
        return reqMock;
    }
}