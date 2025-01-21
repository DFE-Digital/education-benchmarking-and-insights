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

public class GetExpenditureSchoolFunctionTests : FunctionsTestBase
{
    private const string Urn = "URN";
    private readonly GetExpenditureSchoolFunction _function;
    private readonly Mock<IExpenditureService> _service;
    private readonly Mock<IValidator<ExpenditureParameters>> _validator;
    private readonly Fixture _fixture;

    public GetExpenditureSchoolFunctionTests()
    {
        _service = new Mock<IExpenditureService>();
        _validator = new Mock<IValidator<ExpenditureParameters>>();
        _fixture = new Fixture();
        _function = new GetExpenditureSchoolFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<ExpenditureSchoolModel>().Create();
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetSchoolAsync(Urn, It.IsAny<string>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData(), Urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<ExpenditureSchoolResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetSchoolAsync(Urn, It.IsAny<string>()))
            .ReturnsAsync((ExpenditureSchoolModel?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), Urn);

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

        var result = await _function.RunAsync(CreateHttpRequestData(), Urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(ExpenditureParameters.Dimension));

        _service
            .Verify(d => d.GetSchoolAsync(Urn, It.IsAny<string>()), Times.Never);
    }
}