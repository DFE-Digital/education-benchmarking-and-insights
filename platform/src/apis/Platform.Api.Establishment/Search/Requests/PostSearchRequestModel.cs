using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public record PostSearchRequestModel
{
    public string? SearchText { get; set; }
    public int PageSize { get; set; } = 15;
    public int Page { get; set; } = 1;
    public FilterCriteriaRequestModel[]? Filters { get; set; }
}

[ExcludeFromCodeCoverage]
public record FilterCriteriaRequestModel
{
    public string? Field { get; set; }
    public string? Value { get; set; }
}