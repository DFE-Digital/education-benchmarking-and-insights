using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Expenditure;
using Xunit;

namespace Platform.Insight.Tests.Expenditure.Endpoints;

public class WhenFunctionReceivesGetNationalAverageExpenditureHistoryRequest : ExpenditureNationalAveFunctionsTestBase
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        ExpenditureNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((new ExpenditureYearsModel(), Array.Empty<ExpenditureHistoryModel>()));

        var result = await Functions.SchoolExpenditureHistoryAvgNationalAsync(CreateHttpRequestData(), _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        ExpenditureNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[]
            {
                new ValidationFailure(nameof(ExpenditureParameters.Dimension), "error message")
            }));

        Service
            .Setup(d => d.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var result = await Functions.SchoolExpenditureHistoryAvgNationalAsync(CreateHttpRequestData(), _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Service.Verify(
            x => x.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    public async Task ShouldThrowExceptionOnError()
    {
        ExpenditureNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var exception = new Exception();
        Service
            .Setup(d => d.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Throws(exception);

        // exception handled by middleware
        var result = await Assert.ThrowsAsync<Exception>(() => Functions.SchoolExpenditureHistoryAvgNationalAsync(CreateHttpRequestData(), _cancellationToken));

        Assert.Equal(exception, result);
    }
}