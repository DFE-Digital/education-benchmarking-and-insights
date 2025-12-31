namespace Web.App.Infrastructure.Apis;

public class HealthApi(HttpClient httpClient, string? key = null) : ApiBase(httpClient, key), IHealthApi
{
    private const string HealthPath = "api/health";

    public Task<ApiResult> GetHealth() => GetAsync(HealthPath);
}

public interface IHealthApi
{
    Task<ApiResult> GetHealth();
}