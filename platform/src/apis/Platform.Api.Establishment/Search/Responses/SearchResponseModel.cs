using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Platform.Domain;

namespace Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public record SearchResponseModel<T> : IPagedResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, IList<FacetValueResponseModel>>? Facets { get; set; }
    public long TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount => (int)Math.Ceiling(TotalResults / (float)Math.Max(1, PageSize));
    public IEnumerable<T> Results { get; set; } = Array.Empty<T>();

    public static SearchResponseModel<T> Create(IEnumerable<T> results, int page = 1, int pageSize = 10, long? totalResults = null, Dictionary<string, IList<FacetValueResponseModel>>? facets = null)
    {
        var enumerable = results as T[] ?? results.ToArray();
        var resultCount = totalResults ?? enumerable.Length;

        return new SearchResponseModel<T>
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