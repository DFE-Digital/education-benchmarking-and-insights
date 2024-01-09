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
        // create obj
        var data = new { test0 = "foo", test1 = "bar" };
        
        // Act
        // call constructor
        var result = new JsonContentResult(data);

        // Assert
        // Assert equals result.ContentType == "application/json+paged"
        Assert.Equal("application/json", result.ContentType);
        // content is correct
        Assert.Equal(data.ToJson(), result.Content);
        // statusCode == 200
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public void ConstructorGetsPagedContent()
    {
        // Arrange
        // Mock up object that is paged
        var data = new TestPagedResults ();
        
        // Act
        // Call constructor
        var result = new JsonContentResult(data);

        // Assert
        // Assert equals result.ContentType == "application/json+paged"
        Assert.Equal("application/json+paged", result.ContentType);
        // content is correct
        Assert.Equal(data.ToJson(), result.Content);
        // statusCode == 200
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public void ConstructorGetsNotPagedContent()
    {
        // Arrange
        // Mock up object
        var data = new { Test0 = "foo", Test1 = "bar" };

        // Act
        // call constructor as result
        var result = new JsonContentResult(data);

        // Assert
        // assert equals result.ContentType == "application/json"
        Assert.Equal("application/json", result.ContentType);
        // content is correct
        Assert.Equal(data.ToJson(), result.Content);
        // statusCode == 200
        Assert.Equal(200, result.StatusCode);
    }

    
    [Fact]
    public void ConstructorGets400StatusCode()
    {
        // Arrange
        // Mock up object
        var data = new { Test0 = "foo", Test1 = "bar" };
        var statusCode = HttpStatusCode.BadRequest;

        // Act
        // call constructor as result
        var result = new JsonContentResult(data, statusCode);

        // Assert equals result.StatusCode == 400
        Assert.Equal(400, result.StatusCode);
        // content is correct
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
