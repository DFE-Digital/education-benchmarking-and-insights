namespace Web.App.Infrastructure.Apis.LocalAuthorities;

public class LocalAuthoritiesApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ILocalAuthoritiesApi
{
    public Task<ApiResult> GetHighNeeds(ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Api.LocalAuthorities.HighNeeds}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> GetHighNeedsHistory(ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Api.LocalAuthorities.HighNeedsHistory}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> GetSchoolsFinance(string code, ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Api.LocalAuthorities.SchoolsFinance(code)}{query?.ToQueryString()}", cancellationToken);
}

public interface ILocalAuthoritiesApi
{
    Task<ApiResult> GetHighNeeds(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetHighNeedsHistory(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetSchoolsFinance(string code, ApiQuery? query = null, CancellationToken cancellationToken = default);
}