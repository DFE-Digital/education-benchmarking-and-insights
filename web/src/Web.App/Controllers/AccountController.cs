using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Web.App.Controllers;

[Controller]
[Route("account")]
public class AccountController : Controller
{
    [HttpGet]
    [Route("sign-out")]
    public IActionResult Signout()
    {
        if (HttpContext.User.Identity is { IsAuthenticated: true })
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("sign-in")]
    public IActionResult Signin([FromQuery] string redirectUri)
    {
        if (HttpContext.User.Identity is { IsAuthenticated: true })
        {
            return RedirectToAction("Index", "Home");
        }

        var props = new AuthenticationProperties
        {
            RedirectUri = redirectUri
        };
        return Challenge(props, OpenIdConnectDefaults.AuthenticationScheme);
    }
}