using Microsoft.AspNetCore.Mvc;

namespace Web.App.Controllers;

[Controller]
[Route("/")]
public class StaticContentController : Controller
{
    [HttpGet]
    [Route("help-with-this-service")]
    public IActionResult ServiceHelp()
    {
        return View();
    }

    [HttpGet]
    [Route("submit-an-enquiry")]
    public IActionResult SubmitEnquiry()
    {
        return View();
    }

    [HttpGet]
    [Route("ask-for-help")]
    public IActionResult AskForHelp()
    {
        return View();
    }

    [HttpGet]
    [Route("cookies")]
    public IActionResult Cookies()
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;
        return View();
    }

    [HttpGet]
    [Route("privacy")]
    public IActionResult Privacy()
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;
        return View();
    }

    [HttpGet]
    [Route("contact")]
    public IActionResult Contact()
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;
        return View();
    }

    [HttpGet]
    [Route("accessibility")]
    public IActionResult Accessibility()
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;
        return View();
    }
}