namespace Web.App.Infrastructure.Apis;

public class MetricRagRatingApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IMetricRagRatingApi
{
    public async Task<ApiResult> GetDefaultAsync(ApiQuery? query = null)
    {
        return await GetAsync($"api/metric-rag/default{query?.ToQueryString()}");
    }
}

public interface IMetricRagRatingApi
{
    Task<ApiResult> GetDefaultAsync(ApiQuery? query = null);
}