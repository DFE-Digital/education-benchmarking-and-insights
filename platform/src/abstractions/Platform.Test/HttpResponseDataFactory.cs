using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using Platform.Test.Mocks;

namespace Platform.Test;

public static class HttpResponseDataFactory
{
    public static HttpResponseData Create(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new TestHttpResponseData(statusCode);
    }

    private sealed class TestHttpResponseData : HttpResponseData
    {
        private static readonly Mock<FunctionContext> FuncContext = new Mock<FunctionContext>();

        public TestHttpResponseData(HttpStatusCode statusCode) : base(FuncContext.Object)
        {
            Body = new MemoryStream();
            Headers = [];
        }

        public override HttpStatusCode StatusCode { get; set; }
        public override HttpHeadersCollection Headers { get; set; }
        public override Stream Body { get; set; }
        public override HttpCookies Cookies { get; } = new MockHttpCookies();
    }
}