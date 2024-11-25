using System.Diagnostics.CodeAnalysis;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Search.Requests;

[ExcludeFromCodeCoverage]
public record SuggestRequest
{
    public string? SearchText { get; set; }
    public int Size { get; set; } = 10;
    public string? SuggesterName { get; set; }
}