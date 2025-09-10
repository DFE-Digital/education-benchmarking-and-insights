// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public record SchoolSearchResultsOptionsViewModel : SearchResultsOptionsViewModel
{
    public string[] OverallPhase { get; set; } = [];
}