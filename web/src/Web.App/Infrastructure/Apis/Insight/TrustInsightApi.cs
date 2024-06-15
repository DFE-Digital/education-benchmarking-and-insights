namespace Web.App.Infrastructure.Apis;

public class TrustInsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ITrustInsightApi
{
    public async Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null) => await GetAsync($"api/trusts/characteristics{query?.ToQueryString()}");
}

public interface ITrustInsightApi
{
    Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null);
}