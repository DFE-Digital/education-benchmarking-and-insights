using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public record PostSuggestRequestModel
{
    public string? SearchText { get; set; }
    public int Size { get; set; } = 10;
    public string? SuggesterName { get; set; }
}