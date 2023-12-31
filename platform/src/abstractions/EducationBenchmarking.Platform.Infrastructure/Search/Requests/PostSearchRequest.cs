namespace EducationBenchmarking.Platform.Infrastructure.Search;

public class PostSearchRequest
{
    public string? SearchText { get; set; }
    public int PageSize { get; set; } = 15;
    public int Page { get; set; } = 1;
    public FilterCriteria[]? Filters { get; set; }
}

public class FilterCriteria
{
    public string? Field { get; set; }
    public string? Value { get; set; }
}