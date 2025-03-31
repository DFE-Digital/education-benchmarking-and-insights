// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public class SchoolSearchResultsViewModel : SchoolSearchViewModel
{
    public int TotalResults { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Dictionary<string, IList<SearchResultFacetViewModel>> Facets { get; set; } = new();
    public SchoolSearchResultViewModel[] Results { get; set; } = [];

    public SearchResultFacetViewModel[] OverallPhaseFacets => Facets
        .Where(f => f.Key == "OverallPhase")
        .SelectMany(f => f.Value)
        .ToArray();
}