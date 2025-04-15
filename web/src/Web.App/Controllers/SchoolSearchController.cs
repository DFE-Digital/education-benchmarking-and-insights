using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Services;
using Web.App.ViewModels;
using Web.App.ViewModels.Search;

namespace Web.App.Controllers;

[Controller]
[Route("school")]
[SchoolRequestTelemetry(TrackedRequestFeature.Search)]
[FeatureGate(FeatureFlags.FilteredSearch)]
public class SchoolSearchController(
    ILogger<SchoolSearchController> logger,
    ISearchService searchService)
    : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new FindSchoolViewModel());
    }

    [HttpPost]
    public IActionResult Index(FindSchoolViewModel viewModel)
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
            SearchResponse<SchoolSummary> results;
            try
            {
                results = await searchService.SchoolSearch(term, 50, page, overallPhase.Length == 0
                        ? null
                        : new Dictionary<string, IEnumerable<string>>
                        {
                            {
                                "OverallPhase", overallPhase
                            }
                        },
                    string.IsNullOrWhiteSpace(orderBy) ? null : ("SchoolName", orderBy)
                );
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unable to search for school");
                return View(new SchoolSearchResultsViewModel
                {
                    Term = term,
                    Success = false
                });
            }

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