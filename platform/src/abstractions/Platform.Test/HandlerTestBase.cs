using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Primitives;
using Platform.Test.Mocks;

namespace Platform.Test;

public class HandlerTestBase
{
    protected static HttpRequestData CreateHttpRequestData()
    {
        var reqMock = MockHttpRequestData.Create();
        return reqMock;
    }
}