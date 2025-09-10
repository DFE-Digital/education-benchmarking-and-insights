using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[TrustAuthorization]
[Route("trust/{companyNumber}/financial-planning")]
[ValidateCompanyNumber]
[TrustRequestTelemetry(TrackedRequestFeature.Planning)]
public class TrustPlanningController(
    IEstablishmentApi establishmentApi,
    IFinancialPlanService financialPlanService,
    ILogger<TrustPlanningController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustPlanning(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var plans = await financialPlanService.List(trust.Schools.Select(x => x.URN).OfType<string>().ToArray());
                var viewModel = new TrustPlanningViewModel(trust, plans.ToArray());
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}