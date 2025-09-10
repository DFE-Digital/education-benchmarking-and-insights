namespace Web.App.Infrastructure.Apis.Insight;

public interface ICensusApi
{
    Task<ApiResult> Get(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetCustom(string? urn, string? identifier, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SchoolHistoryComparatorSetAverage(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SchoolHistoryNationalAverage(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> Query(ApiQuery? query = null, CancellationToken cancellationToken = default);
}

public class CensusApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ICensusApi
{
    public async Task<ApiResult> Get(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Census.School(urn)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> GetCustom(string? urn, string? identifier, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Census.SchoolCustom(urn, identifier)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Census.SchoolHistory(urn)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> SchoolHistoryComparatorSetAverage(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Census.SchoolHistoryComparatorSetAverage(urn)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> SchoolHistoryNationalAverage(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Census.SchoolHistoryNationalAverage}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> Query(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Census.Schools}{query?.ToQueryString()}", cancellationToken);
}