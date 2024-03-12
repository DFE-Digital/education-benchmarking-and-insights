using System.IO.Pipelines;
using System.Text;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Functions.Extensions;

namespace Platform.Tests;

public class FunctionsTestBase
{
    private static async Task<HttpRequest> CreateRequestWithBody(string body, Dictionary<string, StringValues>? query = null)
    {
        var reqMock = new Mock<HttpRequest>();

        reqMock.Setup(req => req.Query).Returns(new QueryCollection(query ?? new Dictionary<string, StringValues>()));
        reqMock.Setup(req => req.Headers).Returns(new HeaderDictionary());
        reqMock.Setup(req => req.Scheme).Returns("https");
        reqMock.Setup(req => req.Host).Returns(new HostString("localhost"));

        var pipe = new Pipe();
        await pipe.Writer.WriteAsync(Encoding.UTF8.GetBytes(body).AsMemory());
        await pipe.Writer.CompleteAsync();

        reqMock.Setup(req => req.HttpContext.RequestAborted).Returns(new CancellationToken());
        reqMock.Setup(req => req.BodyReader).Returns(pipe.Reader);

        return reqMock.Object;
    }

    protected static HttpRequest CreateRequestWithBody(object item, Dictionary<string, StringValues>? query = null)
    {
        return CreateRequestWithBody(item.ToJson(), query).Result;
    }

    protected static HttpRequest CreateRequest(Dictionary<string, StringValues>? query = null, Stream? body = null)
    {
        var reqMock = new Mock<HttpRequest>();
        reqMock.Setup(req => req.Headers).Returns(new HeaderDictionary());
        reqMock.Setup(req => req.Query).Returns(new QueryCollection(query ?? new Dictionary<string, StringValues>()));
        reqMock.Setup(req => req.Scheme).Returns("https");
        reqMock.Setup(req => req.Host).Returns(new HostString("localhost"));

        if (body != null)
        {
            reqMock.Setup(req => req.Body).Returns(body);
            reqMock.Setup(req => req.BodyReader).Returns(PipeReader.Create(body));
        }

        return reqMock.Object;
    }

    public static HttpRequest CreateMultipartRequest(FormCollection formCollection)
    {
        var context = new DefaultHttpContext();

        var request = context.Request;
        request.Method = "POST";
        request.ContentType = "multipart/form-data";
        request.Form = formCollection;

        return request;
    }

    public static IFormFile CreateFormFile(string keyname, Stream stream, string contentType, string fileName)
    {
        return new FormFile(stream, 0, stream.Length, keyname, fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };
    }
}