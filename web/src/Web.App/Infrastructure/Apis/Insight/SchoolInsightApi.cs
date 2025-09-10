namespace Web.App.Infrastructure.Apis.Insight;

public class SchoolInsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ISchoolInsightApi
{
    public async Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null) => await GetAsync($"{Api.SchoolInsight.SchoolsCharacteristics}{query?.ToQueryString()}");

    public async Task<ApiResult> GetCharacteristicsAsync(string urn) => await GetAsync(Api.SchoolInsight.SchoolCharacteristics(urn));
}

public interface ISchoolInsightApi
{
    Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null);
    Task<ApiResult> GetCharacteristicsAsync(string urn);
}