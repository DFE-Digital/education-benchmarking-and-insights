using System.Diagnostics.CodeAnalysis;

namespace Platform.Functions;

[ExcludeFromCodeCoverage]
public record PagedResult<T>
{
    public IReadOnlyList<T> Results { get; init; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalResults { get; init; }
    public bool HasNextPage => Page * PageSize < TotalResults;
    public bool HasPreviousPage => Page > 1;
}
