// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Infrastructure.Apis;

public record SearchRequest
{
    public string? SearchText { get; set; }
    public int? PageSize { get; set; }
    public int? Page { get; set; }
    public FilterCriteria[]? Filters { get; set; }
    public OrderByCriteria? OrderBy { get; set; }

    public static SearchRequest Create(string? term, int? pageSize, int? page, IEnumerable<(string Field, string Filter)>? filters, (string Field, string Order)? orderBy)
    {
        return new SearchRequest
        {
            SearchText = term,
            PageSize = pageSize,
            Page = page,
            Filters = filters?.Select(f => new FilterCriteria { Field = f.Field, Value = f.Filter }).ToArray(),
            OrderBy = orderBy == null ? null : new OrderByCriteria { Field = orderBy.Value.Field, Value = orderBy.Value.Order }
        };
    }
}

public record FilterCriteria
{
    public string? Field { get; set; }
    public string? Value { get; set; }
}

public record OrderByCriteria
{
    public string? Field { get; set; }
    public string? Value { get; set; }
}