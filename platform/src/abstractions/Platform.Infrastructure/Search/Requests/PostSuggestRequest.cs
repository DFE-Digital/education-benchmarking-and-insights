using System.Diagnostics.CodeAnalysis;

namespace Platform.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public record PostSuggestRequest
{
    public string? SearchText { get; set; }
    public int Size { get; set; } = 10;
    public string? SuggesterName { get; set; }
}