using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Expenditure;
using Xunit;
namespace Platform.Tests.Insight.Expenditure;

public class WhenFunctionReceivesGetNationalAverageExpenditureHistoryRequest : ExpenditureFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        ExpenditureNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAvgNationalAsync(new ExpenditureNationalAvgParameters()))
            .ReturnsAsync(Array.Empty<SchoolExpenditureHistoryModel>());

        var result = await Functions.SchoolExpenditureHistoryAvgNationalAsync(CreateHttpRequestData());

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
            .Setup(d => d.GetSchoolHistoryAvgNationalAsync(new ExpenditureNationalAvgParameters()));

        var result = await Functions.SchoolExpenditureHistoryAvgNationalAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Service.Verify(
            x => x.GetSchoolHistoryAvgNationalAsync(new ExpenditureNationalAvgParameters()), Times.Never());
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        ExpenditureNationalAvgParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAvgNationalAsync(new ExpenditureNationalAvgParameters()))
            .Throws(new Exception());

        var result = await Functions.SchoolExpenditureHistoryAvgNationalAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}