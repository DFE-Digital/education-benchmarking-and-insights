using EducationBenchmarking.Web.Infrastructure.Apis;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/financial-planning")]
public class SchoolPlanningController : Controller
{
    private readonly ILogger<SchoolPlanningController> _logger;

    public SchoolPlanningController(ILogger<SchoolPlanningController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (_logger.BeginScope(new {urn}))
        {
            try
            {
                var parentNode = new MvcBreadcrumbNode("Index", "School", "Your school") { RouteValues = new { urn } };
                var childNode = new MvcBreadcrumbNode("Index", "SchoolPlanning", "Curriculum and financial planning")
                {
                    RouteValues = new { urn },
                    Parent = parentNode
                };
                
                ViewData["BreadcrumbNode"] = childNode; 
                
                
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}