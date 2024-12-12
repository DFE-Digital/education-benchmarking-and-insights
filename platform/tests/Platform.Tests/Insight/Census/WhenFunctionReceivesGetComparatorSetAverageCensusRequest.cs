using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Census;
using Xunit;
namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesGetComparatorSetAverageCensusHistoryRequest : CensusFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        CensusParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetHistoryAvgComparatorSetAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<CensusHistoryModel>());

        var result = await Functions.CensusHistoryAvgComparatorSetAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        CensusParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[]
            {
                new ValidationFailure(nameof(CensusParameters.Dimension), "error message")
            }));

        Service
            .Setup(d => d.GetHistoryAvgComparatorSetAsync(It.IsAny<string>(), It.IsAny<string>()));

        var result = await Functions.CensusHistoryAvgComparatorSetAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Service.Verify(
            x => x.GetHistoryAvgComparatorSetAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        CensusParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetHistoryAvgComparatorSetAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.CensusHistoryAvgComparatorSetAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}