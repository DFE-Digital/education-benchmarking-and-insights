namespace Web.App.ViewModels.Search;

public record SearchResultsOptionsViewModel
{
    public string? OrderBy { get; init; }
    public string? ResetUrl { get; init; }
}