namespace Web.App.Infrastructure.Apis.Benchmark;

public class UserDataApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IUserDataApi
{
    public async Task<ApiResult> GetAsync(ApiQuery? query = null)
    {
        return await GetAsync($"{Api.UserData.All}{query?.ToQueryString()}");
    }
}

public interface IUserDataApi
{
    Task<ApiResult> GetAsync(ApiQuery? query = null);
}