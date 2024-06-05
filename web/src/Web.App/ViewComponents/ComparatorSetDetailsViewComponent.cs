using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ComparatorSetDetailsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, bool hasUserDefinedSet)
    {
        return View(new ComparatorSetDetailsViewModel(identifier, hasUserDefinedSet));
    }
}