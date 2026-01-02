namespace Web.App.Infrastructure.Apis.Insight;

[Obsolete(message: "Use SchoolApi instead.")]
public class MetricRagRatingApi(ISchoolApi schoolApi) : IMetricRagRatingApi
{
    public async Task<ApiResult> GetDefaultAsync(ApiQuery? query = null) => await schoolApi.QueryMetricRagRatingDetailsAsync(query);
    public async Task<ApiResult> UserDefinedAsync(string identifier) => await schoolApi.MetricRagRatingsUserDefinedAsync(identifier);
    public async Task<ApiResult> CustomAsync(string identifier) => await schoolApi.MetricRagRatingsUserDefinedAsync(identifier, new ApiQuery().AddIfNotNull("useCustomData", true));
    public async Task<ApiResult> SummaryAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => await schoolApi.QueryMetricRagRatingsAsync(query, cancellationToken);
}

[Obsolete(message: "Use ISchoolApi instead.")]
public interface IMetricRagRatingApi
{
    Task<ApiResult> GetDefaultAsync(ApiQuery? query = null);
    Task<ApiResult> UserDefinedAsync(string identifier);
    Task<ApiResult> CustomAsync(string identifier);
    Task<ApiResult> SummaryAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
}