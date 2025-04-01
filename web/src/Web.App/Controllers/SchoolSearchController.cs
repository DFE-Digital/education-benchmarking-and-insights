using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.ViewModels;
using Web.App.ViewModels.Search;

namespace Web.App.Controllers;

[Controller]
[Route("school")]
[SchoolRequestTelemetry(TrackedRequestFeature.Search)]
[FeatureGate(FeatureFlags.FacetedSearch)]
public class SchoolSearchController(
    ILogger<SchoolSearchController> logger)
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
        [FromQuery(Name = "sort")] string? orderBy,
        [FromQuery] int? page,
        [FromQuery(Name = "phase")] string[] overallPhase)
    {
        using (logger.BeginScope(new
        {
            term,
            orderBy,
            page,
            overallPhase
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
                },
                Results =
                [
                    new SchoolSearchResultViewModel
                    {
                        URN = "123456",
                        SchoolName = "School Name 1",
                        AddressStreet = "Street",
                        AddressTown = "Town",
                        AddressPostcode = "Postcode"
                    },
                    new SchoolSearchResultViewModel
                    {
                        URN = "654321",
                        SchoolName = "School Name 2",
                        AddressStreet = "Street",
                        AddressTown = "Town",
                        AddressPostcode = "Postcode"
                    }
                ]
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