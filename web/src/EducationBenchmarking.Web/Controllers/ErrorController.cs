using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("error")]
public class ErrorController : Controller
{
    [HttpGet]
    public IActionResult Problem()
    {
        return View();
    }
    
    [HttpGet]
    [Route("{statusCode:int}")]
    public IActionResult StatusCodeError(int statusCode)
    {
        switch (statusCode)
        {
            case 404:
                return View("NotFound");
            default:
                return View("Problem");
        }
    }
}