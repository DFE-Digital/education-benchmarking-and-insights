using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class WhenFunctionReceivesGetSchoolSizeBandingsRequest : BandingsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.SchoolSizeBandings(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal?>(), It.IsAny<bool?>()))
            .ReturnsAsync(Array.Empty<Banding>());
        
        var result =
            await Functions.GetSchoolSizeBandings(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.SchoolSizeBandings(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal?>(), It.IsAny<bool?>()))
            .Throws(new Exception());
        
        var result = await Functions
            .GetSchoolSizeBandings(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}