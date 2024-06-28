namespace Web.App.Infrastructure.Apis;

public interface IBudgetForecastApi
{
    Task<ApiResult> BudgetForecastReturns(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> BudgetForecastReturnsMetrics(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> GetCurrentBudgetForecastYear(string? companyNo, ApiQuery? query = null);
}

public class BudgetForecastApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IBudgetForecastApi
{
    public async Task<ApiResult> BudgetForecastReturns(string? companyNo, ApiQuery? query = null) =>
        await GetAsync($"api/budget-forecast/{companyNo}{query?.ToQueryString()}");

    public async Task<ApiResult> BudgetForecastReturnsMetrics(string? companyNo, ApiQuery? query = null) =>
        await GetAsync($"api/budget-forecast/{companyNo}/metrics{query?.ToQueryString()}");

    public async Task<ApiResult> GetCurrentBudgetForecastYear(string? companyNo, ApiQuery? query = null) =>
        await GetAsync($"api/budget-forecast/{companyNo}/current-year{query?.ToQueryString()}");
}