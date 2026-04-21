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
    /// <summary>The partial search text to generate suggestions for.</summary>
    public string? SearchText { get; set; }
    /// <summary>The maximum number of suggestions to return. Defaults to 10.</summary>
    public int Size { get; set; } = 10;
    /// <summary>The name of the suggester to use for providing results.</summary>
    public virtual string SuggesterName => string.Empty;
}