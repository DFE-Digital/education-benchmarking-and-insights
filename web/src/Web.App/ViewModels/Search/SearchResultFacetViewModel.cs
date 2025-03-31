namespace Web.App.ViewModels.Search;

public record SearchResultFacetViewModel
{
    public string? Value { get; set; }
    public long? Count { get; set; }
}