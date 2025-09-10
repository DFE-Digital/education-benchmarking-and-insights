namespace Web.App.Infrastructure.Apis.Insight;

public interface IBudgetForecastApi
{
    Task<ApiResult> BudgetForecastReturns(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> BudgetForecastReturnsMetrics(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetCurrentBudgetForecastYear(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default);
}

public class BudgetForecastApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IBudgetForecastApi
{
    public async Task<ApiResult> BudgetForecastReturns(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"api/budget-forecast/{companyNo}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> BudgetForecastReturnsMetrics(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"api/budget-forecast/{companyNo}/metrics{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> GetCurrentBudgetForecastYear(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"api/budget-forecast/{companyNo}/current-year{query?.ToQueryString()}", cancellationToken);
}