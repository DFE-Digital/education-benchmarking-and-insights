using Microsoft.Extensions.Primitives;
using Xunit;
using EducationBenchmarking.Platform.Shared.Helpers;

namespace EducationBenchmarking.Platform.Shared.Tests;

public class WhenGetPagingValuesIsCalled
{
    [Fact]
    public void PageAndPageSizeValuesPresent()
    {
        var query = new List<KeyValuePair<string, StringValues>>
        {
            new("page", "2"),
            new("pageSize", "20")
        };
        
        var result = QueryParameters.GetPagingValues(query);
        
        Assert.Equal(2, result.Page);
        Assert.Equal(20, result.PageSize);
    }
    
    [Fact]
    public void NoValuesPresent()
    {
        var result = QueryParameters.GetPagingValues(new List<KeyValuePair<string, StringValues>>());
        
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }


    [Fact]
    public void PageAndPageSizeValuesInvalid()
    {
        var query = new List<KeyValuePair<string, StringValues>>
        {
            new("page", "invalid"),
            new("pageSize", "invalid")
        };
        
        var result = QueryParameters.GetPagingValues(query);
        
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }


    [Fact]
    public void PageValuePresent()
    {
        var query = new List<KeyValuePair<string, StringValues>>
        {
            new("page", "2"),
        };
        
        var result = QueryParameters.GetPagingValues(query);
        
        Assert.Equal(2, result.Page);
        Assert.Equal(10, result.PageSize);
    }


    [Fact]
    public void PageSizeValuePresent()
    {
        var query = new List<KeyValuePair<string, StringValues>>
        {
            new("pageSize", "20")
        };
        
        var result = QueryParameters.GetPagingValues(query);
        
        Assert.Equal(1, result.Page);
        Assert.Equal(20, result.PageSize);
    }

}