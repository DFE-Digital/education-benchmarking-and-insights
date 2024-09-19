using System.Diagnostics.CodeAnalysis;

namespace Platform.Search;

[ExcludeFromCodeCoverage]
public record SuggestRequest
{
    public string? SearchText { get; set; }
    public int Size { get; set; } = 10;
    public string? SuggesterName { get; set; }
}