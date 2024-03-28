namespace Web.App.Infrastructure.Apis;

public class InsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IInsightApi
{
    public async Task<ApiResult> GetMaintainedSchoolFinances(string? urn)
    {
        return await GetAsync($"api/maintained-school/{urn}");
    }

    public async Task<ApiResult> GetMaintainedSchoolWorkforceHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/maintained-school/{urn}/workforce/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetMaintainedSchoolBalanceHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/maintained-school/{urn}/balance/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetAcademyFinances(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/academy/{urn}{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetAcademyWorkforceHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/academy/{urn}/workforce/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetAcademyBalanceHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/academy/{urn}/balance/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetSchoolsExpenditure(ApiQuery? query = null)
    {
        return await GetAsync($"api/schools/expenditure{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetSchoolsWorkforce(ApiQuery? query = null)
    {
        return await GetAsync($"api/schools/workforce{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetCurrentReturnYears()
    {
        return await GetAsync("api/current-return-years");
    }
}

public interface IInsightApi
{
    Task<ApiResult> GetMaintainedSchoolFinances(string? urn);
    Task<ApiResult> GetMaintainedSchoolWorkforceHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetMaintainedSchoolBalanceHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetAcademyFinances(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetAcademyWorkforceHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetAcademyBalanceHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetSchoolsExpenditure(ApiQuery? query = null);
    Task<ApiResult> GetSchoolsWorkforce(ApiQuery? query = null);
    Task<ApiResult> GetCurrentReturnYears();

}