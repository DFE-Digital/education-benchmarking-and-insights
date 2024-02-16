using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("find-organisation")]
public class FindOrganisationController(ILogger<FindOrganisationController> logger) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new FindOrganisationViewModel());
    }

    [HttpPost]
    public IActionResult Index([FromForm] FindOrganisationViewModel viewModel)
    {
        using (logger.BeginScope(new { viewModel }))
        {
            try
            {
                switch (viewModel.FindMethod.ToLower())
                {
                    case Constants.SchoolOrganisationType:
                        {
                            if (string.IsNullOrWhiteSpace(viewModel.Urn) || string.IsNullOrEmpty(viewModel.SchoolInput))
                            {
                                var message = string.IsNullOrEmpty(viewModel.SchoolInput)
                                    ? "Enter a school name select a school"
                                    : "Please select school from the suggester";
                                ModelState.AddModelError("school-input", message);
                                return View(viewModel);
                            }

                            return RedirectToAction("Index", "School", new { urn = viewModel.Urn });
                        }
                    case Constants.TrustOrganisationType:
                        {
                            if (string.IsNullOrWhiteSpace(viewModel.CompanyNumber) ||
                                string.IsNullOrEmpty(viewModel.TrustInput))
                            {
                                var message = string.IsNullOrEmpty(viewModel.TrustInput)
                                    ? "Enter a trust name select a trust"
                                    : "Please select trust from the suggester";
                                ModelState.AddModelError("trust-input", message);
                                return View(viewModel);
                            }

                            return RedirectToAction("Index", "Trust", new { companyNumber = viewModel.CompanyNumber });
                        }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(viewModel.FindMethod));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred finding an organisation: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }


    [HttpGet]
    [Route("v2")]
    public IActionResult V2()
    {
        return View(new FindOrganisationViewModelV2());
    }

    [HttpPost]
    [Route("v2")]
    public IActionResult V2([FromForm] FindOrganisationViewModelV2 viewModel)
    {
        using (logger.BeginScope(new { viewModel }))
        {
            try
            {
                if (string.IsNullOrWhiteSpace(viewModel.Identifier) ||
                    string.IsNullOrWhiteSpace(viewModel.Kind) ||
                    string.IsNullOrEmpty(viewModel.Input))
                {
                    var message = string.IsNullOrEmpty(viewModel.Input)
                        ? "Enter a organisation name select a organisation"
                        : "Please select organisation from the suggester";
                    ModelState.AddModelError("organisation-input", message);
                    return View(viewModel);
                }

                return viewModel.Kind.ToLower() switch
                {
                    "school" => RedirectToAction("Index", "School", new { urn = viewModel.Identifier }),
                    _ => throw new ArgumentOutOfRangeException(nameof(viewModel.Kind))
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred finding an organisation: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
}