using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Handlers;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.EducationHealthCarePlans.Handlers;

public class QueryEducationHealthCarePlansHistoryV1HandlerTests : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IEducationHealthCarePlansService> _service = new();
    private readonly Mock<IValidator<EducationHealthCarePlansParameters>> _validator = new();
    private readonly QueryEducationHealthCarePlansHistoryV1Handler _handler;

    public QueryEducationHealthCarePlansHistoryV1HandlerTests()
    {
        _handler = new QueryEducationHealthCarePlansHistoryV1Handler(_service.Object, _validator.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var years = _fixture.Create<YearsModelDto>();
        var dtos = _fixture.CreateMany<EducationHealthCarePlansYearDto>().ToList();
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues>
        {
            { "code", "LA1" },
            { "dimension", "Total" }
        };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<EducationHealthCarePlansParameters>(), token))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.QueryHistoryAsync(It.IsAny<string[]>(), It.IsAny<string>(), token))
            .ReturnsAsync((years, dtos));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<EducationHealthCarePlansYearHistory>();
        Assert.NotNull(body);
        Assert.Equal(years.StartYear, body.StartYear);
        Assert.Equal(years.EndYear, body.EndYear);
        if (body.Plans != null)
        {
            Assert.Equal(dtos.Count, body.Plans.Length);
        }
    }

    [Fact]
    public async Task ShouldReturn404WhenYearsNotFound()
    {
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues>
        {
            { "code", "LA1" }
        };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<EducationHealthCarePlansParameters>(), token))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.QueryHistoryAsync(It.IsAny<string[]>(), It.IsAny<string>(), token))
            .ReturnsAsync((null, Enumerable.Empty<EducationHealthCarePlansYearDto>()));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues>
        {
            { "code", "" }
        };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        var validationFailures = new[] { new ValidationFailure("Codes", "Error message") };
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<EducationHealthCarePlansParameters>(), token))
            .ReturnsAsync(new ValidationResult(validationFailures));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}