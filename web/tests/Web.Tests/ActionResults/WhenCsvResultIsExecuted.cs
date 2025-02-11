using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Web.App.ActionResults;
using Xunit;

// ReSharper disable ClassNeverInstantiated.Local

namespace Web.Tests.ActionResults;

public class WhenCsvResultIsExecuted
{
    private readonly Mock<IActionResultExecutor<CsvResult>> _actionResultExecutor;
    private readonly string _fileName = nameof(_fileName);
    private readonly TestObject[] _items = [];
    private readonly IServiceCollection _services = new ServiceCollection();

    public WhenCsvResultIsExecuted()
    {
        _actionResultExecutor = new Mock<IActionResultExecutor<CsvResult>>();
        _services.AddScoped<IActionResultExecutor<CsvResult>>(_ => _actionResultExecutor.Object);
    }

    [Fact]
    public void ShouldSetTheContentType()
    {
        var result = new CsvResult(_items, _fileName);

        Assert.Equal("text/csv; charset=utf-8", result.ContentType);
    }

    [Fact]
    public void ShouldSetTheItems()
    {
        var result = new CsvResult(_items, _fileName);

        Assert.Equal(_items, result.Items);
    }

    [Fact]
    public void ShouldSetTheFileName()
    {
        var result = new CsvResult(_items, _fileName);

        Assert.Equal(_fileName, result.FileName);
    }

    [Fact]
    public void ShouldSetTheStatusCode()
    {
        var result = new CsvResult(_items, _fileName);

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

        var result = new CsvResult(_items, _fileName);

        _actionResultExecutor
            .Setup(e => e.ExecuteAsync(actionContext, result))
            .Verifiable();

        await result.ExecuteResultAsync(actionContext);

        _actionResultExecutor.Verify();
    }

    private class TestObject;
}