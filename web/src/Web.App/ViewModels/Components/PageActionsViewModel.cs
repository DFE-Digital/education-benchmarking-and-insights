using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Web.App.ViewModels.Components;

public class PageActionsViewModel(
    PageActions[]? actions,
    string? saveClassName,
    string? saveFileName,
    string? saveTitleAttr,
    string? costCodesAttr,
    string? waitForEventType,
    string? downloadLink)
{
    public PageActions[] Actions => actions ?? [];

    public string SaveClassName { get; init; } = saveClassName ?? "chart-wrapper";
    public string SaveFileName { get; init; } = saveFileName ?? "charts.zip";
    public string SaveTitleAttr { get; init; } = saveTitleAttr ?? "aria-label";
    public string CostCodesAttr { get; init; } = costCodesAttr ?? "";

    public string WaitForEventType { get; init; } = waitForEventType ?? "";

    private Uri? DownloadLink { get; } = string.IsNullOrWhiteSpace(downloadLink) ? null : new Uri(downloadLink);
    public string DownloadAction => DownloadLink?.GetLeftPart(UriPartial.Path) ?? "#";
    public Dictionary<string, StringValues> DownloadParameters => QueryHelpers.ParseQuery(DownloadLink?.Query);
}