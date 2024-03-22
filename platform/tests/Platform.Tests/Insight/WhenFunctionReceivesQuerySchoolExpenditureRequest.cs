using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain.Responses;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight;

public class WhenFunctionReceivesQuerySchoolExpenditureRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Expenditure(It.IsAny<string[]>()))
            .ReturnsAsync(Array.Empty<SchoolExpenditure>());

        var result = await Functions.QuerySchoolExpenditureAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Expenditure(It.IsAny<string[]>()))
            .Throws(new Exception());

        var result = await Functions.QuerySchoolExpenditureAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}