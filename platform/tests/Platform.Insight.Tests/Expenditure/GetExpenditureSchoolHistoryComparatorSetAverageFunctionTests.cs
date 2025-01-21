using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Expenditure;
using Platform.Api.Insight.Features.Expenditure.Models;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Api.Insight.Features.Expenditure.Responses;
using Platform.Api.Insight.Features.Expenditure.Services;
using Platform.Api.Insight.Shared;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Expenditure;

public class GetExpenditureSchoolHistoryComparatorSetAverageFunctionTests : FunctionsTestBase
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;
    private readonly GetExpenditureSchoolHistoryComparatorSetAverageFunction _function;
    private readonly Mock<IExpenditureService> _service;
    private readonly Mock<IValidator<ExpenditureParameters>> _validator;

    public GetExpenditureSchoolHistoryComparatorSetAverageFunctionTests()
    {
        _validator = new Mock<IValidator<ExpenditureParameters>>();
        _service = new Mock<IExpenditureService>();
        _function = new GetExpenditureSchoolHistoryComparatorSetAverageFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetComparatorAveHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((new YearsModel(), Array.Empty<ExpenditureHistoryModel>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), "1", _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<ExpenditureHistoryResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetComparatorAveHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((null, Array.Empty<ExpenditureHistoryModel>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), "1", _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(ExpenditureParameters.Dimension), "error message")
            ]));

        _service
            .Setup(d => d.GetComparatorAveHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), "1", _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(ExpenditureParameters.Dimension));

        _service.Verify(
            x => x.GetComparatorAveHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
    }
}