namespace Web.App.Infrastructure.Apis;

public class LocalAuthorityApi(HttpClient httpClient, string? key = null) : ApiBase(httpClient, key), ILocalAuthorityApi
{
    public Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default) => GetAsync(Routes.Single(identifier), cancellationToken);

    public Task<ApiResult> QueryAsync(CancellationToken cancellationToken = default) => GetAsync(Routes.Query, cancellationToken);

    public Task<ApiResult> StatisticalNeighboursAsync(string? identifier, CancellationToken cancellationToken = default) => GetAsync(Routes.StatisticalNeighbours(identifier), cancellationToken);

    public Task<ApiResult> SuggestAsync(string search, string[]? exclude = null, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri(Routes.Suggest, UriKind.Relative),
        Content = new JsonContent(new
        {
            SearchText = search,
            Size = 10,
            Exclude = exclude
        })
    }, cancellationToken);

    public Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri(Routes.Search, UriKind.Relative),
        Content = new JsonContent(request)
    }, cancellationToken);

    private static class Routes
    {
        public static string Single(string? identifier) => $"api/local-authorities/{identifier}";
        public static string StatisticalNeighbours(string? identifier) => $"api/local-authorities/{identifier}/statistical-neighbours";
        public static string Query => "api/local-authorities";
        public static string Suggest => "api/local-authorities/suggest";
        public static string Search => "api/local-authorities/search";
    }
}

public interface ILocalAuthorityApi
{
    Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> StatisticalNeighboursAsync(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryAsync(CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestAsync(string search, string[]? exclude = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}