using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.NonFinancial.Tests.EducationHealthCarePlans;

public class GetEducationHealthCarePlansLocalAuthoritiesFunctionTests : FunctionsTestBase
{
    private const string Code = nameof(Code);
    private readonly Fixture _fixture;
    private readonly GetEducationHealthCarePlansLocalAuthoritiesFunction _function;
    private readonly Mock<IEducationHealthCarePlansService> _service;
    private readonly Mock<IValidator<EducationHealthCarePlansParameters>> _validator;

    public GetEducationHealthCarePlansLocalAuthoritiesFunctionTests()
    {
        _service = new Mock<IEducationHealthCarePlansService>();
        _validator = new Mock<IValidator<EducationHealthCarePlansParameters>>();
        _function = new GetEducationHealthCarePlansLocalAuthoritiesFunction(_service.Object, _validator.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var models = _fixture
            .Build<LocalAuthorityNumberOfPlans>()
            .CreateMany()
            .ToArray();
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<EducationHealthCarePlansParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(x => x.Get(new[] { Code }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(models);

        var query = new Dictionary<string, StringValues>
        {
            { "code", Code }
        };

        var result = await _function.EducationHealthCarePlans(CreateHttpRequestData(query), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<LocalAuthorityNumberOfPlans[]>();
        Assert.NotNull(body);
        Assert.Equivalent(models, body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<EducationHealthCarePlansParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(EducationHealthCarePlansParameters.Codes), "error")
            ]));

        var result = await _function.EducationHealthCarePlans(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(EducationHealthCarePlansParameters.Codes));

        _service
            .Verify(d => d.Get(It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}