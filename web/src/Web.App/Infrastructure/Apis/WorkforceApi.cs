namespace Web.App.Infrastructure.Apis;

public interface IWorkforceApi
{
    Task<ApiResult> History(string? urn, ApiQuery? query = null);
    Task<ApiResult> Query(ApiQuery? query = null);
}

public class WorkforceApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IWorkforceApi
{
    public async Task<ApiResult> History(string? urn, ApiQuery? query = null)
    {
        return await GetAsync($"api/workforce/{urn}/history{query?.ToQueryString()}");
    }

    public async Task<ApiResult> Query(ApiQuery? query = null)
    {
        return await GetAsync($"api/workforce{query?.ToQueryString()}");
    }
}