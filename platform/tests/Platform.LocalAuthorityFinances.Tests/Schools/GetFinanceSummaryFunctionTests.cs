using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.LocalAuthorityFinances.Features.Schools;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Responses;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.Schools;

public class GetFinanceSummaryFunctionTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetFinanceSummaryFunction _function;
    private readonly Mock<ISchoolsService> _service;
    private readonly Mock<IValidator<FinanceSummaryParameters>> _validator;

    public GetFinanceSummaryFunctionTests()
    {
        _service = new Mock<ISchoolsService>();
        _validator = new Mock<IValidator<FinanceSummaryParameters>>();
        _fixture = new Fixture();
        _function = new GetFinanceSummaryFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<FinanceSummaryResponse>().CreateMany();
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<FinanceSummaryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.GetFinanceSummaryAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string[]>(),
                It.IsAny<string[]>(),
                It.IsAny<string[]>(),
                It.IsAny<int?>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData(), "101");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<FinanceSummaryResponse[]>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<FinanceSummaryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(FinanceSummaryParameters.OverallPhase), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData(), "101");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(FinanceSummaryParameters.OverallPhase));

        _service
            .Verify(s => s.GetFinanceSummaryAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string[]>(),
                It.IsAny<string[]>(),
                It.IsAny<string[]>(),
                It.IsAny<int?>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ShouldReturn404WhenServiceReturnsEmpty()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<FinanceSummaryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.GetFinanceSummaryAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string[]>(),
                It.IsAny<string[]>(),
                It.IsAny<string[]>(),
                It.IsAny<int?>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var result = await _function.RunAsync(CreateHttpRequestData(), "101");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}