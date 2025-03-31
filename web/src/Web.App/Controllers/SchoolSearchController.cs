using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.ViewModels.Search;

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
        [FromQuery] string? term,
        [FromQuery(Name = "sort")] string? orderBy,
        [FromQuery] int? page,
        [FromQuery(Name = "phase")] string? overallPhase,
        [FromQuery(Name = "redirect")] bool redirectIfDistinct = false)
    {
        using (logger.BeginScope(new
        {
            term,
            orderBy,
            page,
            overallPhase,
            redirectIfDistinct
        }))
        {
            await Task.CompletedTask; // todo: call search api

            return View(new SchoolSearchResultsViewModel
            {
                Term = term,
                OrderBy = orderBy,
                TotalResults = 54,
                PageNumber = page ?? 1,
                OverallPhase = overallPhase,
                Facets = new Dictionary<string, IList<SearchResultFacetViewModel>>
                {
                    {
                        "OverallPhase", new List<SearchResultFacetViewModel>
                        {
                            new()
                            {
                                Value = "Primary",
                                Count = 1
                            },
                            new()
                            {
                                Value = "Secondary",
                                Count = 2
                            }
                        }
                    }
                }
            });
        }
    }

    [HttpPost]
    public IActionResult Index(SchoolSearchViewModel viewModel)
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
            return RedirectToAction("Index", new
            {
                term = viewModel.Term
            });
        }

        return RedirectToAction("Index", new
        {
            term = viewModel.Term,
            sort = viewModel.OrderBy,
            phase = viewModel.OverallPhase
        });
    }
}