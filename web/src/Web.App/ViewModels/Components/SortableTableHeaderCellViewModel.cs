namespace Web.App.ViewModels.Components;

public class SortableTableHeaderCellLinkViewModel(
    string label,
    string sortField,
    string sortKey,
    string sortDelimeter,
    string? currentSortField,
    string? currentSortOrder,
    string baseUrl,
    string? className) : SortableTableHeaderCellViewModel(label, sortField, currentSortField, currentSortOrder, className)
{
    public string Href
    {
        get
        {
            var queryPrefix = baseUrl.Contains('?') ? "&" : "?";
            var newSort = currentSortField == sortField && currentSortOrder == "asc" ? "desc" : "asc";
            return $"{baseUrl}{queryPrefix}{sortKey}={sortField}{sortDelimeter}{newSort}";
        }
    }
}

public class SortableTableHeaderCellButtonViewModel(
    string label,
    string sortField,
    string sortDelimeter,
    string sortKey,
    string? currentSortField,
    string? currentSortOrder,
    string? className) : SortableTableHeaderCellViewModel(label, sortField, currentSortField, currentSortOrder, className)
{
    public string SortValue => $"{sortField}{sortDelimeter}{(currentSortField == sortField && currentSortOrder == "asc" ? "desc" : "asc")}";
    public string SortKey => sortKey;
}

public abstract class SortableTableHeaderCellViewModel(
    string label,
    string sortField,
    string? currentSortField,
    string? currentSortOrder,
    string? className)
{
    public string Label => label;
    public string SortField => sortField;
    public string ClassName => className ?? string.Empty;
    public string? Sort => currentSortField == sortField ? currentSortOrder : null;

    public string AriaSort => Sort switch
    {
        "asc" => "ascending",
        "desc" => "descending",
        _ => "none"
    };
}