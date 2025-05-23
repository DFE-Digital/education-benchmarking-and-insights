namespace Web.App.ViewModels.Components;

public class ChartActionsViewModel(
    string? elementId,
    string? title,
    bool? showTitle,
    string? copyEventId,
    string? saveEventId,
    List<string>? costCodes)
{
    public string ElementId { get; init; } = elementId ?? string.Empty;
    public string Title { get; init; } = title ?? string.Empty;
    public bool ShowTitle { get; init; } = showTitle ?? true;
    public string CopyEventId { get; init; } = copyEventId ?? "copy-chart-as-image";
    public string SaveEventId { get; init; } = saveEventId ?? "save-chart-as-image";
    public List<string>? CostCodes { get; init; } = costCodes ?? [];
}