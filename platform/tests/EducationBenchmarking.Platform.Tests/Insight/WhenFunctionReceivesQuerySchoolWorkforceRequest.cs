using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class WhenFunctionReceivesQuerySchoolWorkforceRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.GetWorkforce(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedSchoolWorkforce());
        
        var result = await Functions.QuerySchoolWorkforceAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }
    

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.GetWorkforce(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<int>()))
            .Throws(new Exception());
        
        var result = await Functions.QuerySchoolWorkforceAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}