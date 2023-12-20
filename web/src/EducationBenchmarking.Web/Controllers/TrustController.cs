using EducationBenchmarking.Web.Infrastructure.Apis;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("trust/{companyNumber}")]
public class TrustController : Controller
{
    private readonly ILogger<TrustController> _logger;

    public TrustController(ILogger<TrustController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (_logger.BeginScope(new { companyNumber }))
        {
            try
            {
                var node = new MvcBreadcrumbNode("Index", "Trust", "Your trust") { RouteValues = new { companyNumber } };

                ViewData["BreadcrumbNode"] = node;
                
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error displaying trust details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}