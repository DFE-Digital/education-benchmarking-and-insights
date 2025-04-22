using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.ViewModels.Search;

namespace Web.App.Controllers;

[Controller]
[Route("trust")]
[TrustRequestTelemetry(TrackedRequestFeature.Search)]
[FeatureGate(FeatureFlags.FilteredSearch)]
public class TrustSearchController(ILogger<TrustSearchController> logger)
    : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new FindTrustViewModel());
    }

    [HttpPost]
    public IActionResult Index(FindTrustViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        return RedirectToAction("Search", new
        {
            term = viewModel.Term
        });
    }

    [HttpGet]
    [Route("search")]
    public IActionResult Search(
        [FromQuery] string? term,
        [FromQuery] int? page,
        [FromQuery(Name = "sort")] string? orderBy
    )
    {
        using (logger.BeginScope(new
        {
            term,
            page,
            orderBy
        }))
        {
            return NotFound();
        }
    }
}