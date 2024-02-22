using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/investigation")]
public class SchoolInvestigationAreaController : Controller
{
    [HttpGet]
    public IActionResult Index(string urn)
    {
        ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.DataDashboard(urn);

        return View(new SchoolInvestigationViewModel { Urn = urn });
    }
}