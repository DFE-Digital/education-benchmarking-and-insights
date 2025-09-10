using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Services;
using Web.App.ViewModels.Search;

namespace Web.App.Controllers;

[Controller]
[Route("school")]
[SchoolRequestTelemetry(TrackedRequestFeature.Search)]
public class SchoolSearchController(
    ILogger<SchoolSearchController> logger,
    ISearchService searchService)
    : Controller
{
    [HttpGet]
    public IActionResult Index() => View(new FindSchoolViewModel());

    [HttpPost]
    public IActionResult Index(FindSchoolViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if (!string.IsNullOrWhiteSpace(viewModel.EstablishmentId))
        {
            return RedirectToAction("Index", "School", new
            {
                urn = viewModel.EstablishmentId
            });
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
        [FromQuery(Name = "phase")] string[] overallPhase,
        [FromQuery(Name = "sort")] string? orderBy
    )
    {
        using (logger.BeginScope(new
        {
            term,
            page,
            overallPhase,
            orderBy
        }))
        {
            var results = await searchService.SchoolSearch(term, 50, page, overallPhase.Length == 0
                    ? null
                    : new SearchFilters("OverallPhase", overallPhase),
                string.IsNullOrWhiteSpace(orderBy) ? null : new SearchOrderBy("SchoolNameSortable", orderBy)
            );

            return View(new SchoolSearchResultsViewModel
            {
                Term = term,
                OrderBy = orderBy,
                OverallPhase = overallPhase,
                TotalResults = results.TotalResults,
                PageNumber = results.Page,
                PageSize = results.PageSize,
                Results = results.Results.Select(SchoolSearchResultViewModel.Create).ToArray()
            });
        }
    }

    [HttpPost]
    [Route("search")]
    public IActionResult Search(SchoolSearchViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(new SchoolSearchResultsViewModel
            {
                Term = viewModel.Term,
                OrderBy = viewModel.OrderBy,
                OverallPhase = viewModel.OverallPhase
            });
        }

        if (!string.IsNullOrWhiteSpace(viewModel.EstablishmentId))
        {
            return RedirectToAction("Index", "School", new
            {
                urn = viewModel.EstablishmentId
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
            sort = viewModel.OrderBy,
            phase = viewModel.OverallPhase
        });
    }
}