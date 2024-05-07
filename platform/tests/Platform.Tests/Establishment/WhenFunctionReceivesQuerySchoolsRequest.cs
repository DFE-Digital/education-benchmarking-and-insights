using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesQuerySchoolsRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Query(It.IsAny<string?>(), It.IsAny<string?>()))
            .ReturnsAsync(Array.Empty<SchoolResponseModel>());

        var result = await Functions.QuerySchoolsAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Query(It.IsAny<string?>(), It.IsAny<string?>()))
            .Throws(new Exception());

        var result = await Functions.QuerySchoolsAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}