namespace Web.App.Infrastructure.Apis;

public class InsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IInsightApi
{
    public async Task<ApiResult> GetCurrentReturnYears()
    {
        return await GetAsync("api/current-return-years");
    }
}

public interface IInsightApi
{
    Task<ApiResult> GetCurrentReturnYears();
}