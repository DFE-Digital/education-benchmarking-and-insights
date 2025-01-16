using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Platform.Search;

public abstract class SearchService<T>(IIndexClient client)
{
    protected async Task<(long? Total, IEnumerable<ScoreResponse<T>> Response)> SearchWithScoreAsync(string? search, string? filters, int? size = null)
    {
        var options = new SearchOptions
        {
            Size = size,
            IncludeTotalCount = true,
            Filter = filters,
            QueryType = SearchQueryType.Full
        };

        var response = await client.SearchAsync<T>(search, options);
        var value = response.Value;

        return (value.TotalCount, value
            .GetResults()
            .Select(result => new ScoreResponse<T>
            {
                Score = result.Score,
                Document = result.Document
            }));
    }

    protected async Task<T> LookUpAsync(string? key)
    {
        var response = await client.GetDocumentAsync<T>(key);
        return response.Value;
    }

    protected async Task<SearchResponse<T>> SearchAsync(SearchRequest request, Func<FilterCriteria[], string?>? filterExpBuilder = null, string[]? facets = null)
    {
        var options = new SearchOptions
        {
            Size = request.PageSize,
            Skip = (request.Page - 1) * request.PageSize,
            IncludeTotalCount = true,
            Filter = request.Filters != null ? filterExpBuilder?.Invoke(request.Filters) : null,
            QueryType = SearchQueryType.Simple
        };

        if (facets is { Length: > 0 })
        {
            foreach (var facet in facets)
            {
                if (!string.IsNullOrWhiteSpace(facet))
                {
                    options.Facets.Add(facet);
                }
            }
        }

        var searchResponse = await client.SearchAsync<T>(request.SearchText, options);
        var searchResults = searchResponse.Value;
        var outputFacets = searchResults.Facets is { Count: > 0 } ? BuildFacetOutput(searchResults.Facets) : null;
        var results = searchResults.GetResults().Select(result => result.Document);

        return SearchResponse<T>.Create(results, request.Page, request.PageSize, searchResults.TotalCount, outputFacets);
    }

    protected async Task<SuggestResponse<T>> SuggestAsync(SuggestRequest request, Func<string?>? filterExpBuilder = null, string[]? selectFields = null)
    {
        ArgumentNullException.ThrowIfNull(client);

        var options = new SuggestOptions
        {
            HighlightPreTag = "*",
            HighlightPostTag = "*",
            Filter = filterExpBuilder?.Invoke(),
            Size = request.Size,
            UseFuzzyMatching = false
        };

        if (selectFields is { Length: > 0 })
        {
            foreach (var field in selectFields)
            {
                if (!string.IsNullOrWhiteSpace(field))
                {
                    options.Select.Add(field);
                }
            }
        }

        var response = await client.SuggestAsync<T>(request.SearchText, request.SuggesterName, options);
        var results = response.Value.Results.Select(SuggestValue<T>.Create);

        return new SuggestResponse<T> { Results = results };
    }

    private static Dictionary<string, IList<FacetValueResponseModel>> BuildFacetOutput(IDictionary<string, IList<FacetResult>> facetResults)
    {
        var facetOutput = new Dictionary<string, IList<FacetValueResponseModel>>();
        foreach (var facetResult in facetResults)
        {
            facetOutput[facetResult.Key] = facetResult.Value
                .Select(x => new FacetValueResponseModel
                {
                    Value = x.Value.ToString(),
                    Count = x.Count
                }).ToList();
        }

        return facetOutput;
    }
}