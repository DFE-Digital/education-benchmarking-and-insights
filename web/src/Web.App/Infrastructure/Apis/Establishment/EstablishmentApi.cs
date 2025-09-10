namespace Web.App.Infrastructure.Apis.Establishment;

public class EstablishmentApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IEstablishmentApi
{
    public Task<ApiResult> GetSchool(string? identifier, CancellationToken cancellationToken = default) => GetAsync(Api.Establishment.School(identifier), cancellationToken);

    public Task<ApiResult> GetTrust(string? identifier, CancellationToken cancellationToken = default) => GetAsync(Api.Establishment.Trust(identifier), cancellationToken);

    public Task<ApiResult> GetLocalAuthority(string? identifier, CancellationToken cancellationToken = default) => GetAsync(Api.Establishment.LocalAuthority(identifier), cancellationToken);

    public Task<ApiResult> GetLocalAuthorityStatisticalNeighbours(string? identifier, CancellationToken cancellationToken = default) => GetAsync(Api.Establishment.LocalAuthorityStatisticalNeighbours(identifier), cancellationToken);

    public Task<ApiResult> GetLocalAuthoritiesNationalRank(ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Api.Establishment.LocalAuthorityNationalRank}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> GetLocalAuthorities(CancellationToken cancellationToken = default) => GetAsync(Api.Establishment.LocalAuthorities, cancellationToken);

    public Task<ApiResult> SuggestSchools(string search, string[]? exclude = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri($"{Api.Establishment.SchoolSuggest}", UriKind.Relative),
        Content = new JsonContent(new SchoolSuggestRequest
        {
            SearchText = search,
            Size = 10,
            Exclude = exclude,
            ExcludeMissingFinancialData = excludeMissingFinancialData
        })
    }, cancellationToken);

    public Task<ApiResult> SuggestTrusts(string search, string[]? exclude = null, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri($"{Api.Establishment.TrustSuggest}", UriKind.Relative),
        Content = new JsonContent(new
        {
            SearchText = search,
            Size = 10,
            Exclude = exclude
        })
    }, cancellationToken);

    public Task<ApiResult> SuggestLocalAuthorities(string search, string[]? exclude = null, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri($"{Api.Establishment.LocalAuthoritySuggest}", UriKind.Relative),
        Content = new JsonContent(new
        {
            SearchText = search,
            Size = 10,
            Exclude = exclude
        })
    }, cancellationToken);

    public Task<ApiResult> SearchSchools(SearchRequest request, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri(Api.Establishment.SchoolSearch, UriKind.Relative),
        Content = new JsonContent(request)
    }, cancellationToken);

    public Task<ApiResult> SearchTrusts(SearchRequest request, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri(Api.Establishment.TrustSearch, UriKind.Relative),
        Content = new JsonContent(request)
    }, cancellationToken);

    public Task<ApiResult> SearchLocalAuthorities(SearchRequest request, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri(Api.Establishment.LocalAuthoritySearch, UriKind.Relative),
        Content = new JsonContent(request)
    }, cancellationToken);
}

public interface IEstablishmentApi
{
    Task<ApiResult> GetSchool(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> GetTrust(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> GetLocalAuthority(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> GetLocalAuthorityStatisticalNeighbours(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> GetLocalAuthoritiesNationalRank(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetLocalAuthorities(CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestSchools(string search, string[]? exclude = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestTrusts(string search, string[]? exclude = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestLocalAuthorities(string search, string[]? exclude = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchSchools(SearchRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchTrusts(SearchRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchLocalAuthorities(SearchRequest request, CancellationToken cancellationToken = default);
}