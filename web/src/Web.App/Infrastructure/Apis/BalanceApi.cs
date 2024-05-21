namespace Web.App.Infrastructure.Apis;

public interface IBalanceApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null);
}

public class BalanceApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IBalanceApi
{
    public async Task<ApiResult> School(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/balance/school/{urn}{query?.ToQueryString()}");
    }

    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/balance/school/{urn}/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null)
    {
        return await GetAsync($"api/balance/trust/{companyNo}/history{query?.ToQueryString()}");
    }
}