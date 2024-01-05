using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/investigation")]
public class SchoolInvestigationAreaController : Controller
{
    private readonly ILogger<SchoolInvestigationAreaController> _logger;

    public SchoolInvestigationAreaController(ILogger<SchoolInvestigationAreaController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (_logger.BeginScope(new { urn }))
        {
            try
            {
                var parentNode = new MvcBreadcrumbNode("Index", "School", "Your school") { RouteValues = new { urn } };
                var childNode = new MvcBreadcrumbNode("Index", "SchoolDashboard", "View your data dashboard")
                {
                    RouteValues = new { urn },
                    Parent = parentNode
                };

                ViewData["BreadcrumbNode"] = childNode;

                return View(new SchoolInvestigationViewModel { Urn = urn });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}