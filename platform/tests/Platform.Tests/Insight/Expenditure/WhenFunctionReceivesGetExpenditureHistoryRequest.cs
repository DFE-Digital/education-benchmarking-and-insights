using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Expenditure;
using Xunit;
namespace Platform.Tests.Insight.Expenditure;

public class WhenFunctionReceivesGetExpenditureHistoryRequest : ExpenditureFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        ExpenditureParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<SchoolExpenditureHistoryModel>());

        var result = await Functions.SchoolExpenditureHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        ExpenditureParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SchoolExpenditureHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}