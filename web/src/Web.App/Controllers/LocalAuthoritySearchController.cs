using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Services;
using Web.App.ViewModels.Search;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority")]
[LocalAuthorityRequestTelemetry(TrackedRequestFeature.Search)]
public class LocalAuthoritySearchController(
    ILogger<LocalAuthoritySearchController> logger,
    ISearchService searchService)
    : Controller
{
    [HttpGet]
    public IActionResult Index() => View(new FindLocalAuthorityViewModel());

    [HttpPost]
    public IActionResult Index(FindLocalAuthorityViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if (!string.IsNullOrWhiteSpace(viewModel.EstablishmentId))
        {
            return RedirectToAction("Index", "LocalAuthority", new
            {
                code = viewModel.EstablishmentId
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

            return View(new LocalAuthoritySearchResultsViewModel
            {
                Term = term,
                OrderBy = orderBy,
                TotalResults = results.TotalResults,
                PageNumber = results.Page,
                PageSize = results.PageSize,
                Results = results.Results.Select(LocalAuthoritySearchResultViewModel.Create).ToArray()
            });
        }
    }

    [HttpPost]
    [Route("search")]
    public IActionResult Search(LocalAuthoritySearchViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(new LocalAuthoritySearchResultsViewModel
            {
                Term = viewModel.Term,
                OrderBy = viewModel.OrderBy
            });
        }

        if (!string.IsNullOrWhiteSpace(viewModel.EstablishmentId))
        {
            return RedirectToAction("Index", "LocalAuthority", new
            {
                code = viewModel.EstablishmentId
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