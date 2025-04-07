// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public class SchoolSearchResultsViewModel : SchoolSearchViewModel
{
    public long TotalResults { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Dictionary<string, IEnumerable<SearchResultFacetViewModel>> Facets { get; set; } = new();
    public SchoolSearchResultViewModel[] Results { get; set; } = [];
    public bool Success { get; set; } = true;

    public SearchResultFacetViewModel[] OverallPhaseFacets => Facets
        .Where(f => f.Key == "OverallPhase")
        .SelectMany(f => f.Value)
        .ToArray();
}