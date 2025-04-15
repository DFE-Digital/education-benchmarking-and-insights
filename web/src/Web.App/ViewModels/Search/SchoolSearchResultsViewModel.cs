// ReSharper disable PropertyCanBeMadeInitOnly.Global

using Web.App.Domain;

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
        .Union(OverallPhaseAllFacets)
        .OrderBy(f => f.Value)
        .ToArray();

    private SearchResultFacetViewModel[] OverallPhaseAllFacets => OverallPhaseAll
        .Where(f => OverallPhaseTypes.All.Contains(f))
        .Select(f => new SearchResultFacetViewModel { Value = f, Count = 0 })
        .ToArray();
}