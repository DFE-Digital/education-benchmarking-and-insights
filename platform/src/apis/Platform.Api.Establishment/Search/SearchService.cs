using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Platform.Api.Establishment.Search;

public interface ISearchService<T>
{
    Task<SearchResponseModel<T>> SearchAsync(PostSearchRequestModel request);
    Task<SuggestResponseModel<T>> SuggestAsync(PostSuggestRequestModel request);
}


[ExcludeFromCodeCoverage]
public record SearchServiceOptions
{
    [Required(ErrorMessage = "Missing search service name")] 
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Missing search service keu")] 
    public string? Key { get; set; }

    public Uri Endpoint => new($"https://{Name}.search.windows.net/");
    public AzureKeyCredential Credential => new(Key ?? throw new ArgumentNullException(nameof(Key)));
}

[ExcludeFromCodeCoverage]
public abstract class SearchService(SearchServiceOptions options, string indexName)
{
    private readonly SearchClient _client = new(options.Endpoint, indexName, options.Credential);

    protected async Task<SearchResponseModel<T>> SearchAsync<T>(PostSearchRequestModel request, Func<FilterCriteriaRequestModel[], string?>? filterExpBuilder = null, string[]? facets = null)
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

        var searchResponse = await _client.SearchAsync<T>(request.SearchText, options);
        var searchResults = searchResponse.Value;

        var outputFacets = searchResults.Facets is { Count: > 0 } ? BuildFacetOutput(searchResults.Facets) : default;
        var results = searchResults.GetResults().Select(result => result.Document);

        return SearchResponseModel<T>.Create(results, request.Page, request.PageSize, searchResults.TotalCount, outputFacets);
    }

    protected async Task<SuggestResponseModel<T>> SuggestAsync<T>(PostSuggestRequestModel request, Func<string?>? filterExpBuilder = null, string[]? selectFields = null)
    {
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

        var response = await _client.SuggestAsync<T>(request.SearchText, request.SuggesterName, options);
        var results = response.Value.Results.Select(SuggestValueResponseModel<T>.Create);

        return new SuggestResponseModel<T>
        {
            Results = results
        };
    }

    protected static string? Sanitize(string? query)
    {
        if (query == null) return query;

        query = query.Replace("'", "''");
        return query;
    }

    private static Dictionary<string, IList<FacetValueResponseModel>> BuildFacetOutput(IDictionary<string, IList<FacetResult>> facetResults)
    {
        var facetOutput = new Dictionary<string, IList<FacetValueResponseModel>>();
        foreach (var facetResult in facetResults)
        {
            facetOutput[facetResult.Key] = facetResult.Value
                .Select(x => new FacetValueResponseModel { Value = x.Value.ToString(), Count = x.Count }).ToList();
        }

        return facetOutput;
    }
}