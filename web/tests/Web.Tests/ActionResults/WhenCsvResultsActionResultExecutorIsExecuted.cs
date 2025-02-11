using System.IO.Compression;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App.ActionResults;
using Web.App.Services;
using Xunit;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Web.Tests.ActionResults;

public class WhenCsvResultsActionResultExecutorIsExecuted
{
    private readonly ActionContext _actionContext;
    private readonly CsvResultsActionResultExecutor _executor;
    private readonly DefaultHttpContext _httpContext;
    private readonly Mock<ICsvService> _service;

    public WhenCsvResultsActionResultExecutorIsExecuted()
    {
        _service = new Mock<ICsvService>();
        _executor = new CsvResultsActionResultExecutor(_service.Object, NullLogger<CsvResultsActionResultExecutor>.Instance);

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

        var result = new CsvResults([new CsvResult(items)], fileName);
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
        const string fileName = nameof(fileName);

        var result = new CsvResults([new CsvResult(items, fileName)]);
        const string csv = nameof(csv);
        _service.Setup(s => s.SaveToCsv(items)).Returns(csv);

        // act
        await _executor.ExecuteAsync(_actionContext, result);

        // assert
        await foreach (var tuple in GetFilesFromZip(_httpContext.Response.Body))
        {
            Assert.Equal(fileName, tuple.fileName);
            Assert.Equal(csv, tuple.content);
        }
    }

    private static async IAsyncEnumerable<(string fileName, string content)> GetFilesFromZip(Stream responseBody)
    {
        responseBody.Seek(0, SeekOrigin.Begin);

        using var archive = new ZipArchive(responseBody, ZipArchiveMode.Read);
        foreach (var entry in archive.Entries)
        {
            await using var entryStream = entry.Open();
            using var reader = new StreamReader(entryStream);
            var content = await reader.ReadToEndAsync();
            yield return (entry.Name, content);
        }
    }

    private class TestObject
    {
        public int Id { get; set; }
        public string? Value { get; set; }
    }
}