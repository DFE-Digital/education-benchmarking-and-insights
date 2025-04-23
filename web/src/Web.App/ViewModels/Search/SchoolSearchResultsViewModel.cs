// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public class SchoolSearchResultsViewModel : SchoolSearchViewModel
{
    public long TotalResults { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public SchoolSearchResultViewModel[] Results { get; set; } = [];
}