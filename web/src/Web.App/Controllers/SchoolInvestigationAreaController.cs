using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels;

namespace Web.App.Controllers;

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