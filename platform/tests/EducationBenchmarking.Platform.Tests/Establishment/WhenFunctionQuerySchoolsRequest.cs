using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;
using School = EducationBenchmarking.Platform.Api.Establishment.Models.School;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class WhenFunctionQuerySchoolsRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Query(It.IsAny<IEnumerable<KeyValuePair<string,StringValues>>>()))
            .ReturnsAsync(new PagedResults<School>());
        
        var result = await Functions.QuerySchoolsAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }
    
    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Query(It.IsAny<IEnumerable<KeyValuePair<string,StringValues>>>()))
            .Throws(new Exception());
        
        var result = await Functions.QuerySchoolsAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}