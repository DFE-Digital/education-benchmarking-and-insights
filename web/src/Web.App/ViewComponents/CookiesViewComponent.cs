using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;
namespace Web.App.ViewComponents;

public class CookiesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        if (HttpContext.Request.Cookies.ContainsKey(Constants.CookieSettingsName))
        {
            return new EmptyContentView();
        }

        return View(new CookiesViewModel(Constants.CookieSettingsName));
    }
}