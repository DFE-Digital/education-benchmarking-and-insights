using Microsoft.AspNetCore.Mvc;
using Web.App.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class PhaseBannerViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var vm = new PhaseBannerViewModel();

        if (UserClaimsPrincipal.Claims.Any())
        {
            vm.Organisation = UserClaimsPrincipal.Organisation().Name;
        }

        return View(vm);
    }
}