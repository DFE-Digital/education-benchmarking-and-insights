// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Web.App.ViewModels.Search;

public record SchoolSearchResultsOptionsViewModel
{
    public string? OrderBy { get; set; }
    public string[] OverallPhase { get; set; } = [];
    public SearchResultFacetViewModel[] OverallPhaseFacets { get; set; } = [];
}