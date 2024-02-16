using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using School = EducationBenchmarking.Platform.Domain.Responses.School;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class WhenFunctionQuerySchoolsRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Query(It.IsAny<IQueryCollection>()))
            .ReturnsAsync(new PagedResults<School>());

        var result = await Functions.QuerySchoolsAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Query(It.IsAny<IQueryCollection>()))
            .Throws(new Exception());

        var result = await Functions.QuerySchoolsAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}