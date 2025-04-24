using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Services;
using Web.App.ViewModels.Search;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority")]
[LocalAuthorityRequestTelemetry(TrackedRequestFeature.Search)]
[FeatureGate(FeatureFlags.FilteredSearch)]
public class LocalAuthoritySearchController(
    ILogger<LocalAuthoritySearchController> logger,
    ISearchService searchService)
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
    public async Task<IActionResult> Search(
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
            var results = await searchService.LocalAuthoritySearch(term, 50, page, string.IsNullOrWhiteSpace(orderBy) ? null : new SearchOrderBy("LocalAuthorityNameSortable", orderBy));

            return NotFound();
        }
    }
}