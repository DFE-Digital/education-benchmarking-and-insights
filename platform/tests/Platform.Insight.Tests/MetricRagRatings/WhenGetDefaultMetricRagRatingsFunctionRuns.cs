using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Insight.Features.MetricRagRatings;
using Platform.Api.Insight.Features.MetricRagRatings.Models;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Platform.Api.Insight.Features.MetricRagRatings.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings;

public class WhenGetDefaultMetricRagRatingsFunctionRuns : FunctionsTestBase
{
    private static readonly Fixture Fixture = new();
    private static readonly MetricRagRatingsParameters QueryParams = Fixture.Create<MetricRagRatingsParameters>();
    private readonly GetDefaultMetricRagRatingsFunction _function;

    private readonly Dictionary<string, StringValues> _query = new()
    {
        { nameof(QueryParams.Urns), QueryParams.Urns },
        { nameof(QueryParams.Categories), QueryParams.Categories },
        { nameof(QueryParams.Statuses), QueryParams.Statuses },
        { nameof(QueryParams.CompanyNumber), QueryParams.CompanyNumber },
        { nameof(QueryParams.LaCode), QueryParams.LaCode },
        { nameof(QueryParams.Phase), QueryParams.Phase }
    };

    private readonly Mock<IMetricRagRatingsService> _service = new();
    private readonly Mock<IValidator<MetricRagRatingsParameters>> _validator = new();

    public WhenGetDefaultMetricRagRatingsFunctionRuns()
    {
        _function = new GetDefaultMetricRagRatingsFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var results = Fixture.Build<MetricRagRating>().CreateMany().ToArray();

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<MetricRagRatingsParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.QueryAsync(QueryParams.Urns, QueryParams.Categories, QueryParams.Statuses, QueryParams.CompanyNumber, QueryParams.LaCode, QueryParams.Phase, It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(results);

        var result = await _function.RunAsync(CreateHttpRequestData(_query));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<MetricRagRating[]>();
        Assert.NotNull(body);
        Assert.Equal(results, body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<MetricRagRatingsParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(MetricRagRatingsParameters.Categories), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData(_query));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(MetricRagRatingsParameters.Categories));

        _service.Verify(
            x => x.QueryAsync(It.IsAny<string[]>(), It.IsAny<string[]>(), It.IsAny<string[]>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never());
    }
}