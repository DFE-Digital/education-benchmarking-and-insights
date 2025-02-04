namespace Web.App.Infrastructure.Apis.Insight;

public class InsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IInsightApi
{
    public async Task<ApiResult> GetCurrentReturnYears()
    {
        return await GetAsync(Api.Insight.CurrentReturnYears);
    }
}

public interface IInsightApi
{
    Task<ApiResult> GetCurrentReturnYears();
}