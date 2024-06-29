namespace Web.App.Infrastructure.Apis;

public class HealthApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IHealthApi
{
    public Task<ApiResult> GetHealth()
    {
        return GetAsync("api/health");
    }
}

public interface IHealthApi
{
    Task<ApiResult> GetHealth();
}