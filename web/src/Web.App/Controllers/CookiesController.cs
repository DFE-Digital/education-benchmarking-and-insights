using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[Route("/cookies")]
public class CookiesController : Controller
{
    [HttpGet]
    [ImportModelState]
    public IActionResult Index([FromQuery(Name = "cookies-saved")] bool cookiesSaved = false)
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;

        var cookiePolicy = HttpContext.Request.Cookies[Constants.CookieSettingsName];
        var vm = new StaticCookiesViewModel(Constants.CookieSettingsName, cookiePolicy switch
        {
            "enabled" => true,
            "disabled" => false,
            _ => null
        }, cookiesSaved);
        return View(vm);
    }

    [HttpPost]
    [ExportModelState]
    public IActionResult Save([FromForm] bool? analyticsCookiesEnabled)
    {
        if (analyticsCookiesEnabled == null)
        {
            ModelState.AddModelError(nameof(StaticCookiesViewModel.AnalyticsCookiesEnabled), "Select if you want to accept analytics cookies");
            return RedirectToAction("Index");
        }

        HttpContext.Response.Cookies.Append(Constants.CookieSettingsName, analyticsCookiesEnabled == true ? "enabled" : "disabled", new CookieOptions
        {
            Path = "/",
            MaxAge = TimeSpan.FromDays(365),
            Secure = true,
            HttpOnly = true
        });
        if (analyticsCookiesEnabled == false)
        {
            HttpContext.Response.Cookies.Delete("ai_session");
            HttpContext.Response.Cookies.Delete("ai_user");
        }

        return RedirectToAction("Index", new Dictionary<string, string>
        {
            {
                "cookies-saved", "true"
            }
        });
    }
}