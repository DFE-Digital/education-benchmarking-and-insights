namespace Web.App.Infrastructure.Apis;

public class SchoolInsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ISchoolInsightApi
{
    public async Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null)
    {
        return await GetAsync($"api/schools/characteristics{query?.ToQueryString()}");
    }
    public async Task<ApiResult> GetCharacteristicsAsync(string urn)
    {
        return await GetAsync($"api/school/{urn}/characteristics");
    }
}

public interface ISchoolInsightApi
{
    Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null);
    Task<ApiResult> GetCharacteristicsAsync(string urn);
}