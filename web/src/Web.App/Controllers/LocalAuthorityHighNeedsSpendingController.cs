using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/high-needs-spending")]
[ValidateLaCode]
public class LocalAuthorityHighNeedsSpendingController
    : Controller
{
    [HttpGet]
    public IActionResult Index(string code)
    {
        return new ContentResult();
    }
}