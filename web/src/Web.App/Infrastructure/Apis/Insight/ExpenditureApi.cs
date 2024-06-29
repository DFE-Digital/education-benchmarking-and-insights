namespace Web.App.Infrastructure.Apis.Insight;

public interface IExpenditureApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null);
    Task<ApiResult> SchoolCustom(string? urn, string? identifier, ApiQuery? query = null);
    Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> QuerySchools(ApiQuery? query = null);
    Task<ApiResult> QueryTrusts(ApiQuery? query = null);
}

public class ExpenditureApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IExpenditureApi
{
    public async Task<ApiResult> QuerySchools(ApiQuery? query = null) => await GetAsync($"{Api.Expenditure.Schools}{query?.ToQueryString()}");
    public async Task<ApiResult> School(string? urn, ApiQuery? query = null) => await GetAsync($"{Api.Expenditure.School(urn)}{query?.ToQueryString()}");
    public async Task<ApiResult> SchoolCustom(string? urn, string? identifier, ApiQuery? query = null) => await GetAsync($"{Api.Expenditure.SchoolCustom(urn, identifier)}{query?.ToQueryString()}");
    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null) => await GetAsync($"{Api.Expenditure.SchoolHistory(urn)}{query?.ToQueryString()}");
    public async Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null) => await GetAsync($"{Api.Expenditure.Trust(companyNo)}{query?.ToQueryString()}");
    public async Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null) => await GetAsync($"{Api.Expenditure.TrustHistory(companyNo)}{query?.ToQueryString()}");
    public async Task<ApiResult> QueryTrusts(ApiQuery? query = null) => await GetAsync($"{Api.Expenditure.Trusts}{query?.ToQueryString()}");
}