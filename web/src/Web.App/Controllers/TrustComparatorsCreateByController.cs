using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[Authorize]
[FeatureGate(FeatureFlags.UserDefinedComparators, FeatureFlags.Trusts)]
[Route("trust/{companyNumber}/comparators/create")]
public class TrustComparatorsCreateByController(
    ILogger<TrustComparatorsCreateByController> logger,
    IEstablishmentApi establishmentApi
) : Controller
{
    [HttpGet]
    [Route("by")]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            urn = companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparators(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var viewModel = new TrustComparatorsViewModel(trust);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create trust comparators by: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("by")]
    public async Task<IActionResult> Index(string companyNumber, [FromForm] string? by)
    {
        if (!string.IsNullOrWhiteSpace(by))
        {
            return by.Equals("name", StringComparison.OrdinalIgnoreCase)
                ? RedirectToAction("Name", new
                {
                    companyNumber
                })
                : RedirectToAction("Characteristic", new
                {
                    companyNumber
                });
        }

        ModelState.AddModelError(nameof(by), "Select whether to choose trusts by name or characteristic");
        ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparators(companyNumber);

        var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
        var viewModel = new TrustComparatorsViewModel(trust, by);
        return View(viewModel);
    }

    [HttpGet]
    [Route("by/name")]
    public IActionResult Name(string companyNumber) => new StatusCodeResult(StatusCodes.Status302Found);

    [HttpGet]
    [Route("by/characteristic")]
    public IActionResult Characteristic(string companyNumber) => new StatusCodeResult(StatusCodes.Status302Found);
}