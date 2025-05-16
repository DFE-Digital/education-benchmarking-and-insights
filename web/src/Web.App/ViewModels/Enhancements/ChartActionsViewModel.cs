namespace Web.App.ViewModels.Enhancements;

public class ChartActionsViewModel
{
    public string CopyEventId { get; init; } = "copy-chart-as-image";
    public string? DataSetAttribute { get; init; }
    public string SaveEventId { get; init; } = "save-chart-as-image";
    public bool? ShowCopy { get; init; }
    public bool? ShowSave { get; init; }
    public bool? ShowTitle { get; init; }
}