namespace Web.App.ViewModels.Enhancements;

public record PageActionsViewModel
{
    public string? ElementId { get; init; }
    public string? SaveClassName { get; init; }
    public string SaveEventId { get; init; } = "save-chart-as-image";
    public string? SaveFileName { get; init; }
    public string? SaveTitleAttr { get; init; }
    public string? CostCodesAttr { get; init; }
    public string? WaitForEventType { get; init; }
}