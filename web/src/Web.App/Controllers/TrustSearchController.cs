using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Services;
using Web.App.ViewModels.Search;

namespace Web.App.Controllers;

[Controller]
[Route("trust")]
[TrustRequestTelemetry(TrackedRequestFeature.Search)]
[FeatureGate(FeatureFlags.FilteredSearch)]
public class TrustSearchController(
    ILogger<TrustSearchController> logger,
    ISearchService searchService)
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
            var results = await searchService.TrustSearch(term, 50, page, string.IsNullOrWhiteSpace(orderBy) ? null : new SearchOrderBy("TrustNameSortable", orderBy));

            return View(new TrustSearchResultsViewModel
            {
                Term = term,
                OrderBy = orderBy,
                TotalResults = results.TotalResults,
                PageNumber = results.Page,
                PageSize = results.PageSize,
                Results = results.Results.Select(TrustSearchResultViewModel.Create).ToArray()
            });
        }
    }

    [HttpPost]
    [Route("search")]
    public IActionResult Search(TrustSearchViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(new TrustSearchResultsViewModel
            {
                Term = viewModel.Term,
                OrderBy = viewModel.OrderBy
            });
        }

        // reset search options if new search term provided
        if (viewModel.Action == FormAction.Reset)
        {
            return RedirectToAction("Search", new
            {
                term = viewModel.Term
            });
        }

        return RedirectToAction("Search", new
        {
            term = viewModel.Term,
            sort = viewModel.OrderBy
        });
    }
}