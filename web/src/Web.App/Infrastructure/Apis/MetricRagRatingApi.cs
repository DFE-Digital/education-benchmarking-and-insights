namespace Web.App.Infrastructure.Apis;

public class MetricRagRatingApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IMetricRagRatingApi
{
    public async Task<ApiResult> GetDefaultAsync(ApiQuery? query = null)
    {
        return await GetAsync($"api/metric-rag/default{query?.ToQueryString()}");
    }

    public async Task<ApiResult> UserDefinedAsync(string identifier)
    {
        return await GetAsync($"api/metric-rag/{identifier}");
    }
}

public interface IMetricRagRatingApi
{
    Task<ApiResult> GetDefaultAsync(ApiQuery? query = null);
    Task<ApiResult> UserDefinedAsync(string identifier);
}