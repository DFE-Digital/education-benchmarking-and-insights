using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Income;
using Platform.Api.Insight.Features.Income.Models;
using Platform.Api.Insight.Features.Income.Parameters;
using Platform.Api.Insight.Features.Income.Responses;
using Platform.Api.Insight.Features.Income.Services;
using Platform.Api.Insight.Shared;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Income;

public class GetIncomeTrustHistoryFunctionTests : FunctionsTestBase
{
    private readonly GetIncomeTrustHistoryFunction _function;
    private readonly Mock<IIncomeService> _service;
    private readonly Mock<IValidator<IncomeParameters>> _validator;

    public GetIncomeTrustHistoryFunctionTests()
    {
        _validator = new Mock<IValidator<IncomeParameters>>();
        _service = new Mock<IIncomeService>();
        _function = new GetIncomeTrustHistoryFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<IncomeParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetTrustHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((new YearsModel(), Array.Empty<IncomeHistoryModel>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<IncomeHistoryResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<IncomeParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetTrustHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((null, Array.Empty<IncomeHistoryModel>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<IncomeParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(IncomeParameters.Dimension), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(IncomeParameters.Dimension));

        _service
            .Verify(d => d.GetTrustHistoryAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}