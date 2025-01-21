using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Expenditure;
using Platform.Api.Insight.Features.Expenditure.Models;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Api.Insight.Features.Expenditure.Responses;
using Platform.Api.Insight.Features.Expenditure.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Expenditure;

public class GetExpenditureTrustsFunctionTests : FunctionsTestBase
{
    private readonly GetExpenditureTrustsFunction _function;
    private readonly Mock<IExpenditureService> _service;
    private readonly Mock<IValidator<ExpenditureQueryTrustParameters>> _validator;
    private readonly Fixture _fixture;

    public GetExpenditureTrustsFunctionTests()
    {
        _service = new Mock<IExpenditureService>();
        _validator = new Mock<IValidator<ExpenditureQueryTrustParameters>>();
        _fixture = new Fixture();
        _function = new GetExpenditureTrustsFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<ExpenditureTrustModel>().CreateMany(5);
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureQueryTrustParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.QueryTrustsAsync(It.IsAny<string[]>(), It.IsAny<string>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<ExpenditureTrustResponse[]>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureQueryTrustParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(ExpenditureQueryTrustParameters.Dimension), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(ExpenditureQueryTrustParameters.Dimension));

        _service.Verify(
            x => x.QueryTrustsAsync(It.IsAny<string[]>(), It.IsAny<string>()), Times.Never());
    }
}