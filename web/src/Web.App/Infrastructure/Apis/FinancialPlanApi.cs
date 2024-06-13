namespace Web.App.Infrastructure.Apis;

public class FinancialPlanApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IFinancialPlanApi
{
    public async Task<ApiResult> UpsertAsync(PutFinancialPlanRequest request)
    {
        return await PutAsync($"api/financial-plan/{request.Urn}/{request.Year}", new JsonContent(request));
    }

    public async Task<ApiResult> GetAsync(string? urn, int? year)
    {
        return await GetAsync($"api/financial-plan/{urn}/{year}");
    }

    public async Task<ApiResult> GetDeploymentPlanAsync(string? urn, int? year)
    {
        return await GetAsync($"api/financial-plan/{urn}/{year}/deployment");
    }

    public async Task<ApiResult> QueryAsync(ApiQuery? query = null)
    {
        return await GetAsync($"api/financial-plans{query?.ToQueryString()}");
    }
}

public interface IFinancialPlanApi
{
    Task<ApiResult> UpsertAsync(PutFinancialPlanRequest request);
    Task<ApiResult> GetAsync(string? urn, int? year);
    Task<ApiResult> QueryAsync(ApiQuery? query = null);
    Task<ApiResult> GetDeploymentPlanAsync(string? urn, int? year);
}