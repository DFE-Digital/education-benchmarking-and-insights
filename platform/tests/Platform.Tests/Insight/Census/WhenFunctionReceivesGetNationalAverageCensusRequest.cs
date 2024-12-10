using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Census;
using Xunit;
namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesGetNationalAverageExpenditureHistoryRequest : CensusFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        CensusNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetHistoryAvgNationalAsync(new CensusNationalAvgParameters()))
            .ReturnsAsync(Array.Empty<CensusHistoryModel>());

        var result = await Functions.CensusHistoryAvgNationalAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        CensusNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[]
            {
                new ValidationFailure(nameof(CensusNationalAvgParameters.Dimension), "error message")
            }));

        Service
            .Setup(d => d.GetHistoryAvgNationalAsync(new CensusNationalAvgParameters()));

        var result = await Functions.CensusHistoryAvgNationalAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Service.Verify(
            x => x.GetHistoryAvgNationalAsync(new CensusNationalAvgParameters()), Times.Never());
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        CensusNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetHistoryAvgNationalAsync(new CensusNationalAvgParameters()))
            .Throws(new Exception());

        var result = await Functions.CensusHistoryAvgNationalAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}