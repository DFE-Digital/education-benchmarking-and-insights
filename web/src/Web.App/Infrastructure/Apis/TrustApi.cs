namespace Web.App.Infrastructure.Apis;

public class TrustApi(HttpClient httpClient, string? key = null) : ApiBase(httpClient, key), ITrustApi
{
    public Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default) => GetAsync(Routes.Single(identifier), cancellationToken);

    public Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri(Routes.Search, UriKind.Relative),
        Content = new JsonContent(request)
    }, cancellationToken);

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

    private static class Routes
    {
        public static string Single(string? identifier) => $"api/trusts/{identifier}";
        public static string Suggest => "api/trusts/suggest";
        public static string Search => "api/trusts/search";
    }
}

public interface ITrustApi
{
    Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancel = default);
    Task<ApiResult> SuggestAsync(string search, string[]? exclude = null, CancellationToken cancellationToken = default);
}