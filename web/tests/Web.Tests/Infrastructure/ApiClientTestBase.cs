using System.Net;
using Moq;
using Moq.Protected;
using Web.App.Infrastructure.Apis;
using Xunit.Abstractions;
namespace Web.Tests.Infrastructure;

public class ApiClientTestBase
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Mock<HttpMessageHandler>? _handlerMock;
    private HttpClient? _httpClient;


    public ApiClientTestBase(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        CreateHttpClient();
    }

    protected Mock<HttpMessageHandler> HandlerMock => _handlerMock ?? throw new InvalidOperationException();
    protected HttpClient HttpClient => _httpClient ?? throw new InvalidOperationException();

    protected Uri Uri { get; set; } = new("http://test.com/");

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
            BaseAddress = Uri
        };
    }

    protected void VerifyCall(HttpMethod method, string uri, string? body = null)
    {
        Func<HttpRequestMessage, Task<bool>> isRequestValid = async req =>
        {
            if (req.Method != method)
            {
                _testOutputHelper.WriteLine($"Expected method {method} but received {req.Method}");
                return false;
            }

            var expectedUri = new Uri($"http://test.com/{uri}");
            if (req.RequestUri != expectedUri)
            {
                _testOutputHelper.WriteLine($"Expected URI {expectedUri} but received {req.RequestUri}");
                return false;
            }

            if (body == null)
            {
                return true;
            }

            if (req.Content == null)
            {
                _testOutputHelper.WriteLine($"Expected body {body} but received no content");
                return false;
            }

            var content = await req.Content.ReadAsStringAsync();
            if (content == body)
            {
                return true;
            }

            _testOutputHelper.WriteLine($"Expected body {body} but received {content}");
            return false;
        };

        _handlerMock?.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(x => isRequestValid(x).Result),
            ItExpr.IsAny<CancellationToken>()
        );
    }
}