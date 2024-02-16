using Microsoft.AspNetCore.Mvc;


namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/history")]
public class SchoolHistoryController : Controller
{
    [HttpGet]
    public IActionResult Index(string urn)
    {
        ViewData[ViewDataConstants.BreadcrumbNode] = BreadcrumbNodes.HistoricData(urn);

        return View();
    }
}