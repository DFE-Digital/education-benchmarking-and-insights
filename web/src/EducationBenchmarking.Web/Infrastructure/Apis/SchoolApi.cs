namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class SchoolApi : BaseApi, ISchoolApi
{
    private const string SchoolsRoute = "api/schools";
    
    public SchoolApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }
    
    public async Task<ApiResult> Query(ApiQuery? query = null)
    {
        return await GetAsync($"{SchoolsRoute}{query?.ToQueryString()}");
    }
}

public interface ISchoolApi
{
    Task<ApiResult> Query(ApiQuery? query = null);
}