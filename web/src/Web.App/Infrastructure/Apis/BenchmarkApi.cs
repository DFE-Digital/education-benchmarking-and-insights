namespace Web.App.Infrastructure.Apis;

public class BenchmarkApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IBenchmarkApi
{
    public async Task<ApiResult> GetComparatorSet(string? urn)
    {
        return await GetAsync($"api/comparator-set/{urn}/default");
    }

    public async Task<ApiResult> UpsertFinancialPlan(PutFinancialPlanRequest request)
    {
        return await PutAsync($"api/financial-plan/{request.Urn}/{request.Year}", new JsonContent(request));
    }

    public async Task<ApiResult> GetFinancialPlan(string? urn, int? year)
    {
        return await GetAsync($"api/financial-plan/{urn}/{year}");
    }

    public async Task<ApiResult> QueryFinancialPlan(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/financial-plan/{urn}{query?.ToQueryString()}");
    }
}

public interface IBenchmarkApi
{
    Task<ApiResult> GetComparatorSet(string? urn);
    Task<ApiResult> UpsertFinancialPlan(PutFinancialPlanRequest request);
    Task<ApiResult> GetFinancialPlan(string? urn, int? year);
    Task<ApiResult> QueryFinancialPlan(string? urn, ApiQuery? query = null);
}