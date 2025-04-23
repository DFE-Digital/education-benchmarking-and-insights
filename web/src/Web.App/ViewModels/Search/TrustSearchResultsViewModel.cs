// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public class TrustSearchResultsViewModel : TrustSearchViewModel
{
    public long TotalResults { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public TrustSearchResultViewModel[] Results { get; set; } = [];
}