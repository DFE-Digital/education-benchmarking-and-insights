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

    private static class Routes
    {
        public static string Single(string? identifier) => $"api/schools/{identifier}";
        public static string Suggest => "api/schools/suggest";
        public static string Search => "api/schools/search";
    }
}

public interface ISchoolApi
{
    Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestAsync(string search, string[]? exclude = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);

}