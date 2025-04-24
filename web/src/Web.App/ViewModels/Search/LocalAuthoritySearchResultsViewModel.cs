// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public class LocalAuthoritySearchResultsViewModel : LocalAuthoritySearchViewModel
{
    public long TotalResults { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public LocalAuthoritySearchResultViewModel[] Results { get; set; } = [];
}