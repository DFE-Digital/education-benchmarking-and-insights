using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace Platform.Search;

[ExcludeFromCodeCoverage]
public record SearchRequest
{
    public string? SearchText { get; set; }
    public int PageSize { get; set; } = 15;
    public int Page { get; set; } = 1;
    public FilterCriteria[]? Filters { get; set; }
}

[ExcludeFromCodeCoverage]
public record FilterCriteria
{
    public string? Field { get; set; }
    public string? Value { get; set; }
}