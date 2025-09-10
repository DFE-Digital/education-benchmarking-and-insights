// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Web.App.Infrastructure.Apis;

public record SearchResponse<T>
{
    public Dictionary<string, IList<FacetValueResponseModel>>? Facets { get; set; }
    public long TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public T[] Results { get; set; } = [];
}

public record FacetValueResponseModel
{
    public string? Value { get; set; }
    public long? Count { get; set; }
}