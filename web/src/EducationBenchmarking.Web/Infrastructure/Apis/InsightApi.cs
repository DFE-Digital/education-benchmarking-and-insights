namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class InsightApi(HttpClient httpClient, string? key = default) : BaseApi(httpClient, key), IInsightApi
{
    public async Task<ApiResult> GetMaintainedSchoolFinances(string urn)
    {
        return await GetAsync($"api/maintained-school/{urn}");
    }

    public async Task<ApiResult> GetAcademyFinances(string urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/academy/{urn}{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetSchoolsExpenditure(ApiQuery? query = null)
    {
        return await GetAsync($"api/schools/expenditure{query?.ToQueryString()}");
    }
    
    public async Task<ApiResult> GetSchoolsWorkforce(ApiQuery? query = null)
    {
        return await GetAsync($"api/schools/workforce{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetSchoolsRatings(ApiQuery? query = null)
    {
        return await GetAsync($"api/schools/ratings{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetFinanceYears()
    {
        return await GetAsync("api/finance-years");
    }
}

public interface IInsightApi
{
    Task<ApiResult> GetMaintainedSchoolFinances(string urn);
    Task<ApiResult> GetAcademyFinances(string urn, ApiQuery? query = null);
    Task<ApiResult> GetSchoolsExpenditure(ApiQuery? query = null);
    Task<ApiResult> GetSchoolsWorkforce(ApiQuery? query = null);
    Task<ApiResult> GetSchoolsRatings(ApiQuery? query = null);
    Task<ApiResult> GetFinanceYears();
    
}