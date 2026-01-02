namespace Web.App.Infrastructure.Apis.Insight;

[Obsolete(message: "Use TrustApi instead.")]
public class TrustInsightApi(ITrustApi trustApi) : ITrustInsightApi
{
    public async Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null) => await trustApi.QueryAsync(query);
}

[Obsolete(message: "Use ITrustApi instead.")]
public interface ITrustInsightApi
{
    Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null);
}