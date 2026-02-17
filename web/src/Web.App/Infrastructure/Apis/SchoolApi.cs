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

    public Task<ApiResult> CreateComparatorsAsync(string identifier, PostSchoolComparatorsRequest request, CancellationToken cancellationToken = default) => PostAsync(Routes.Comparators(identifier), new JsonContent(request), cancellationToken);

    public async Task<ApiResult> QueryAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Routes.Query}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> CharacteristicsAsync(string identifier, CancellationToken cancellationToken = default) => await GetAsync(Routes.Characteristics(identifier), cancellationToken);

    public async Task<ApiResult> QueryMetricRagRatingsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Routes.QueryMetricRagRatings}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> MetricRagRatingsUserDefinedAsync(string identifier, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Routes.MetricRagRatingsUserDefined(identifier)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> QueryMetricRagRatingDetailsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Routes.QueryMetricRagRatingsDetails}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> QueryItSpendingAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Routes.QueryItSpending}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> QuerySeniorLeadershipAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Routes.QuerySeniorLeadership}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> QueryIncomeHistoryAsync(string identifier, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Routes.QueryIncomeHistory(identifier)}{query?.ToQueryString()}", cancellationToken);

    private static class Routes
    {
        private const string Base = "api/schools";

        public static string Query => $"{Base}";
        public static string Single(string? identifier) => $"{Base}/{identifier}";
        public static string Suggest => $"{Base}/suggest";
        public static string Search => $"{Base}/search";
        public static string Comparators(string? identifier) => $"{Base}/{identifier}/comparators";
        public static string Characteristics(string? identifier) => $"{Base}/{identifier}/characteristics";
        public static string QueryMetricRagRatingsDetails => $"{Base}/metric-rag-ratings/details";
        public static string QueryMetricRagRatings => $"{Base}/metric-rag-ratings";
        public static string MetricRagRatingsUserDefined(string? identifier) => $"{Base}/user-defined/{identifier}/metric-rag-ratings";
        public static string QueryItSpending => $"{Base}/accounts/it-spending";
        public static string QuerySeniorLeadership => $"{Base}/census/senior-leadership";
        public static string QueryIncomeHistory(string? identifier) => $"{Base}/{identifier}/accounts/income/history";
    }
}

public interface ISchoolApi
{
    Task<ApiResult> SingleAsync(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> CharacteristicsAsync(string identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestAsync(string search, string[]? exclude = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult> CreateComparatorsAsync(string identifier, PostSchoolComparatorsRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryMetricRagRatingsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> MetricRagRatingsUserDefinedAsync(string identifier, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryMetricRagRatingDetailsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryItSpendingAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QuerySeniorLeadershipAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryIncomeHistoryAsync(string identifier, ApiQuery? query = null, CancellationToken cancellationToken = default);
}