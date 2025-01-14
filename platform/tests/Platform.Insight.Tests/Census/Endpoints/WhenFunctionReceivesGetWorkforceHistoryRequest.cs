using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Census;
using Xunit;

namespace Platform.Insight.Tests.Census.Endpoints;

public class WhenFunctionReceivesGetWorkforceHistoryRequest : CensusSchoolFunctionsTestBase
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        CensusParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((new CensusYearsModel(), Array.Empty<CensusHistoryModel>()));

        var result = await Functions.CensusHistoryAsync(CreateHttpRequestData(), "1", _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        CensusParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var exception = new Exception();
        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Throws(exception);

        // exception handled by middleware
        var result = await Assert.ThrowsAsync<Exception>(() => Functions.CensusHistoryAsync(CreateHttpRequestData(), "1", _cancellationToken));

        Assert.Equal(exception, result);
    }
}