using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace Platform.Search;

[ExcludeFromCodeCoverage]
public abstract record SuggestRequest
{
    public string? SearchText { get; set; }
    public int Size { get; set; } = 10;
    public virtual string SuggesterName => string.Empty;
}