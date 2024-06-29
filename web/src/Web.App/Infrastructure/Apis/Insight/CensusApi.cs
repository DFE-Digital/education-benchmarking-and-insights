namespace Web.App.Infrastructure.Apis.Insight;

public interface ICensusApi
{
    Task<ApiResult> Get(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetCustom(string? urn, string? identifier, ApiQuery? query = null);
    Task<ApiResult> History(string? urn, ApiQuery? query = null);
    Task<ApiResult> Query(ApiQuery? query = null);
}

public class CensusApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ICensusApi
{
    public async Task<ApiResult> Get(string? urn, ApiQuery? query = null) => await GetAsync($"{Api.Census.School(urn)}{query?.ToQueryString()}");
    public async Task<ApiResult> GetCustom(string? urn, string? identifier, ApiQuery? query = null) => await GetAsync($"{Api.Census.SchoolCustom(urn, identifier)}{query?.ToQueryString()}");
    public async Task<ApiResult> History(string? urn, ApiQuery? query = null) => await GetAsync($"{Api.Census.SchoolHistory(urn)}{query?.ToQueryString()}");
    public async Task<ApiResult> Query(ApiQuery? query = null) => await GetAsync($"{Api.Census.Schools}{query?.ToQueryString()}");
}