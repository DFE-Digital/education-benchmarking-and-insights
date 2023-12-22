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
        return statusCode switch
        {
            404 => View("NotFound"),
            _ => View("Problem")
        };
    }
}