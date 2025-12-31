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

    public Task<ApiResult> CreateComparatorsAsync(string companyNumber, PostTrustComparatorsRequest request, CancellationToken cancellationToken = default) => PostAsync(Routes.Comparators(companyNumber), new JsonContent(request), cancellationToken);

    private static class Routes
    {
        private const string Base = "api/trusts";

        public static string Single(string? identifier) => $"{Base}/{identifier}";
        public static string Suggest => $"{Base}/suggest";
        public static string Search => $"{Base}/search";
        public static string Comparators(string? identifier) => $"{Base}/{identifier}/comparators";
    }
}

public interface ITrustApi
{
    Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancel = default);
    Task<ApiResult> SuggestAsync(string search, string[]? exclude = null, CancellationToken cancellationToken = default);
    Task<ApiResult> CreateComparatorsAsync(string companyNumber, PostTrustComparatorsRequest request, CancellationToken cancellationToken = default);
}