using System.Text.Json.Serialization;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public class PostSuggestRequest
{
    [JsonPropertyName("q")]
    public string SearchText { get; set; }

    [JsonPropertyName("skip")]
    public int Size { get; set; }
    
    [JsonPropertyName("suggester")]
    public string SuggesterName { get; set; }
}