using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels;

namespace Web.App.Controllers;

// This feature is documented at /documentation/developers/features/find-organisation.md
[Controller]
[Route("find-organisation")]
public class FindOrganisationController(ILogger<FindOrganisationController> logger) : Controller
{
    [HttpGet]
    public IActionResult Index(string method = OrganisationTypes.School)
    {
        var vm = new FindOrganisationViewModel
        {
            FindMethod = method
        };
        return View(vm);
    }

    [HttpPost]
    public IActionResult Index([FromForm] FindOrganisationViewModel viewModel)
    {
        using (logger.BeginScope(new
               {
                   viewModel
               }))
        {
            try
            {
                return FilteredSearchResult(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred finding an organisation: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }

    private IActionResult FilteredSearchResult(FindOrganisationSelectViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        return viewModel.FindMethod switch
        {
            OrganisationTypes.School => RedirectToAction("Index", "SchoolSearch"),
            OrganisationTypes.Trust => RedirectToAction("Index", "TrustSearch"),
            OrganisationTypes.LocalAuthority => RedirectToAction("Index", "LocalAuthoritySearch"),
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel.FindMethod))
        };
    }
}