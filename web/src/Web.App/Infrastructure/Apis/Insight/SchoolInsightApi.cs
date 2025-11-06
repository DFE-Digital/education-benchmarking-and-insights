namespace Web.App.Infrastructure.Apis.Insight;

public class SchoolInsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ISchoolInsightApi
{
    public async Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.SchoolInsight.SchoolsCharacteristics}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> GetCharacteristicsAsync(string urn, CancellationToken cancellationToken = default) => await GetAsync(Api.SchoolInsight.SchoolCharacteristics(urn), cancellationToken);
}

public interface ISchoolInsightApi
{
    Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetCharacteristicsAsync(string urn, CancellationToken cancellationToken = default);
}