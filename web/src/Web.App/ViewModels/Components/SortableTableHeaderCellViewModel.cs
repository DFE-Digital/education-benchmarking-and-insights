namespace Web.App.ViewModels.Components;

public class SortableTableHeaderCellLinkViewModel(
    string label,
    string sortField,
    string sortKey,
    string sortDelimeter,
    string? currentSortField,
    string? currentSortOrder,
    string baseUrl,
    string? className,
    string? tableId) : SortableTableHeaderCellViewModel(label, sortField, sortDelimeter, currentSortField, currentSortOrder, className, tableId)
{
    public string Href
    {
        get
        {
            var queryPrefix = baseUrl.Contains('?') ? "&" : "?";
            return $"{baseUrl}{queryPrefix}{sortKey}={SortValue}";
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
    string? className,
    string? tableId) : SortableTableHeaderCellViewModel(label, sortField, sortDelimeter, currentSortField, currentSortOrder, className, tableId)
{
    public string AriaPressed => string.IsNullOrWhiteSpace(CurrentSortField)
        ? "mixed"
        : CurrentSortField == SortField
            ? "true"
            : "false";
    public string SortKey => sortKey;
}

public abstract class SortableTableHeaderCellViewModel(
    string label,
    string sortField,
    string sortDelimeter,
    string? currentSortField,
    string? currentSortOrder,
    string? className,
    string? tableId)
{
    protected string? CurrentSortField => currentSortField;

    public string AriaSort => Sort switch
    {
        "asc" => "ascending",
        "desc" => "descending",
        _ => "none"
    };
    public string ClassName => className ?? string.Empty;
    public string Label => label;
    public string? Sort => CurrentSortField == sortField ? currentSortOrder : null;
    public string SortField => sortField;
    public string SortIntent => Sort == "asc" ? "desc" : "asc";
    public string SortValue => $"{SortField}{sortDelimeter}{SortIntent}";
    public string? TableId => tableId;
}