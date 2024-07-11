using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Web.App.ViewModels.Components;
namespace Web.App.ViewComponents;

public class CookiesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        if (HttpContext.Request.Cookies.ContainsKey(Constants.CookieSettingsName))
        {
            return new HtmlContentViewComponentResult(new HtmlString(string.Empty));
        }

        return View(new CookiesViewModel(Constants.CookieSettingsName));
    }
}