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

    public Task<ApiResult> QueryEhcpAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Routes.QueryEhcp}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> QueryEhcpHistoryAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Routes.QueryEhcpHistory}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> QueryHighNeedsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Routes.QueryHighNeeds}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> QueryHighNeedsHistoryAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Routes.QueryHighNeedsHistory}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> QueryMaintainedSchoolsFinanceAsync(string code, ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Routes.QueryMaintainedSchoolsFinance(code)}{query?.ToQueryString()}", cancellationToken);

    public Task<ApiResult> QueryMaintainedSchoolsWorkforceAsync(string code, ApiQuery? query = null, CancellationToken cancellationToken = default) => GetAsync($"{Routes.QueryMaintainedSchoolsWorkforce(code)}{query?.ToQueryString()}", cancellationToken);

    private static class Routes
    {
        private const string Base = "api/local-authorities";

        public static string Single(string? identifier) => $"{Base}/{identifier}";
        public static string StatisticalNeighbours(string? identifier) => $"{Base}/{identifier}/statistical-neighbours";
        public static string Query => $"{Base}";
        public static string Suggest => $"{Base}/suggest";
        public static string Search => $"{Base}/search";
        public static string QueryEhcp => $"{Base}/education-health-care-plans";
        public static string QueryEhcpHistory => $"{Base}/education-health-care-plans/history";
        public static string QueryHighNeeds => $"{Base}/accounts/high-needs";
        public static string QueryHighNeedsHistory => $"{Base}/accounts/high-needs/history";
        public static string QueryMaintainedSchoolsFinance(string? identifier) => $"{Base}/{identifier}/maintained-schools/finance";
        public static string QueryMaintainedSchoolsWorkforce(string? identifier) => $"{Base}/{identifier}/maintained-schools/workforce";
    }
}

public interface ILocalAuthorityApi
{
    Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> StatisticalNeighboursAsync(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryAsync(CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestAsync(string search, string[]? exclude = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryEhcpAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryEhcpHistoryAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryHighNeedsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryHighNeedsHistoryAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryMaintainedSchoolsFinanceAsync(string code, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryMaintainedSchoolsWorkforceAsync(string code, ApiQuery? query = null, CancellationToken cancellationToken = default);
}