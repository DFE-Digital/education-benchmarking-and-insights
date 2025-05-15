using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ChartActionsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
        string? elementId,
        string? title,
        bool? showTitle,
        string? copyEventId,
        string? saveEventId,
        List<string>? costCodes,
        bool? progressivelyEnhance)
    {
        return View(new ChartActionsViewModel(
            elementId,
            title,
            showTitle,
            copyEventId,
            saveEventId,
            costCodes,
            progressivelyEnhance));
    }
}