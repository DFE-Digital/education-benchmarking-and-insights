namespace Platform.Search.Tests;

public class TestService(IIndexClient client) : SearchService<TestType>(client)
{
    public async Task<(long? Total, IEnumerable<ScoreResponse<TestType>> Response)> CallSearchWithScoreAsync(
        string? search,
        string? filters,
        int? size = null) => await SearchWithScoreAsync(search, filters, size);

    public async Task<SearchResponse<TestType>> CallSearchAsync(
        SearchRequest request,
        Func<string?>? filterExpBuilder = null,
        string[]? facets = null) => await SearchAsync(request, filterExpBuilder, facets);

    public async Task<TestType> CallLookUpAsync(string? key) => await LookUpAsync(key);

    public async Task<SuggestResponse<TestType>> CallSuggestAsync(
        SuggestRequest request,
        Func<string?>? filterExpBuilder = null,
        string[]? selectFields = null) => await SuggestAsync(request, filterExpBuilder, selectFields);
}

public record TestType;

public record TestSuggestRequest : SuggestRequest
{
    public override string SuggesterName => nameof(SuggesterName);
}