namespace Web.App.Infrastructure.Apis.LocalAuthorityFinances;

public class LocalAuthorityFinancesApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ILocalAuthorityFinancesApi
{
    public Task<ApiResult> GetHighNeeds(ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Api.LocalAuthorityFinances.HighNeeds}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> GetHighNeedsHistory(ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Api.LocalAuthorityFinances.HighNeedsHistory}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> GetSchoolsFinance(string code, ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Api.LocalAuthorityFinances.SchoolsFinance(code)}{query?.ToQueryString()}", cancellationToken);
    public Task<ApiResult> GetSchoolsWorkforce(string code, ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Api.LocalAuthorityFinances.SchoolsWorkforce(code)}{query?.ToQueryString()}", cancellationToken);
}

public interface ILocalAuthorityFinancesApi
{
    Task<ApiResult> GetHighNeeds(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetHighNeedsHistory(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetSchoolsFinance(string code, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetSchoolsWorkforce(string code, ApiQuery? query = null, CancellationToken cancellationToken = default);
}