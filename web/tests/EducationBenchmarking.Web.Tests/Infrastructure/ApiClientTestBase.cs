using System.Net;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Moq;
using Moq.Protected;

namespace EducationBenchmarking.Web.Tests.Infrastructure;

public class ApiClientTestBase
{
    private Mock<HttpMessageHandler>? _handlerMock;
    private HttpClient? _httpClient;

    protected Mock<HttpMessageHandler> HandlerMock => _handlerMock ?? throw new InvalidOperationException();
    protected HttpClient HttpClient => _httpClient ?? throw new InvalidOperationException();

    protected Uri Uri { get; set; } = new Uri("http://test.com/");

    protected void CreateHttpClient(HttpStatusCode statusCode = HttpStatusCode.OK, HttpContent? content = null)
    {
        _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = content ?? new JsonContent(new
                {
                    Data = "My data"
                })
            })
            .Verifiable();

        _httpClient = new HttpClient(HandlerMock.Object)
        {
            BaseAddress = Uri,
        };
    }


    public ApiClientTestBase()
    {
        CreateHttpClient();
    }

    public void VerifyCall(HttpMethod method, string uri)
    {
        _handlerMock?.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == method
                && req.RequestUri == new Uri($"http://test.com/{uri}")
            ),
            ItExpr.IsAny<CancellationToken>()
        );
    }
}