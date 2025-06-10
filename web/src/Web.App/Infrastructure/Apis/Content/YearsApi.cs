namespace Web.App.Infrastructure.Apis.Content;

public class YearsApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IYearsApi
{
    public async Task<ApiResult> GetCurrentReturnYears()
    {
        return await GetAsync(Api.Years.CurrentReturnYears);
    }
}

public interface IYearsApi
{
    Task<ApiResult> GetCurrentReturnYears();
}