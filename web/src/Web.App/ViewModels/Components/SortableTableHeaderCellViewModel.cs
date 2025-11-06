namespace Web.App.ViewModels.Components;

public class SortableTableHeaderCellViewModel(
    string label,
    string sortField,
    string sortDelimeter,
    string sortKey,
    string? currentSortField,
    string? currentSortOrder,
    string? className,
    string? tableId)
{
    public string AriaPressed => string.IsNullOrWhiteSpace(currentSortField)
        ? "mixed"
        : currentSortField == SortField
            ? "true"
            : "false";
    public string AriaSort => Sort switch
    {
        "asc" => "ascending",
        "desc" => "descending",
        _ => "none"
    };
    public string ClassName => className ?? string.Empty;
    public string Label => label;
    public string? Sort => currentSortField == sortField ? currentSortOrder : null;
    public string SortField => sortField;
    public string SortIntent => Sort == "asc" ? "desc" : "asc";
    public string SortValue => $"{SortField}{sortDelimeter}{SortIntent}";
    public string? TableId => tableId;
    public string SortKey => sortKey;
}