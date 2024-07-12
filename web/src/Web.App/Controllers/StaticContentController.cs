using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[Route("/")]
public class StaticContentController : Controller
{
    // TODO: review for public beta
    //[HttpGet]
    //[Route("help-with-this-service")]
    //public IActionResult ServiceHelp()
    //{
    //    return View();
    //}

    // TODO: review for public beta
    //[HttpGet]
    //[Route("submit-an-enquiry")]
    //public IActionResult SubmitEnquiry()
    //{
    //    return View();
    //}

    // TODO: review for public beta
    //[HttpGet]
    //[Route("ask-for-help")]
    //public IActionResult AskForHelp()
    //{
    //    return View();
    //}

    [HttpGet]
    [Route("cookies")]
    public IActionResult Cookies()
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;

        var cookiePolicy = HttpContext.Request.Cookies[Constants.CookieSettingsName];
        var vm = new StaticCookiesViewModel(Constants.CookieSettingsName, cookiePolicy != "disabled");
        return View(vm);
    }

    [HttpPost]
    [Route("cookies")]
    public IActionResult SaveCookies([FromForm(Name = "cookies-analytics")] bool analytics)
    {
        HttpContext.Response.Cookies.Append(Constants.CookieSettingsName, analytics ? "enabled" : "disabled", new CookieOptions
        {
            Path = "/",
            MaxAge = TimeSpan.FromDays(365),
            Secure = true,
            HttpOnly = true
        });
        if (!analytics)
        {
            HttpContext.Response.Cookies.Delete("ai_session");
            HttpContext.Response.Cookies.Delete("ai_user");
        }

        return RedirectToAction("Cookies");
    }

    // TODO: review for public beta
    //[HttpGet]
    //[Route("privacy")]
    //public IActionResult Privacy()
    //{
    //    ViewData[ViewDataKeys.UseJsBackLink] = true;
    //    return View();
    //}

    [HttpGet]
    [Route("contact")]
    public IActionResult Contact()
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;
        return View();
    }

    // TODO: review for public beta
    //[HttpGet]
    //[Route("accessibility")]
    //public IActionResult Accessibility()
    //{
    //    ViewData[ViewDataKeys.UseJsBackLink] = true;
    //    return View();
    //}
}