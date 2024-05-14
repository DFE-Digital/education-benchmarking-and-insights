using Microsoft.AspNetCore.Mvc;

namespace Web.App.Controllers;

[Controller]
[Route("error")]
public class ErrorController : Controller
{
    [HttpGet]
    [HttpPost]
    public IActionResult Problem()
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;
        return View();
    }

    [HttpGet]
    [HttpPost]
    [Route("{statusCode:int}")]
    public IActionResult StatusCodeError(int statusCode)
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;

        return statusCode switch
        {
            401 => View("AccessDenied"),
            404 => View("NotFound"),
            403 => View("AccessDenied"),
            _ => View("Problem")
        };
    }
}