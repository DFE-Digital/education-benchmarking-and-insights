using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Infrastructure.Apis;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/find-ways-to-spend-less")]
public class LocalAuthorityResourcesController(
    ILogger<LocalAuthorityResourcesController> logger) : Controller
{
    [HttpGet]
    public IActionResult Index(string code)
    {

        using (logger.BeginScope(new { code }))
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority resources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}
