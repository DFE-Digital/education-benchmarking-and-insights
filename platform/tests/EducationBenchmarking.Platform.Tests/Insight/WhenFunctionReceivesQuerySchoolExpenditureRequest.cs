using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class WhenFunctionReceivesQuerySchoolExpenditureRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Expenditure(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedSchoolExpenditure());
        
        var result = await Functions.QuerySchoolExpenditureAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }
    

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Expenditure(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<int>()))
            .Throws(new Exception());
        
        var result = await Functions.QuerySchoolExpenditureAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}