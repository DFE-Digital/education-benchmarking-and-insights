namespace Web.App.Infrastructure.Apis.Insight;

public class InsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IInsightApi
{
    public async Task<ApiResult> GetCurrentReturnYears() => await GetAsync(Api.Insight.CurrentReturnYears);

    public async Task<ApiResult> Test429() => await GetAsync("api/429");
}

public interface IInsightApi
{
    Task<ApiResult> GetCurrentReturnYears();
    Task<ApiResult> Test429();
}