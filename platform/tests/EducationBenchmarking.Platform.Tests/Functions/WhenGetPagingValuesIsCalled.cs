using EducationBenchmarking.Platform.Functions.Extensions;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Functions;

public class WhenGetPagingValuesIsCalled
{
    [Fact]
    public void PageAndPageSizeValuesPresent()
    {
        var query = new QueryCollection(
                new Dictionary<string, StringValues>
                {
                    { "page", "2" },
                    { "pageSize", "20" }
                });

        var result = query.GetPagingValues();

        Assert.Equal(2, result.Page);
        Assert.Equal(20, result.PageSize);
    }

    [Fact]
    public void NoValuesPresent()
    {
        var result =  new QueryCollection().GetPagingValues();

        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }


    [Fact]
    public void PageAndPageSizeValuesInvalid()
    {
        var query = new QueryCollection(
            new Dictionary<string, StringValues>
            {
                { "page", "invalid" },
                { "pageSize", "invalid" }
            });

        var result = query.GetPagingValues();

        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }
}