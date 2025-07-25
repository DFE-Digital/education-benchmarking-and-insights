namespace Web.App.Infrastructure.Apis.Insight;

public interface IItSpendApi
{
    Task<ApiResult> QuerySchools(ApiQuery? query = null, CancellationToken cancellationToken = default);
}

public class ItSpendApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IItSpendApi
{
    public async Task<ApiResult> QuerySchools(ApiQuery? query = null, CancellationToken cancellationToken = default)
    {
        return await GetAsync($"{Api.ItSpend.Schools}{query?.ToQueryString()}", cancellationToken);
    }
}