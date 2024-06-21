using Web.App.Domain;
namespace Web.App.Infrastructure.Apis;

public interface IBalanceApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null);
    Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> QueryTrusts(ApiQuery? query = null);
    Task<ApiResult> BudgetForecastReturns(string? companyNo);
    Task<ApiResult> BudgetForecastReturnsMetrics(string? companyNo);
}

public class BalanceApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IBalanceApi
{
    public async Task<ApiResult> School(string? urn, ApiQuery? query = null) => await GetAsync($"api/balance/school/{urn}{query?.ToQueryString()}");

    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null) => await GetAsync($"api/balance/school/{urn}/history{query?.ToQueryString()}");

    public async Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null) => await GetAsync($"api/balance/trust/{companyNo}{query?.ToQueryString()}");

    public async Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null) => await GetAsync($"api/balance/trust/{companyNo}/history{query?.ToQueryString()}");

    public async Task<ApiResult> QueryTrusts(ApiQuery? query = null) => await GetAsync($"api/balance/trusts{query?.ToQueryString()}");

    // todo: replace stub with actual API call
    public Task<ApiResult> BudgetForecastReturns(string? companyNo) => Task.FromResult(ApiResult.Ok(new[]
    {
        new BudgetForecastReturn
        {
            Year = 2023,
            Forecast = 211_000_000,
            Actual = 219_000_000
        },
        new BudgetForecastReturn
        {
            Year = 2021,
            Forecast = 277_000_000,
            Actual = 412_000_000
        },
        new BudgetForecastReturn
        {
            Year = 2022,
            Forecast = 487_000_000,
            Actual = 609_000_000
        },
        new BudgetForecastReturn
        {
            Year = 2023,
            Forecast = 265_000_000
        },
        new BudgetForecastReturn
        {
            Year = 2024,
            Forecast = 264_000_000
        },
        new BudgetForecastReturn
        {
            Year = 2025,
            Forecast = 286_000_000
        }
    }));

    // todo: replace stub with actual API call
    public Task<ApiResult> BudgetForecastReturnsMetrics(string? companyNo) => Task.FromResult(ApiResult.Ok(new[]
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