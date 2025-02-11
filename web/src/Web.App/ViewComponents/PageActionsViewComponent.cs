using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class PageActionsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string? title, string? name, string? id, string? kind)
        => View(new PageActionsViewModel(title, name, id, kind));
}