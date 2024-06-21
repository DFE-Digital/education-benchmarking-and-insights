using Web.App.Domain;
namespace Web.App.Infrastructure.Apis;

public interface IBudgetForecastApi
{
    Task<ApiResult> BudgetForecastReturns(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> BudgetForecastReturnsMetrics(string? companyNo, ApiQuery? query = null);
}

public class BudgetForecastApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IBudgetForecastApi
{
    // todo: replace stub with actual API call
    // e.g. await GetAsync($"api/budget-forecast/{companyNo}{query?.ToQueryString()}");
    public Task<ApiResult> BudgetForecastReturns(string? companyNo, ApiQuery? query = null) => Task.FromResult(ApiResult.Ok(new[]
    {
        new BudgetForecastReturn
        {
            Year = 2020,
            Forecast = 211_000_000,
            Actual = 219_000_000,
            TotalPupils = 11_000,
            PercentVariance = 15
        },
        new BudgetForecastReturn
        {
            Year = 2021,
            Forecast = 277_000_000,
            Actual = 412_000_000,
            TotalPupils = 12_200,
            PercentVariance = 14
        },
        new BudgetForecastReturn
        {
            Year = 2022,
            Forecast = 487_000_000,
            Actual = 609_000_000,
            TotalPupils = 13_300,
            PercentVariance = 13
        },
        new BudgetForecastReturn
        {
            Year = 2023,
            Forecast = 265_000_000,
            TotalPupils = 14_400
        },
        new BudgetForecastReturn
        {
            Year = 2024,
            Forecast = 264_000_000,
            TotalPupils = 15_500
        },
        new BudgetForecastReturn
        {
            Year = 2025,
            Forecast = 286_000_000,
            TotalPupils = 16_600
        }
    }));

    // todo: replace stub with actual API call
    // e.g. await GetAsync($"api/budget-forecast/{companyNo}/metrics{query?.ToQueryString()}");
    public Task<ApiResult> BudgetForecastReturnsMetrics(string? companyNo, ApiQuery? query = null) => Task.FromResult(ApiResult.Ok(new[]
    {
        new BudgetForecastReturnMetric
        {
            Year = 2022,
            Metric = "Revenue reserve as percentage of income",
            Value = 1.2m
        },
        new BudgetForecastReturnMetric
        {
            Year = 2022,
            Metric = "Staff costs as percentage of income",
            Value = 2.3m
        },
        new BudgetForecastReturnMetric
        {
            Year = 2022,
            Metric = "Expenditure as percentage of income",
            Value = 3.4m
        },
        new BudgetForecastReturnMetric
        {
            Year = 2022,
            Metric = "Self-generated funding as percentage of income",
            Value = 4.5m
        },
        new BudgetForecastReturnMetric
        {
            Year = 2022,
            Metric = "Grant funding as percentage of income",
            Value = 5.6m
        }
    }));
}