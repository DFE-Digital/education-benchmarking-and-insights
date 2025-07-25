using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.ItSpend;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Api.Insight.Features.ItSpend.Responses;
using Platform.Api.Insight.Features.ItSpend.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.ItSpend;

public class GetItSpendSchoolsFunctionTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetItSpendSchoolsFunction _function;
    private readonly Mock<IItSpendService> _service;
    private readonly Mock<IValidator<ItSpendSchoolsParameters>> _validator;

    public GetItSpendSchoolsFunctionTests()
    {
        _service = new Mock<IItSpendService>();
        _validator = new Mock<IValidator<ItSpendSchoolsParameters>>();
        _fixture = new Fixture();
        _function = new GetItSpendSchoolsFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<ItSpendSchoolResponse>().CreateMany();
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ItSpendSchoolsParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.GetSchoolsAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<ItSpendSchoolResponse[]>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ItSpendSchoolsParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(ItSpendSchoolsParameters.Dimension), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(ItSpendSchoolsParameters.Dimension));

        _service
            .Verify(s => s.GetSchoolsAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}