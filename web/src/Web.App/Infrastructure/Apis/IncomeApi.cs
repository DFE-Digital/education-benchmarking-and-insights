namespace Web.App.Infrastructure.Apis;

public interface IIncomeApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null);
}

public class IncomeApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IIncomeApi
{
    public async Task<ApiResult> School(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/income/school/{urn}{query?.ToQueryString()}");
    }

    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/income/school/{urn}/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null)
    {
        return await GetAsync($"api/income/trust/{companyNo}/history{query?.ToQueryString()}");
    }
}