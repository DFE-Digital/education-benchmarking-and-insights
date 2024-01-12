using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public class PostSuggestRequest
{
    public string? SearchText { get; set; }
    public int Size { get; set; } = 10;
    public string? SuggesterName { get; set; }
}