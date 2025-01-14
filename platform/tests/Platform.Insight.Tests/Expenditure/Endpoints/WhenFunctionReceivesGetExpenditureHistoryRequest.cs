using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Expenditure;
using Xunit;

namespace Platform.Insight.Tests.Expenditure.Endpoints;

public class WhenFunctionReceivesGetExpenditureHistoryRequest : ExpenditureSchoolFunctionsTestBase
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        ExpenditureParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((new ExpenditureYearsModel(), Array.Empty<ExpenditureHistoryModel>()));

        var result = await Functions.SchoolExpenditureHistoryAsync(CreateHttpRequestData(), "1", _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldThrowExceptionOnError()
    {
        ExpenditureParametersValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var exception = new Exception();
        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Throws(exception);

        // exception handled by middleware
        var result = await Assert.ThrowsAsync<Exception>(() => Functions.SchoolExpenditureHistoryAsync(CreateHttpRequestData(), "1", _cancellationToken));

        Assert.Equal(exception, result);
    }
}