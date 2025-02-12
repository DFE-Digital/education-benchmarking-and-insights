using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Web.App.ViewModels.Components;

public class PageActionsViewModel(
    bool? saveButtonVisible,
    bool? downloadButtonVisible,
    string? saveClassName,
    string? saveFileName,
    string? saveTitleAttr,
    string? waitForEventType,
    string? downloadLink)
{
    public bool SaveButtonVisible { get; init; } = saveButtonVisible == true;
    public bool DownloadButtonVisible { get; init; } = downloadButtonVisible == true;

    public string SaveClassName { get; init; } = saveClassName ?? "chart-wrapper";
    public string SaveFileName { get; init; } = saveFileName ?? "charts.zip";
    public string SaveTitleAttr { get; init; } = saveTitleAttr ?? "aria-label";

    public string WaitForEventType { get; init; } = waitForEventType ?? "";

    private Uri? DownloadLink { get; } = string.IsNullOrWhiteSpace(downloadLink) ? null : new Uri(downloadLink);
    public string DownloadAction => DownloadLink?.GetLeftPart(UriPartial.Path) ?? "#";
    public Dictionary<string, StringValues> DownloadParameters => QueryHelpers.ParseQuery(DownloadLink?.Query);
}