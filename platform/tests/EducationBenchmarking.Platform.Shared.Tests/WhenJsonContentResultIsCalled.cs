using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests;

public class WhenJsonContentResultIsCalled
{
    [Fact]
    public void ConstructorGetsObj()
    {
        // Arrange
        var data = new { test0 = "foo", test1 = "bar" };
        
        // Act
        var result = new JsonContentResult(data);

        // Assert
        Assert.Equal("application/json", result.ContentType);
        Assert.Equal(data.ToJson(), result.Content);
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public void ConstructorGetsPagedContent()
    {
        // Arrange
        var data = new TestPagedResults ();
        
        // Act
        var result = new JsonContentResult(data);

        // Assert
        Assert.Equal("application/json+paged", result.ContentType);
        Assert.Equal(data.ToJson(), result.Content);
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public void ConstructorGetsNotPagedContent()
    {
        // Arrange
        var data = new { Test0 = "foo", Test1 = "bar" };

        // Act
        var result = new JsonContentResult(data);

        // Assert
        Assert.Equal("application/json", result.ContentType);
        Assert.Equal(data.ToJson(), result.Content);
        Assert.Equal(200, result.StatusCode);
    }

    
    [Fact]
    public void ConstructorGets400StatusCode()
    {
        // Arrange
        var data = new { Test0 = "foo", Test1 = "bar" };
        var statusCode = HttpStatusCode.BadRequest;

        // Act
        var result = new JsonContentResult(data, statusCode);

        // Assert 
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(data.ToJson(), result.Content);
    }
}

public class TestPagedResults : IPagedResults
{
    public long TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; }

}
