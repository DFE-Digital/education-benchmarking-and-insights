using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight.SchoolFinance;

public class WhenFunctionReceivesGetIncomeHistoryRequest : SchoolFinanceFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.GetIncomeHistory(It.IsAny<string>(), It.IsAny<Dimension>()))
            .ReturnsAsync(Array.Empty<IncomeResponseModel>());

        var result = await Functions.SchoolIncomeHistoryAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.GetIncomeHistory(It.IsAny<string>(), It.IsAny<Dimension>()))
            .Throws(new Exception());

        var result = await Functions.SchoolIncomeHistoryAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}