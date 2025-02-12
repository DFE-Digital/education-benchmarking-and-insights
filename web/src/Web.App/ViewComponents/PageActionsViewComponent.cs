using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class PageActionsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
        bool? saveButtonVisible,
        bool? downloadButtonVisible,
        string? saveClassName,
        string? saveFileName,
        string? saveTitleAttr,
        string? waitForEventType,
        string? downloadLink)
        => View(new PageActionsViewModel(
            saveButtonVisible,
            downloadButtonVisible,
            saveClassName,
            saveFileName,
            saveTitleAttr,
            waitForEventType,
            downloadLink));
}