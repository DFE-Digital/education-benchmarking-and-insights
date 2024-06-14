namespace Web.App.Infrastructure.Apis;

public interface IExpenditureApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null);
    Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null);
}

public class ExpenditureApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IExpenditureApi
{
    public async Task<ApiResult> School(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/expenditure/school/{urn}{query?.ToQueryString()}");
    }

    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/expenditure/school/{urn}/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null)
    {
        return await GetAsync($"api/expenditure/trust/{companyNo}{query?.ToQueryString()}");
    }

    public async Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null)
    {
        return await GetAsync($"api/expenditure/trust/{companyNo}/history{query?.ToQueryString()}");
    }
}