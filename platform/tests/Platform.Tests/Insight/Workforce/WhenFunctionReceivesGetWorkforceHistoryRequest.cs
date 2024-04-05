using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight.Workforce;

public class WhenFunctionReceivesGetWorkforceHistoryRequest : WorkforceFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.GetHistory(It.IsAny<string>(), It.IsAny<WorkforceDimension>()))
            .ReturnsAsync(Array.Empty<WorkforceResponseModel>());

        var result = await Functions.WorkforceHistoryAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.GetHistory(It.IsAny<string>(), It.IsAny<WorkforceDimension>()))
            .Throws(new Exception());

        var result = await Functions.WorkforceHistoryAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}