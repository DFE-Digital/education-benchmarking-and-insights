using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.MetricRagRatings;
using Platform.Api.Insight.Features.MetricRagRatings.Models;
using Platform.Api.Insight.Features.MetricRagRatings.Services;
using Platform.Domain;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings;

public class WhenGetUserDefinedMetricRagRatingsFunctionRuns : FunctionsTestBase
{
    private static readonly Fixture Fixture = new();
    private readonly GetUserDefinedMetricRagRatingsFunction _function;
    private readonly Mock<IMetricRagRatingsService> _service = new();

    public WhenGetUserDefinedMetricRagRatingsFunctionRuns()
    {
        _function = new GetUserDefinedMetricRagRatingsFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var results = Fixture.Build<MetricRagRating>().CreateMany().ToArray();
        const string identifier = nameof(identifier);

        _service
            .Setup(d => d.UserDefinedAsync(identifier, Pipeline.RunType.Default, It.IsAny<bool>()))
            .ReturnsAsync(results);

        var result = await _function.RunAsync(CreateHttpRequestData(), identifier);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<MetricRagRating[]>();
        Assert.NotNull(body);
        Assert.Equal(results, body);
    }
}