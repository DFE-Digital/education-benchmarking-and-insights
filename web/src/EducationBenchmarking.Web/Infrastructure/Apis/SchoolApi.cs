namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class SchoolApi : BaseApi, ISchoolApi
{
    public SchoolApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }
    
    public async Task<ApiResult> QueryExpenditure(ApiQuery? query = null)
    {
        return await GetAsync($"api/school-expenditure{query?.ToQueryString()}");
    }
}

public interface ISchoolApi
{
    Task<ApiResult> QueryExpenditure(ApiQuery? query = null);
}