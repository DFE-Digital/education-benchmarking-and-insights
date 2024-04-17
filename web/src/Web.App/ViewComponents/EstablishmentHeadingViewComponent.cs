using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class EstablishmentHeadingViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string title, string name, string id, string kind)
    {
        return View(new EstablishmentHeadingViewModel(title, name, id, kind));
    }
}