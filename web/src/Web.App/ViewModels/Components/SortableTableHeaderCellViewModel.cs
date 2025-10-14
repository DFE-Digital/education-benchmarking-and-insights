namespace Web.App.ViewModels.Components;

public class SortableTableHeaderCellViewModel(
    string label,
    string sortField,
    string sortFieldKey,
    string sortOrderKey,
    string? currentSortField,
    string? currentSortOrder,
    string baseUrl,
    string? className)
{
    public string Label => label;
    public string SortField => sortField;
    public string ClassName => className ?? string.Empty;

    public string Href
    {
        get
        {
            var queryPrefix = baseUrl.Contains('?') ? "&" : "?";
            var newSort = currentSortField == sortField && currentSortOrder == "asc" ? "desc" : "asc";
            return $"{baseUrl}{queryPrefix}{sortFieldKey}={sortField}&{sortOrderKey}={newSort}";
        }
    }

    public string? Sort => currentSortField == sortField ? currentSortOrder : null;
}