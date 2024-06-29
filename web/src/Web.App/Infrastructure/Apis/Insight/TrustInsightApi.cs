namespace Web.App.Infrastructure.Apis.Insight;

public class TrustInsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ITrustInsightApi
{
    public async Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null) => await GetAsync($"{Api.TrustInsight.TrustsCharacteristics}{query?.ToQueryString()}");
}

public interface ITrustInsightApi
{
    Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null);
}