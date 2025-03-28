using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/search")]
[SchoolRequestTelemetry(TrackedRequestFeature.Search)]
public class SchoolSearchController(
    ILogger<SchoolSearchController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(
        [FromQuery] string term,
        [FromQuery(Name = "redirect")] bool redirectIfDistinct = false)
    {
        using (logger.BeginScope(new
        {
            term,
            redirectIfDistinct
        }))
        {
            await Task.CompletedTask; // todo

            return View(new SchoolSearchViewModel
            {
                Term = term
            });
        }
    }

    [HttpPost]
    public IActionResult Index(SchoolSearchViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        return RedirectToAction("Index", new
        {
            term = viewModel.Term
        });
    }
}