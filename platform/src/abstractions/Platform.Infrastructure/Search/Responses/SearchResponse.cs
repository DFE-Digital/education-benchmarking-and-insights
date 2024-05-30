using System.Diagnostics.CodeAnalysis;
using Platform.Domain;

namespace Platform.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public record SearchResponse<T> : IPagedResponse
{
    public Dictionary<string, IList<FacetValueResponseModel>>? Facets { get; set; }
    public long TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount => (int)Math.Ceiling(TotalResults / (float)Math.Max(1, PageSize));
    public IEnumerable<T> Results { get; set; } = Array.Empty<T>();

    public static SearchResponse<T> Create(IEnumerable<T> results, int page = 1, int pageSize = 10, long? totalResults = null, Dictionary<string, IList<FacetValueResponseModel>>? facets = null)
    {
        var enumerable = results as T[] ?? results.ToArray();
        var resultCount = totalResults ?? enumerable.Length;

        return new SearchResponse<T>
        {
            Page = page,
            PageSize = pageSize,
            Results = enumerable,
            TotalResults = resultCount,
            Facets = facets
        };
    }
}

[ExcludeFromCodeCoverage]
public record FacetValueResponseModel
{
    public string? Value { get; set; }
    public long? Count { get; set; }
}