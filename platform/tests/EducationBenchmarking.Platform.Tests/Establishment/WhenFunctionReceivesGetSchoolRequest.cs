using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class WhenFunctionReceivesGetSchoolRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Get(It.IsAny<string>()))
            .ReturnsAsync(new School());
        
        var result = await Functions.GetSchoolAsync(CreateRequest(),"1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        
        Db
            .Setup(d => d.Get(It.IsAny<string>()))
            .ReturnsAsync((School?)null);
        
        var result = await Functions.GetSchoolAsync(CreateRequest(),"1") as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result?.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Get(It.IsAny<string>()))
            .Throws(new Exception());
        
        var result = await Functions.GetSchoolAsync(CreateRequest(),"1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}