using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.MetricRagRatings;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Platform.Api.Insight.Features.MetricRagRatings.Models;
using Platform.Api.Insight.Features.MetricRagRatings.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings;

public class WhenGetDefaultMetricRagRatingsSummaryFunctionTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetDefaultMetricRagRatingsSummaryFunction _function;
    private readonly Mock<IMetricRagRatingsService> _service;
    private readonly Mock<IValidator<MetricRagRatingSummaryParameters>> _validator;

    public WhenGetDefaultMetricRagRatingsSummaryFunctionTests()
    {
        _service = new Mock<IMetricRagRatingsService>();
        _validator = new Mock<IValidator<MetricRagRatingSummaryParameters>>();
        _fixture = new Fixture();
        _function = new GetDefaultMetricRagRatingsSummaryFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<MetricRagRatingSummary>().CreateMany();
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<MetricRagRatingSummaryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.QuerySummaryAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<MetricRagRatingSummary[]>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<MetricRagRatingSummaryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(MetricRagRatingSummaryParameters.LaCode), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(MetricRagRatingSummaryParameters.LaCode));

        _service
            .Verify(s => s.QuerySummaryAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}