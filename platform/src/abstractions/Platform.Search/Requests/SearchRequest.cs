using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Search;

[ExcludeFromCodeCoverage]
public record SearchRequest
{
    /// <summary>The search text to match against school names or other searchable fields.</summary>
    public string? SearchText { get; set; }
    /// <summary>The number of results to return per page. Defaults to 15.</summary>
    public int PageSize { get; set; } = 15;
    /// <summary>The page number of results to return. Defaults to 1.</summary>
    public int Page { get; set; } = 1;
    /// <summary>The collection of filter criteria to apply to the search.</summary>
    public FilterCriteria[]? Filters { get; set; }
    /// <summary>The criteria used to order the search results.</summary>
    public OrderByCriteria? OrderBy { get; set; }

    public string? FilterExpression()
    {
        if (Filters == null || Filters.Length == 0)
        {
            return null;
        }

        return $"({string.Join(" or ", Filters.Select(f => $"{f.Field} eq '{f.Value}'"))})";
    }
}

[ExcludeFromCodeCoverage]
public record FilterCriteria
{
    /// <summary>The field name to filter on.</summary>
    public string? Field { get; set; }
    /// <summary>The value to match for the specified filter field.</summary>
    public string? Value { get; set; }
}

public record OrderByCriteria
{
    /// <summary>The field name to order the results by.</summary>
    public string? Field { get; set; }
    /// <summary>The direction or specific sort value (e.g., "asc", "desc").</summary>
    public string? Value { get; set; }
}