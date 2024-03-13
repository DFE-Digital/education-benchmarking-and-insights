using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ResourcesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, Resources[] resources)
    {
        return View(new ResourcesViewModel(identifier, resources));
    }
}