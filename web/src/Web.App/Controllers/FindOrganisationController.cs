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
        var vm = new FindOrganisationViewModel { FindMethod = method };
        return View(vm);
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
                    case OrganisationTypes.School:
                        {
                            if (string.IsNullOrWhiteSpace(viewModel.Urn) || string.IsNullOrEmpty(viewModel.SchoolInput))
                            {
                                var message = string.IsNullOrEmpty(viewModel.SchoolInput)
                                    ? "Enter a school or academy name, postcode or URN"
                                    : "Select a school or academy from the suggested list";
                                ModelState.AddModelError("school-input", message);
                                return View(viewModel);
                            }

                            return RedirectToAction("Index", "School", new { urn = viewModel.Urn });
                        }
                    case OrganisationTypes.Trust:
                        {
                            if (string.IsNullOrWhiteSpace(viewModel.CompanyNumber) ||
                                string.IsNullOrEmpty(viewModel.TrustInput))
                            {
                                var message = string.IsNullOrEmpty(viewModel.TrustInput)
                                    ? "Enter a trust name or company number"
                                    : "Select a trust from the suggested list";
                                ModelState.AddModelError("trust-input", message);
                                return View(viewModel);
                            }

                            return RedirectToAction("Index", "Trust", new { companyNumber = viewModel.CompanyNumber });
                        }
                    case OrganisationTypes.LocalAuthority:
                        {
                            if (string.IsNullOrWhiteSpace(viewModel.Code) || string.IsNullOrEmpty(viewModel.LaInput))
                            {
                                var message = string.IsNullOrEmpty(viewModel.LaInput)
                                    ? "Enter a valid local authority name"
                                    : "Select a local authority from the suggested list";
                                ModelState.AddModelError("la-input", message);
                                return View(viewModel);
                            }

                            return RedirectToAction("Index", "LocalAuthority", new { code = viewModel.Code });
                        }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(viewModel.FindMethod));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred finding an organisation: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}