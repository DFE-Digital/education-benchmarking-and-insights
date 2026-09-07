using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.App.Clarity;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ClarityViewComponent(IOptions<ClarityOptions> clarityOptions) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cookiePolicy = HttpContext.Request.Cookies[Constants.CookieSettingsName];
        var vm = new ClarityViewModel(
            cookiePolicy == "enabled",
            clarityOptions.Value.ProjectId);

        return View(vm);
    }
}
