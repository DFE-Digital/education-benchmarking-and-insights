using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Web.App.ActionResults;
using Xunit;

// ReSharper disable ClassNeverInstantiated.Local

namespace Web.Tests.ActionResults;

public class WhenCsvResultsIsExecuted
{
    private readonly Mock<IActionResultExecutor<CsvResults>> _actionResultExecutor;
    private readonly string _csvFileName = nameof(_csvFileName);
    private readonly TestObject[] _items = [];
    private readonly IServiceCollection _services = new ServiceCollection();
    private readonly string _zipFileName = nameof(_zipFileName);

    public WhenCsvResultsIsExecuted()
    {
        _actionResultExecutor = new Mock<IActionResultExecutor<CsvResults>>();
        _services.AddScoped<IActionResultExecutor<CsvResults>>(_ => _actionResultExecutor.Object);
    }

    [Fact]
    public void ShouldSetTheContentType()
    {
        var result = new CsvResults([new CsvResult(_items, _csvFileName)], _zipFileName);

        Assert.Equal("application/zip", result.ContentType);
    }

    [Fact]
    public void ShouldSetTheItems()
    {
        var result = new CsvResults([new CsvResult(_items, _csvFileName)], _zipFileName);

        Assert.Single(result.Items);
        Assert.Equal(_items, result.Items.First().Items);
    }

    [Fact]
    public void ShouldSetTheFileName()
    {
        var result = new CsvResults([new CsvResult(_items, _csvFileName)], _zipFileName);

        Assert.Equal(_zipFileName, result.ZipFileName);
        Assert.Single(result.Items);
        Assert.Equal(_csvFileName, result.Items.First().CsvFileName);
    }

    [Fact]
    public void ShouldSetTheStatusCode()
    {
        var result = new CsvResults([new CsvResult(_items, _csvFileName)], _zipFileName);

        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldExecuteTheActionResultExecutor()
    {
        var provider = _services.BuildServiceProvider();
        var httpContext = new DefaultHttpContext
        {
            RequestServices = provider
        };

        var actionContext = new ActionContext
        {
            HttpContext = httpContext
        };

        var result = new CsvResults([new CsvResult(_items, _csvFileName)], _zipFileName);

        _actionResultExecutor
            .Setup(e => e.ExecuteAsync(actionContext, result))
            .Verifiable();

        await result.ExecuteResultAsync(actionContext);

        _actionResultExecutor.Verify();
    }

    private class TestObject;
}