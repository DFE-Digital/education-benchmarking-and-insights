namespace Web.App.Infrastructure.Apis;

public class SchoolApi(HttpClient httpClient, string? key = null) : ApiBase(httpClient, key), ISchoolApi
{
    public Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default) => GetAsync(Routes.Single(identifier), cancellationToken);

    public Task<ApiResult> SuggestAsync(string search, string[]? exclude = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri(Routes.Suggest, UriKind.Relative),
        Content = new JsonContent(new
        {
            SearchText = search,
            Size = 10,
            Exclude = exclude,
            ExcludeMissingFinancialData = excludeMissingFinancialData
        })
    }, cancellationToken);

    public Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default) => SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri(Routes.Search, UriKind.Relative),
        Content = new JsonContent(request)
    }, cancellationToken);

    public Task<ApiResult> CreateComparatorsAsync(string urn, PostSchoolComparatorsRequest request, CancellationToken cancellationToken = default) => PostAsync(Routes.Comparators(urn), new JsonContent(request), cancellationToken);

    private static class Routes
    {
        private const string Base = "api/schools";

        public static string Single(string? identifier) => $"{Base}/{identifier}";
        public static string Suggest => $"{Base}/suggest";
        public static string Search => $"{Base}/search";
        public static string Comparators(string? identifier) => $"{Base}/{identifier}/comparators";
    }
}

public interface ISchoolApi
{
    Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestAsync(string search, string[]? exclude = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult> CreateComparatorsAsync(string urn, PostSchoolComparatorsRequest request, CancellationToken cancellationToken = default);

}