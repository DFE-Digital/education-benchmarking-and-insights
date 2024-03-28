using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight;

public class WhenFunctionReceivesGetMaintainedSchoolBalanceHistoryRequest : MaintainedSchoolFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.GetBalanceHistory(It.IsAny<string>(), It.IsAny<Dimension>()))
            .ReturnsAsync(Array.Empty<FinanceBalanceResponseModel>());

        var result = await Functions.BalanceHistoryMaintainedSchoolAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.GetBalanceHistory(It.IsAny<string>(), It.IsAny<Dimension>()))
            .Throws(new Exception());

        var result = await Functions.BalanceHistoryMaintainedSchoolAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}