using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.BudgetForecast;
using Platform.Api.Insight.Features.BudgetForecast.Models;
using Platform.Api.Insight.Features.BudgetForecast.Responses;
using Platform.Api.Insight.Features.BudgetForecast.Services;
using Platform.Domain;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.BudgetForecast;

public class WhenGetBudgetForecastCurrentYearFunctionRuns : FunctionsTestBase
{
    private static readonly Fixture Fixture = new();
    private readonly GetBudgetForecastCurrentYearFunction _function;
    private readonly Mock<IBudgetForecastService> _service = new();

    public WhenGetBudgetForecastCurrentYearFunctionRuns()
    {
        _function = new GetBudgetForecastCurrentYearFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        const string companyNumber = nameof(companyNumber);
        const int year = 2021;

        _service
            .Setup(d => d.GetBudgetForecastCurrentYearAsync())
            .ReturnsAsync(year);

        var result = await _function.RunAsync(CreateHttpRequestData(), companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<int>();
        Assert.Equal(year, body);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        const string companyNumber = nameof(companyNumber);
        int? year = null;

        _service
            .Setup(d => d.GetBudgetForecastCurrentYearAsync())
            .ReturnsAsync(year);

        var result = await _function.RunAsync(CreateHttpRequestData(), companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}