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
}