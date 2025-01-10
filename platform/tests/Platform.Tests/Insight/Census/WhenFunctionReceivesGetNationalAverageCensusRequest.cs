using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Census;
using Xunit;

namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesGetNationalAverageExpenditureHistoryRequest : CensusNationalAveFunctionsTestBase
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        CensusNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        DistributedCache
            .Setup(d => d.GetSetAsync(It.IsAny<string>(), It.IsAny<Func<Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)>>>()))
            .ReturnsAsync((new CensusYearsModel(), Array.Empty<CensusHistoryModel>()));

        var result = await Functions.CensusHistoryAvgNationalAsync(CreateHttpRequestData(), _cancellationToken);

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
            .Setup(d => d.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var result = await Functions.CensusHistoryAvgNationalAsync(CreateHttpRequestData(), _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Service.Verify(
            x => x.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        CensusNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var exception = new Exception();
        DistributedCache
            .Setup(d => d.GetSetAsync(It.IsAny<string>(), It.IsAny<Func<Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)>>>()))
            .Throws(exception);

        // exception handled by middleware
        var result = await Assert.ThrowsAsync<Exception>(() => Functions.CensusHistoryAvgNationalAsync(CreateHttpRequestData(), _cancellationToken));

        Assert.Equal(exception, result);
    }
}