using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class PageActionsSsrViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
        PageActions[]? actions,
        string? saveClassName,
        string? saveFileName,
        string? saveTitleAttr,
        string? costCodesAttr,
        string? waitForEventType,
        string? downloadLink)
    {
        return View(new PageActionsViewModel(
            actions,
            saveClassName,
            saveFileName,
            saveTitleAttr,
            costCodesAttr,
            waitForEventType,
            downloadLink));
    }
}