using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight;

public class WhenFunctionReceivesQuerySchoolWorkforceRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Workforce(It.IsAny<string[]>()))
            .ReturnsAsync(Array.Empty<SchoolWorkforceResponseModel>());

        var result = await Functions.QuerySchoolWorkforceAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Workforce(It.IsAny<string[]>()))
            .Throws(new Exception());

        var result = await Functions.QuerySchoolWorkforceAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}