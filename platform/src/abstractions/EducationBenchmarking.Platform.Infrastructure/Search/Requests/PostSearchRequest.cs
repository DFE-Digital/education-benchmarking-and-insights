using System.Text.Json.Serialization;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public class PostSearchRequest
{
    [JsonPropertyName("q")]
    public string SearchText { get; set; }
    
    [JsonPropertyName("skip")]
    public int Skip { get; set; }
    
    [JsonPropertyName("top")]
    public int Size { get; set; }
    
    [JsonPropertyName("filters")]
    public List<SearchFilters> Filters { get; set; }
}

public class SearchFilters
{
    [JsonPropertyName("field")]
    public string Field { get; set; }
    
    [JsonPropertyName("value")]
    public string Value { get; set; }
}