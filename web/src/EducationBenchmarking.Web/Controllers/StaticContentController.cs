using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

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
    [Route("find-commercial-resources")]
    public IActionResult CommercialResources()
    {
        return View();
    }
}