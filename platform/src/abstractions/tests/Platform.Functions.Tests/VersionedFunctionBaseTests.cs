using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using Platform.Test;
using Xunit;

namespace Platform.Functions.Tests;

public class VersionedFunctionBaseTests : FunctionsTestBase
{
    [Fact]
    public async Task RunAsync_NoVersion_UsesLatestHandler()
    {
        var request = CreateHttpRequestData();
        var expectedResponse = HttpResponseDataFactory.Create();

        var v1Handler = new Mock<ITestHandler>();
        v1Handler
            .SetupGet(h => h.Version).Returns("1.0");
        v1Handler
            .Setup(h => h.HandleAsync(It.IsAny<TestContext>()))
            .ReturnsAsync(HttpResponseDataFactory.Create());

        var v2Handler = new Mock<ITestHandler>();
        v2Handler
            .SetupGet(h => h.Version).Returns("2.0"); // latest
        v2Handler
            .Setup(h => h.HandleAsync(It.IsAny<TestContext>()))
            .ReturnsAsync(expectedResponse);

        var function = new TestVersionedFunction([v1Handler.Object, v2Handler.Object]);
        var context = new TestContext(request, CancellationToken.None);

        var result = await function.ExecuteAsync(context);

        Assert.Same(expectedResponse, result);
        v2Handler.Verify(h => h.HandleAsync(It.IsAny<TestContext>()), Times.Once);
        v1Handler.Verify(h => h.HandleAsync(It.IsAny<TestContext>()), Times.Never);
    }

    [Fact]
    public async Task RunAsync_UnsupportedVersion_ReturnsUnsupportedVersionResponse()
    {
        var kvp = new KeyValuePair<string, string>("x-api-version", "99.0");
        var request = CreateHttpRequestData(null, new HttpHeadersCollection([kvp]));

        var handler = new Mock<ITestHandler>();
        handler.SetupGet(h => h.Version).Returns("1.0");

        var function = new TestVersionedFunction([handler.Object]);
        var context = new TestContext(request, CancellationToken.None);

        var response = await function.ExecuteAsync(context);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        handler.Verify(h => h.HandleAsync(It.IsAny<TestContext>()), Times.Never);
    }

    [Fact]
    public async Task RunAsync_HandlerResponse_IsReturnedUnmodified()
    {
        var kvp = new KeyValuePair<string, string>("x-api-version", "1.0");
        var request = CreateHttpRequestData(null, new HttpHeadersCollection([kvp]));
        var expectedResponse = HttpResponseDataFactory.Create();

        var handler = new Mock<ITestHandler>();
        handler
            .SetupGet(h => h.Version).Returns("1.0");
        handler
            .Setup(h => h.HandleAsync(It.IsAny<TestContext>()))
            .ReturnsAsync(expectedResponse);

        var function = new TestVersionedFunction([handler.Object]);
        var context = new TestContext(request, CancellationToken.None);

        var result = await function.ExecuteAsync(context);

        Assert.Same(expectedResponse, result);
    }
}

public sealed record TestContext(HttpRequestData Request, CancellationToken Token) : HandlerContext(Request, Token);

public interface ITestHandler : IVersionedHandler<TestContext>;

public sealed class TestVersionedFunction(IEnumerable<ITestHandler> handlers) : VersionedFunctionBase<ITestHandler, TestContext>(handlers)
{
    public Task<HttpResponseData> ExecuteAsync(TestContext context) => RunAsync(context);
}