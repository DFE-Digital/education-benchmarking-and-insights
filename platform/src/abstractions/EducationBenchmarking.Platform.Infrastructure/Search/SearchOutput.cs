using System.Text.Json.Serialization;
using Azure.Search.Documents.Models;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public class SearchOutput<T>
{
    [JsonPropertyName("count")]
    public long? Count { get; set; }
    
    [JsonPropertyName("results")]
    public List<SearchResult<T>> Results { get; set; }
    
    [JsonPropertyName("Facets")]
    public Dictionary<string, IList<FacetValue>>? Facets { get; set; } 
}

public class FacetValue
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }
    
    [JsonPropertyName("count")]
    public long? Count { get; set; }
}