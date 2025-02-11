namespace Web.App.ViewModels.Components;

public class PageActionsViewModel(string? className, string? fileName, string? titleAttr, string? waitForEventType)
{
    public string ClassName { get; set; } = className ?? "chart-wrapper";
    public string FileName { get; set; } = fileName ?? "charts.zip";
    public string TitleAttr { get; set; } = titleAttr ?? "aria-label";
    public string WaitForEventType { get; set; } = waitForEventType ?? "";
}