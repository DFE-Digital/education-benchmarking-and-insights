using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class CustomDataBannerViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string name, string id)
    {
        var vm = new CustomDataBannerViewModel(name, id);

        return View(vm);
    }
}
