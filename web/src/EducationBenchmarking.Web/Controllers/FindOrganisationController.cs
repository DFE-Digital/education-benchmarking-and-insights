using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("find-organisation")]
public class FindOrganisationController : Controller
{
    private readonly ILogger<FindOrganisationController> _logger;

    public FindOrganisationController(ILogger<FindOrganisationController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index(string findMethod = Constants.SchoolOrganisationType)
    {
        return View(new FindOrganisationViewModel { FindMethod = findMethod });
    }

    [HttpPost]
    public IActionResult Index([FromForm] FindOrganisationViewModel viewModel)
    {
        using (_logger.BeginScope(new { viewModel }))
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
                        if (string.IsNullOrWhiteSpace(viewModel.CompanyNumber) || string.IsNullOrEmpty(viewModel.TrustInput))
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
                _logger.LogError(e, "An error occurred finding an organisation: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
}