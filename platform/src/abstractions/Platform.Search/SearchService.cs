using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Platform.Search;

public abstract class SearchService<T>(IIndexClient client)
{
    protected async Task<(long? Total, IEnumerable<ScoreResponse<T>> Response)> SearchWithScoreAsync(string? search, string? filters, int? size = null, CancellationToken cancellationToken = default)
    {
        var options = new SearchOptions
        {
            Size = size,
            IncludeTotalCount = true,
            Filter = filters,
            QueryType = SearchQueryType.Full
        };

        var response = await client.SearchAsync<T>(search, options, cancellationToken);
        var value = response.Value;

        return (value.TotalCount, value
            .GetResults()
            .Select(result => new ScoreResponse<T>
            {
                Score = result.Score,
                Document = result.Document
            }));
    }

    protected async Task<T> LookUpAsync(string? key, CancellationToken cancellationToken = default)
    {
        var response = await client.GetDocumentAsync<T>(key, cancellationToken);
        return response.Value;
    }

    protected virtual async Task<SearchResponse<T>> SearchAsync(SearchRequest request, Func<string?>? filterExpBuilder = null, string[]? facets = null, CancellationToken cancellationToken = default)
    {
        var options = new SearchOptions
        {
            Size = request.PageSize,
            Skip = (request.Page - 1) * request.PageSize,
            IncludeTotalCount = true,
            Filter = request.Filters != null ? filterExpBuilder?.Invoke() : null,
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

        if (request.OrderBy is not null)
        {
            var orderByItem = $"{request.OrderBy.Field} {request.OrderBy.Value?.ToLower()}";
            options.OrderBy.Add(orderByItem);
        }

        var searchResponse = await client.SearchAsync<T>(request.SearchText, options, cancellationToken);
        var searchResults = searchResponse.Value;
        var outputFacets = searchResults.Facets is { Count: > 0 } ? BuildFacetOutput(searchResults.Facets) : null;
        var results = searchResults.GetResults().Select(result => result.Document);

        return SearchResponse<T>.Create(results, request.Page, request.PageSize, searchResults.TotalCount, outputFacets);
    }

    protected virtual async Task<SuggestResponse<T>> SuggestAsync(
        SuggestRequest request,
        Func<string?>? filterExpBuilder = null,
        string[]? selectFields = null,
        CancellationToken cancellationToken = default)
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

        var response = await client.SuggestAsync<T>(request.SearchText, request.SuggesterName, options, cancellationToken);
        var results = response.Value.Results.Select(SuggestValue<T>.Create);

        return new SuggestResponse<T>
        {
            Results = results
        };
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