using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/investigation")]
public class SchoolInvestigationAreaController : Controller
{
    [HttpGet]
    public IActionResult Index(string urn)
    {
        var parentNode = new MvcBreadcrumbNode("Index", "School", "Your school") { RouteValues = new { urn } };
        var childNode = new MvcBreadcrumbNode("Index", "SchoolDashboard", "View your data dashboard")
        {
            RouteValues = new { urn },
            Parent = parentNode
        };

        ViewData[ViewDataConstants.BreadcrumbNode] = childNode;

        return View(new SchoolInvestigationViewModel { Urn = urn });
    }
}