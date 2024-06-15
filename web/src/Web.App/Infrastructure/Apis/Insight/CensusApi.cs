namespace Web.App.Infrastructure.Apis;

public interface ICensusApi
{
    Task<ApiResult> Get(string? urn, ApiQuery? query = null);
    Task<ApiResult> History(string? urn, ApiQuery? query = null);
    Task<ApiResult> Query(ApiQuery? query = null);
}

public class CensusApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ICensusApi
{
    public async Task<ApiResult> Get(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/census/{urn}{query?.ToQueryString()}");
    }

    public async Task<ApiResult> History(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/census/{urn}/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> Query(ApiQuery? query = null)
    {
        return await GetAsync($"api/census{query?.ToQueryString()}");
    }
}