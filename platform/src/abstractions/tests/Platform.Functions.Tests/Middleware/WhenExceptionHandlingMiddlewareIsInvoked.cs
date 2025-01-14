using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Functions.Middleware;
using Platform.Functions.Tests.Mocks;
using Xunit;

namespace Platform.Functions.Tests.Middleware;

public class WhenExceptionHandlingMiddlewareIsInvoked
{
    private readonly Mock<FunctionContextWrapper> _context;
    private readonly ExceptionHandlingMiddleware _middleware;
    private HttpResponseData? _actual;

    public WhenExceptionHandlingMiddlewareIsInvoked()
    {
        var logger = NullLogger<ExceptionHandlingMiddleware>.Instance;
        _middleware = new ExceptionHandlingMiddleware(logger);

        _context = new Mock<FunctionContextWrapper>(new Mock<FunctionContext>().Object);
        var request = MockHttpRequestData.Create();
        _context.Setup(c => c.GetHttpRequestDataAsync()).ReturnsAsync(request);
        _context.Setup(c => c.SetInvocationResult(It.IsAny<HttpResponseData>())).Callback((HttpResponseData value) =>
        {
            _actual = value;
        });
    }

    [Fact]
    public async Task WritesClientClosedRequestForOperationCancelledException()
    {
        await _middleware.Invoke(_context.Object, Next);

        Assert.NotNull(_actual);
        Assert.Equal((HttpStatusCode)499, _actual.StatusCode);
        return;

        Task Next(FunctionContext _)
        {
            throw new OperationCanceledException("Message");
        }
    }

    [Fact]
    public async Task WritesClientClosedRequestForTaskCancelledException()
    {
        await _middleware.Invoke(_context.Object, Next);

        Assert.NotNull(_actual);
        Assert.Equal((HttpStatusCode)499, _actual.StatusCode);
        return;

        Task Next(FunctionContext _)
        {
            throw new TaskCanceledException("Message");
        }
    }

    [Fact]
    public async Task WritesClientClosedRequestForCancelledSqlException()
    {
        await _middleware.Invoke(_context.Object, Next);

        Assert.NotNull(_actual);
        Assert.Equal((HttpStatusCode)499, _actual.StatusCode);
        return;

        Task Next(FunctionContext _)
        {
            throw TestDatabase.MakeSqlException("Operation cancelled by user");
        }
    }

    [Fact]
    public async Task WritesInternalServerErrorForOtherExceptions()
    {
        await _middleware.Invoke(_context.Object, Next);

        Assert.NotNull(_actual);
        Assert.Equal(HttpStatusCode.InternalServerError, _actual.StatusCode);
        return;

        Task Next(FunctionContext _)
        {
            throw new Exception("Message");
        }
    }
}