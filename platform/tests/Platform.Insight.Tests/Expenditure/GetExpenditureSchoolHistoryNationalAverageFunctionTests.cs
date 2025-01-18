using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Expenditure;
using Platform.Api.Insight.Features.Expenditure.Models;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Api.Insight.Features.Expenditure.Services;
using Platform.Api.Insight.Shared;
using Platform.Test;
using Xunit;

namespace Platform.Insight.Tests.Expenditure;

public class GetExpenditureSchoolHistoryNationalAverageFunctionTests : FunctionsTestBase
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;
    private readonly GetExpenditureSchoolHistoryNationalAverageFunction _function;
    private readonly Mock<IExpenditureService> _service;
    private readonly Mock<IValidator<ExpenditureNationalAvgParameters>> _validator;

    public GetExpenditureSchoolHistoryNationalAverageFunctionTests()
    {
        _validator = new Mock<IValidator<ExpenditureNationalAvgParameters>>();
        _service = new Mock<IExpenditureService>();
        _function = new GetExpenditureSchoolHistoryNationalAverageFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((new YearsModel(), Array.Empty<ExpenditureHistoryModel>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[]
            {
                new ValidationFailure(nameof(ExpenditureParameters.Dimension), "error message")
            }));

        _service
            .Setup(d => d.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        _service.Verify(
            x => x.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
    }
}