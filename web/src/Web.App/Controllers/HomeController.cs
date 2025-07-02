using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace Web.App.Controllers;

[Controller]
[Route("/")]
public class HomeController : Controller
{

    [HttpGet]
    [DefaultBreadcrumb(PageTitles.ServiceHome)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("sign-out")]
    public IActionResult Signout()
    {
        if (HttpContext.User.Identity is { IsAuthenticated: true })
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("sign-in")]
    public IActionResult Signin([FromQuery] string redirectUri)
    {
        if (HttpContext.User.Identity is { IsAuthenticated: true })
        {
            return RedirectToAction("Index");
        }

        var props = new AuthenticationProperties
        {
            RedirectUri = redirectUri
        };
        return Challenge(props, OpenIdConnectDefaults.AuthenticationScheme);
    }
}