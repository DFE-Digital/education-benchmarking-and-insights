using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/high-needs/benchmarking")]
[ValidateLaCode]
public class LocalAuthorityHighNeedsBenchmarkingController(
    ILogger<LocalAuthorityHighNeedsBenchmarkingController> logger,
    IEstablishmentApi establishmentApi,
    IFinanceService financeService,
    ILocalAuthorityComparatorSetService localAuthorityComparatorSetService)
    : Controller
{
    [HttpGet]
    [LocalAuthorityRequestTelemetry(TrackedRequestFeature.HighNeeds)]
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityHome(code);

                var localAuthority = await establishmentApi
                    .GetLocalAuthority(code)
                    .GetResultOrDefault<LocalAuthority>();
                if (localAuthority == null)
                {
                    return NotFound();
                }

                var set = localAuthorityComparatorSetService.ReadUserDefinedComparatorSetFromSession(code).Set;
                if (set.Length == 0)
                {
                    return RedirectToAction("Index", "LocalAuthorityComparators", new { code });
                }

                var years = await financeService.GetYears();

                return View(new LocalAuthorityHighNeedsBenchmarkingViewModel(localAuthority, set, years));
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority high needs benchmarking: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}