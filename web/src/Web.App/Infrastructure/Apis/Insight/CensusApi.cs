namespace Web.App.Infrastructure.Apis;

public interface ICensusApi
{
    Task<ApiResult> Get(string? urn, ApiQuery? query = null);
    Task<ApiResult> GetCustom(string? urn, string? identifier, ApiQuery? query = null);
    Task<ApiResult> History(string? urn, ApiQuery? query = null);
    Task<ApiResult> Query(ApiQuery? query = null);
}

public class CensusApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ICensusApi
{
    public async Task<ApiResult> Get(string? urn, ApiQuery? query = null) => await GetAsync($"api/census/{urn}{query?.ToQueryString()}");
    public async Task<ApiResult> GetCustom(string? urn, string? identifier, ApiQuery? query = null) => await GetAsync($"api/census/{urn}/custom/{identifier}{query?.ToQueryString()}");
    public async Task<ApiResult> History(string? urn, ApiQuery? query = null) => await GetAsync($"api/census/{urn}/history{query?.ToQueryString()}");
    public async Task<ApiResult> Query(ApiQuery? query = null) => await GetAsync($"api/census{query?.ToQueryString()}");
}