using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesGetCensusRequest : CensusFunctionsTestBase
{
    private const string Urn = "URN";

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Get(Urn, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new CensusResponseModel());

        var result = await Functions.CensusAsync(CreateRequest(), Urn) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Theory]
    [InlineData(null, null, null, CensusDimensions.Total)]
    [InlineData("category", "dimension", null, CensusDimensions.Total)]
    [InlineData(CensusCategories.TeachersFte, CensusDimensions.HeadcountPerFte, CensusCategories.TeachersFte,
        CensusDimensions.HeadcountPerFte)]
    public async Task ShouldTryParseQueryString(string? category, string? dimension, string? expectedCategory,
        string expectedDimension)
    {
        Db.Setup(d => d.Get(Urn, expectedCategory, expectedDimension)).Verifiable();

        var query = new Dictionary<string, StringValues>
        {
            { "category", category },
            { "dimension", dimension }
        };
        await Functions.CensusAsync(CreateRequest(query), Urn);

        Db.Verify();
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        Db
            .Setup(d => d.Get(Urn, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((CensusResponseModel?)null);

        var result = await Functions.CensusAsync(CreateRequest(), Urn) as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Get(Urn, It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.CensusAsync(CreateRequest(), Urn) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}