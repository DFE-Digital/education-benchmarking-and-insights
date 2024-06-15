namespace Web.App.Infrastructure.Apis;

public class UserDataApi(HttpClient httpClient, string? key = default)
    : ApiBase(httpClient, key), IUserDataApi
{
    public async Task<ApiResult> GetAsync(ApiQuery? query = null)
    {
        return await GetAsync($"api/user-data{query?.ToQueryString()}");
    }
}

public interface IUserDataApi
{
    Task<ApiResult> GetAsync(ApiQuery? query = null);
}