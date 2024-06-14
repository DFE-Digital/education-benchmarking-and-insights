using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Api.Insight.Income;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight.Income;

public class WhenFunctionReceivesGetIncomeHistoryRequest : IncomeFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<SchoolIncomeHistoryModel>());

        var result = await Functions.SchoolIncomeHistoryAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SchoolIncomeHistoryAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}