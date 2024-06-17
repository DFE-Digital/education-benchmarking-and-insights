namespace Web.App.Infrastructure.Apis;

public interface IExpenditureApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null);
    Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> QuerySchools(ApiQuery? query = null);
    Task<ApiResult> QueryTrusts(ApiQuery? query = null);
}

public class ExpenditureApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IExpenditureApi
{
    public async Task<ApiResult> QuerySchools(ApiQuery? query = null) => await GetAsync($"api/expenditure/schools{query?.ToQueryString()}");

    public async Task<ApiResult> School(string? urn, ApiQuery? query = null) => await GetAsync($"api/expenditure/school/{urn}{query?.ToQueryString()}");

    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null) => await GetAsync($"api/expenditure/school/{urn}/history{query?.ToQueryString()}");

    public async Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null) => await GetAsync($"api/expenditure/trust/{companyNo}{query?.ToQueryString()}");

    public async Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null) => await GetAsync($"api/expenditure/trust/{companyNo}/history{query?.ToQueryString()}");

    public async Task<ApiResult> QueryTrusts(ApiQuery? query = null) => await GetAsync($"api/expenditure/trusts{query?.ToQueryString()}");
}