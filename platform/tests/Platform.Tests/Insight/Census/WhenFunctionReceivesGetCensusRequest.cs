using System.Net;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Insight.Census;
using Xunit;

namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesGetCensusRequest : CensusSchoolFunctionsTestBase
{
    private const string Urn = "URN";

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        CensusParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetAsync(Urn, It.IsAny<string>()))
            .ReturnsAsync(new CensusSchoolModel());

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
        CensusParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service.Setup(d => d.GetAsync(Urn, It.IsAny<string>())).Verifiable();

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
        CensusParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetAsync(Urn, It.IsAny<string>()))
            .ReturnsAsync((CensusSchoolModel?)null);

        var result = await Functions.CensusAsync(CreateHttpRequestData(), Urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        CensusParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetAsync(Urn, It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.CensusAsync(CreateHttpRequestData(), Urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}