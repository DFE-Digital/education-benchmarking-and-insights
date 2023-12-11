using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public abstract class SearchService
{
    private readonly SearchClient _client;

    protected SearchService(Uri searchEndpoint, string indexName, AzureKeyCredential credential)
    {
        _client = new SearchClient(searchEndpoint, indexName, credential);
    }

    protected async Task<SearchOutput<T>> SearchAsync<T>(PostSearchRequest request,  Func<List<SearchFilters>, string>? filterExpBuilder = null, string[]? facets = null)
    {
        var options = new SearchOptions
        {
            Size = request.Size,
            Skip = request.Skip,
            IncludeTotalCount = true,
            Filter = filterExpBuilder?.Invoke(request.Filters)
        };

        if (facets is { Length: > 0 })
        {
            foreach (var facet in facets)
            {
                if (string.IsNullOrWhiteSpace(facet))
                {
                    options.Facets.Add(facet);    
                }
            }
        }

        var results = await _client.SearchAsync<T>(request.SearchText, options);
        var facetOutput = BuildFacetOutput(results);

        return new SearchOutput<T>
        {
            Count = results.Value.TotalCount,
            Results = results.Value.GetResults().ToList(),
            Facets = facetOutput
        };
    }

    private static Dictionary<string, IList<FacetValue>> BuildFacetOutput<T>(Response<SearchResults<T>> results)
    {
        var facetOutput = new Dictionary<string, IList<FacetValue>>();
        foreach (var facetResult in results.Value.Facets)
        {
            facetOutput[facetResult.Key] = facetResult.Value
                .Select(x => new FacetValue { Value = x.Value.ToString(), Count = x.Count }).ToList();
        }

        return facetOutput;
    }
}