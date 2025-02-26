namespace Web.App.Infrastructure.Apis.LocalAuthorities;

public class LocalAuthoritiesApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ILocalAuthoritiesApi
{
    public Task<ApiResult> GetHighNeedsHistory(ApiQuery? query = null, CancellationToken cancellationToken = default)
    {
        return GetAsync($"{Api.LocalAuthorities.HighNeedsHistory}{query?.ToQueryString()}", cancellationToken);
    }
}

public interface ILocalAuthoritiesApi
{
    Task<ApiResult> GetHighNeedsHistory(ApiQuery? query = null, CancellationToken cancellationToken = default);
}