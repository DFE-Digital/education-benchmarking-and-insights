using System.IO.Pipelines;
using System.Text;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Primitives;
using Moq;

namespace EducationBenchmarking.Platform.Tests;

public class FunctionsTestBase
{
     public async Task<HttpRequest> CreateRequestWithBody(string body, Dictionary<String, StringValues> query = null)
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
        
    public HttpRequest CreateRequestWithBody(object item, Dictionary<String, StringValues> query = null)
    {
        return CreateRequestWithBody(item.ToJson(), query).Result;
    }

        
    public HttpRequest CreateRequest(Dictionary<String, StringValues> query = null, Stream body = null)
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