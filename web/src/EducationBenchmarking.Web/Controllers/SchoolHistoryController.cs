using EducationBenchmarking.Web.Infrastructure.Apis;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/history")]
public class SchoolHistoryController : Controller
{
    [HttpGet]
    public IActionResult Index(string urn)
    {
        var parentNode = new MvcBreadcrumbNode("Index", "School", "Your school") { RouteValues = new { urn } };
        var childNode = new MvcBreadcrumbNode("Index", "SchoolHistory", "Historic data")
        {
            RouteValues = new { urn },
            Parent = parentNode
        };

        ViewData[ViewDataConstants.BreadcrumbNode] = childNode;

        return View();
    }
}