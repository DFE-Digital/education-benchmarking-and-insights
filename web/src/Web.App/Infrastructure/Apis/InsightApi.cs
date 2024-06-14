namespace Web.App.Infrastructure.Apis;

public class InsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IInsightApi
{
    public async Task<ApiResult> GetSchoolFinances(string? urn)
    {
        return await GetAsync($"api/school/{urn}");
    }

    public async Task<ApiResult> GetSchoolsExpenditure(ApiQuery? query = null)
    {
        return await GetAsync($"api/schools/expenditure{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetSchoolFinances(ApiQuery? query = null)
    {
        return await GetAsync($"api/schools{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetCurrentReturnYears()
    {
        return await GetAsync("api/current-return-years");
    }

    public async Task<ApiResult> GetSchoolFloorAreaMetric(string? urn)
    {
        return await GetAsync($"api/metric/{urn}/floor-area");
    }
}

public interface IInsightApi
{
    Task<ApiResult> GetSchoolFinances(string? urn);
    Task<ApiResult> GetSchoolFinances(ApiQuery? query = null);
    Task<ApiResult> GetSchoolsExpenditure(ApiQuery? query = null);
    Task<ApiResult> GetCurrentReturnYears();
    Task<ApiResult> GetSchoolFloorAreaMetric(string? urn);
}