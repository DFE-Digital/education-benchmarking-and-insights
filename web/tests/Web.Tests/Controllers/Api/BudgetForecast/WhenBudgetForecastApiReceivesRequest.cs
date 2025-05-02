using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App.Controllers.Api;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Xunit;

namespace Web.Tests.Controllers.Api.BudgetForecast;

public class WhenBudgetForecastApiReceivesRequest
{
    private readonly BudgetForecastProxyController _api;
    private readonly Mock<IBudgetForecastApi> _budgetForecastApi = new();
    private readonly NullLogger<BudgetForecastProxyController> _logger = new();

    public WhenBudgetForecastApiReceivesRequest()
    {
        _api = new BudgetForecastProxyController(_logger, _budgetForecastApi.Object);
    }

    [Theory]
    [InlineData("companyNumber", 2023, "?runId=2023")]
    public async Task ShouldGetBudgetForecastReturns(string companyNumber, int year, string expectedQuery)
    {
        // arrange
        var metrics = new BudgetForecastReturnMetric[]
        {
            new()
            {
                Year = year - 1
            },
            new()
            {
                Year = year
            },
            new()
            {
                Year = year - 2
            }
        };

        var results = Array.Empty<BudgetForecastReturn>();
        var actualQuery = string.Empty;

        _budgetForecastApi
            .Setup(e => e.BudgetForecastReturnsMetrics(companyNumber, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(metrics));
        _budgetForecastApi
            .Setup(e => e.BudgetForecastReturns(companyNumber, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .Callback<string, ApiQuery?, CancellationToken>((_, query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.Index(companyNumber);

        // assert
        var json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json);
        Assert.Equal(expectedQuery, actualQuery);
    }
}