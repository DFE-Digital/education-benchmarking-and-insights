namespace Web.App.Infrastructure.Apis.Insight;

public interface IBalanceApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryTrusts(ApiQuery? query = null, CancellationToken cancellationToken = default);
}

public class BalanceApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IBalanceApi
{
    public async Task<ApiResult> School(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Balance.School(urn)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Balance.SchoolHistory(urn)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Balance.Trust(companyNo)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Balance.TrustHistory(companyNo)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> QueryTrusts(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Balance.Trusts}{query?.ToQueryString()}", cancellationToken);
}