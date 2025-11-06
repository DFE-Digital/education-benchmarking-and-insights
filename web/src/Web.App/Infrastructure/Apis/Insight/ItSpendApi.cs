namespace Web.App.Infrastructure.Apis.Insight;

public interface IItSpendApi
{
    Task<ApiResult> QuerySchools(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryTrusts(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> TrustForecast(string? companyNumber, CancellationToken cancellationToken = default);
}

public class ItSpendApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IItSpendApi
{
    public async Task<ApiResult> QuerySchools(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.ItSpend.Schools}{query?.ToQueryString()}", cancellationToken);
    public async Task<ApiResult> QueryTrusts(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.ItSpend.Trusts}{query?.ToQueryString()}", cancellationToken);
    public async Task<ApiResult> TrustForecast(string? companyNumber, CancellationToken cancellationToken = default) => await GetAsync(Api.ItSpend.TrustForecast(companyNumber), cancellationToken);
}