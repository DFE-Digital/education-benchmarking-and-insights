namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class InsightApi : BaseApi, IInsightApi
{
    public InsightApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }

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
}

public interface IInsightApi
{
    Task<ApiResult> GetMaintainedSchoolFinances(string urn);
    Task<ApiResult> GetAcademyFinances(string urn, ApiQuery? query = null);
    Task<ApiResult> GetSchoolsExpenditure(ApiQuery? query = null);
    Task<ApiResult> GetSchoolsWorkforce(ApiQuery? query = null);
}