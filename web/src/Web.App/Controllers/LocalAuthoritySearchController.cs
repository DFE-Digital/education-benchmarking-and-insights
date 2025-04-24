using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.ViewModels.Search;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority")]
[LocalAuthorityRequestTelemetry(TrackedRequestFeature.Search)]
[FeatureGate(FeatureFlags.FilteredSearch)]
public class LocalAuthoritySearchController(ILogger<LocalAuthoritySearchController> logger)
    : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new FindLocalAuthorityViewModel());
    }

    [HttpPost]
    public IActionResult Index(FindLocalAuthorityViewModel viewModel)
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