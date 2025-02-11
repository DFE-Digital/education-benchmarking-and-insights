using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App.ActionResults;
using Web.App.Services;
using Xunit;

namespace Web.Tests.ActionResults;

public class WhenCsvResultActionResultExecutorIsExecuted
{
    private readonly ActionContext _actionContext;
    private readonly CsvResultActionResultExecutor _executor;
    private readonly DefaultHttpContext _httpContext;
    private readonly Mock<ICsvService> _service;

    public WhenCsvResultActionResultExecutorIsExecuted()
    {
        _service = new Mock<ICsvService>();
        _executor = new CsvResultActionResultExecutor(_service.Object, NullLogger<CsvResultActionResultExecutor>.Instance);

        _httpContext = new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };
        _actionContext = new ActionContext
        {
            HttpContext = _httpContext
        };
    }

    [Fact]
    public async Task ShouldSetResponseHeaders()
    {
        // arrange
        var items = new List<TestObject>
        {
            new()
            {
                Id = 1,
                Value = "Value"
            }
        };
        const string fileName = nameof(fileName);

        var result = new CsvResult(items, fileName);
        const string csv = nameof(csv);
        _service.Setup(s => s.SaveToCsv(items)).Returns(csv);

        // act
        await _executor.ExecuteAsync(_actionContext, result);

        // assert
        Assert.Equal(result.StatusCode, _httpContext.Response.StatusCode);
        Assert.Equal(result.ContentType, _httpContext.Response.ContentType);
        Assert.Equal($"attachment; filename=\"{fileName}\"", _httpContext.Response.Headers.ContentDisposition);
    }

    [Fact]
    public async Task ShouldCallServiceAndSetResponseBody()
    {
        // arrange
        var items = new List<TestObject>
        {
            new()
            {
                Id = 1,
                Value = "Value"
            }
        };

        var result = new CsvResult(items);
        const string csv = nameof(csv);
        _service.Setup(s => s.SaveToCsv(items)).Returns(csv);

        // act
        await _executor.ExecuteAsync(_actionContext, result);

        // assert
        _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(_httpContext.Response.Body);
        var body = await reader.ReadToEndAsync();

        Assert.Equal(csv, body);
    }

    private class TestObject
    {
        public int Id { get; set; }
        public string? Value { get; set; }
    }
}