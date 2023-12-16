using System.Text.Json.Serialization;
using Azure.Search.Documents.Models;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public class SearchOutput<T> : PagedResults<T>
{
    public Dictionary<string, IList<FacetValue>>? Facets { get; set; }

    public static SearchOutput<T> Create(IEnumerable<T> results, int page = 1, int pageSize = 10, long? totalResults = null, Dictionary<string, IList<FacetValue>>? facets = null)
    {
        var enumerable = results as T[] ?? results.ToArray();
        var resultCount = totalResults ?? enumerable.Length;
            
        return new SearchOutput<T>
        {
            Page = page,
            PageSize = pageSize,
            Results = enumerable,
            TotalResults = resultCount,
            Facets = facets
        };         
    }
}

public class FacetValue
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }
    
    [JsonPropertyName("count")]
    public long? Count { get; set; }
}