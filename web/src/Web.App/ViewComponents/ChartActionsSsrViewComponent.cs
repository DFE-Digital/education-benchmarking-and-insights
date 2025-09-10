using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ChartActionsSsrViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
        string? elementId,
        string? title,
        bool? showTitle,
        string? copyEventId,
        string? saveEventId,
        List<string>? costCodes) => View(new ChartActionsViewModel(
        elementId,
        title,
        showTitle,
        copyEventId,
        saveEventId,
        costCodes));
}