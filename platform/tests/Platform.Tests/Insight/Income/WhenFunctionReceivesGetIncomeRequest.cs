using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight.Income;

public class WhenFunctionReceivesGetIncomeRequest : IncomeFunctionsTestBase
{
    private const string Urn = "URN";

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db.Setup(d => d.GetSchool(Urn, It.IsAny<string>())).ReturnsAsync(new IncomeResponseModel());

        var result = await Functions.SchoolIncomeAsync(CreateRequest(), Urn) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Theory]
    [InlineData(null, IncomeDimensions.Actuals)]
    [InlineData("dimension", IncomeDimensions.Actuals)]
    [InlineData(IncomeDimensions.PoundPerPupil, IncomeDimensions.PoundPerPupil)]
    public async Task ShouldTryParseQueryString(string? dimension, string expectedDimension)
    {
        Db.Setup(d => d.GetSchool(Urn, expectedDimension)).Verifiable();

        var query = new Dictionary<string, StringValues>
        {
            { "dimension", dimension }
        };
        await Functions.SchoolIncomeAsync(CreateRequest(query), Urn);

        Db.Verify();
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        Db.Setup(d => d.GetSchool(Urn, It.IsAny<string>())).ReturnsAsync((IncomeResponseModel?)null);

        var result = await Functions.SchoolIncomeAsync(CreateRequest(), Urn) as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db.Setup(d => d.GetSchool(Urn, It.IsAny<string>())).Throws(new Exception());

        var result = await Functions.SchoolIncomeAsync(CreateRequest(), Urn) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}