namespace Web.App.Infrastructure.Apis.Establishment;

public class EstablishmentApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IEstablishmentApi
{
    public Task<ApiResult> GetSchool(string? identifier)
    {
        return GetAsync(Api.Establishment.School(identifier));
    }

    public Task<ApiResult> GetTrust(string? identifier)
    {
        return GetAsync(Api.Establishment.Trust(identifier));
    }

    public Task<ApiResult> GetLocalAuthority(string? identifier)
    {
        return GetAsync(Api.Establishment.LocalAuthority(identifier));
    }

    public Task<ApiResult> GetLocalAuthorityStatisticalNeighbours(string? identifier)
    {
        return GetAsync(Api.Establishment.LocalAuthorityStatisticalNeighbours(identifier));
    }

    public Task<ApiResult> GetLocalAuthoritiesNationalRank(ApiQuery? query = null, CancellationToken cancellationToken = default)
    {
        return GetAsync($"{Api.Establishment.LocalAuthorityNationalRank}{query?.ToQueryString()}", cancellationToken);
    }

    public Task<ApiResult> SuggestSchools(string search, string[]? exclude = null)
    {
        return SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{Api.Establishment.SchoolSuggest}", UriKind.Relative),
            Content = new JsonContent(new { SearchText = search, Size = 10, Exclude = exclude })
        });
    }

    public Task<ApiResult> SuggestTrusts(string search, string[]? exclude = null)
    {
        return SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{Api.Establishment.TrustSuggest}", UriKind.Relative),
            Content = new JsonContent(new { SearchText = search, Size = 10, Exclude = exclude })
        });
    }

    public Task<ApiResult> SuggestLocalAuthorities(string search, string[]? exclude = null)
    {
        return SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{Api.Establishment.LocalAuthoritySuggest}", UriKind.Relative),
            Content = new JsonContent(new { SearchText = search, Size = 10, Exclude = exclude })
        });
    }
}

public interface IEstablishmentApi
{
    Task<ApiResult> GetSchool(string? identifier);
    Task<ApiResult> GetTrust(string? identifier);
    Task<ApiResult> GetLocalAuthority(string? identifier);
    Task<ApiResult> GetLocalAuthorityStatisticalNeighbours(string? identifier);
    Task<ApiResult> GetLocalAuthoritiesNationalRank(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestSchools(string search, string[]? exclude = null);
    Task<ApiResult> SuggestTrusts(string search, string[]? exclude = null);
    Task<ApiResult> SuggestLocalAuthorities(string search, string[]? exclude = null);
}