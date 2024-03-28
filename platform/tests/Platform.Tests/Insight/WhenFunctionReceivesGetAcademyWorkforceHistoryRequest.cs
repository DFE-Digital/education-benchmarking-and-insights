using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight;

public class WhenFunctionReceivesGetAcademyWorkforceHistoryRequest : AcademySchoolFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.GetWorkforceHistory(It.IsAny<string>(), It.IsAny<Dimension>()))
            .ReturnsAsync(Array.Empty<FinanceWorkforceResponseModel>());

        var result = await Functions.WorkforceHistoryAcademyAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.GetWorkforceHistory(It.IsAny<string>(), It.IsAny<Dimension>()))
            .Throws(new Exception());

        var result = await Functions.WorkforceHistoryAcademyAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}