using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/education-health-care-plans")]
[ValidateLaCode]
public class LocalAuthorityEducationHealthCarePlansController 
    : Controller
{
    [HttpGet]
    public IActionResult Index(string code)
    {
        return new ContentResult();
    }
}
