using System.Net;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Insight.Census;
using Xunit;
namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesGetCensusRequest : CensusFunctionsTestBase
{
    private const string Urn = "URN";

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.GetAsync(Urn))
            .ReturnsAsync(new CensusModel());

        var result = await Functions.CensusAsync(CreateHttpRequestData(), Urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("category", "dimension")]
    [InlineData(CensusCategories.TeachersFte, CensusDimensions.HeadcountPerFte)]
    public async Task ShouldTryParseQueryString(string? category, string? dimension)
    {
        Service.Setup(d => d.GetAsync(Urn)).Verifiable();

        var query = new Dictionary<string, StringValues>
        {
            {
                "category", category
            },
            {
                "dimension", dimension
            }
        };
        await Functions.CensusAsync(CreateHttpRequestData(query), Urn);

        Service.Verify();
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        Service
            .Setup(d => d.GetAsync(Urn))
            .ReturnsAsync((CensusModel?)null);

        var result = await Functions.CensusAsync(CreateHttpRequestData(), Urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetAsync(Urn))
            .Throws(new Exception());

        var result = await Functions.CensusAsync(CreateHttpRequestData(), Urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}