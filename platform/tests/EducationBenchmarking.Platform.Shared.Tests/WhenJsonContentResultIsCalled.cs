using System.Net;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests;

public class WhenJsonContentResultIsCalled
{
    [Fact]
    public void ConstructorGetsObj()
    {
        var data = new { test0 = "foo", test1 = "bar" };
        var result = new JsonContentResult(data);

        Assert.Equal("application/json", result.ContentType);
        Assert.Equal(data.ToJson(), result.Content);
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public void ConstructorGetsPagedContent()
    {
        var data = new TestPagedResults();
        var result = new JsonContentResult(data);
        
        Assert.Equal("application/json+paged", result.ContentType);
        Assert.Equal(data.ToJson(), result.Content);
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public void ConstructorGetsNotPagedContent()
    {
        var data = new { Test0 = "foo", Test1 = "bar" };
        var result = new JsonContentResult(data);
        
        Assert.Equal("application/json", result.ContentType);
        Assert.Equal(data.ToJson(), result.Content);
        Assert.Equal(200, result.StatusCode);
    }

    
    [Fact]
    public void ConstructorGets400StatusCode()
    {
        var data = new { Test0 = "foo", Test1 = "bar" };
        var statusCode = HttpStatusCode.BadRequest;
        var result = new JsonContentResult(data, statusCode);
        
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(data.ToJson(), result.Content);
    }
}

public class TestPagedResults : IPagedResults
{
    public long TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount => 0;
}
