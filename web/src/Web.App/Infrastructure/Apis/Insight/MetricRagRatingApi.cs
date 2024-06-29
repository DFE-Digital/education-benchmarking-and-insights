namespace Web.App.Infrastructure.Apis.Insight;

public class MetricRagRatingApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IMetricRagRatingApi
{
    public async Task<ApiResult> GetDefaultAsync(ApiQuery? query = null)
    {
        return await GetAsync($"{Api.MetricRagRating.Default}{query?.ToQueryString()}");
    }

    public async Task<ApiResult> UserDefinedAsync(string identifier)
    {
        return await GetAsync(Api.MetricRagRating.Single(identifier));
    }

    public async Task<ApiResult> CustomAsync(string identifier)
    {
        var query = new ApiQuery().AddIfNotNull("useCustomData", true);
        return await GetAsync($"{Api.MetricRagRating.Single(identifier)}{query.ToQueryString()}");
    }
}

public interface IMetricRagRatingApi
{
    Task<ApiResult> GetDefaultAsync(ApiQuery? query = null);
    Task<ApiResult> UserDefinedAsync(string identifier);
    Task<ApiResult> CustomAsync(string identifier);
}