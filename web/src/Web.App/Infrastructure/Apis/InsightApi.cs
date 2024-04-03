namespace Web.App.Infrastructure.Apis;

public class InsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IInsightApi
{
    public async Task<ApiResult> GetSchoolFinances(string? urn)
    {
        return await GetAsync($"api/school/{urn}");
    }

    public async Task<ApiResult> GetSchoolWorkforceHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/school/{urn}/workforce/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetSchoolBalanceHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/school/{urn}/balance/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetSchoolIncomeHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/school/{urn}/income/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetSchoolExpenditureHistory(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/school/{urn}/expenditure/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetTrustBalanceHistory(string? companyNo, ApiQuery? query = null)
    {
        return await GetAsync($"api/trust/{companyNo}/balance/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetTrustIncomeHistory(string? companyNo, ApiQuery? query = null)
    {
        return await GetAsync($"api/trust/{companyNo}/income/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetTrustExpenditureHistory(string? companyNo, ApiQuery? query = null)
    {
        return await GetAsync($"api/trust/{companyNo}/expenditure/history{query?.ToQueryString()}");
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
    Task<ApiResult> GetSchoolFinances(string? urn);
    Task<ApiResult> GetSchoolWorkforceHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetSchoolBalanceHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetSchoolIncomeHistory(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetSchoolExpenditureHistory(string? urn, ApiQuery? query = null);

    Task<ApiResult> GetSchoolsExpenditure(ApiQuery? query = null);
    Task<ApiResult> GetSchoolsWorkforce(ApiQuery? query = null);

    Task<ApiResult> GetTrustBalanceHistory(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> GetTrustIncomeHistory(string? companyNo, ApiQuery? query = null);
    Task<ApiResult> GetTrustExpenditureHistory(string? companyNo, ApiQuery? query = null);

    Task<ApiResult> GetCurrentReturnYears();

}