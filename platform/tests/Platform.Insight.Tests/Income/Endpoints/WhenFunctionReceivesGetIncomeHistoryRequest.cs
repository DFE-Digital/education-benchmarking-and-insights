using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Income;
using Xunit;

namespace Platform.Insight.Tests.Income.Endpoints;

public class WhenFunctionReceivesGetIncomeHistoryRequest : IncomeSchoolFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<IncomeParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((new IncomeYearsModel(), Array.Empty<IncomeHistoryModel>()));

        var result = await Functions.SchoolIncomeHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<IncomeParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SchoolIncomeHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}