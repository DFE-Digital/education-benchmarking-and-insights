using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[TrustAuthorization]
[FeatureGate(FeatureFlags.Trusts, FeatureFlags.CurriculumFinancialPlanning)]
[Route("trust/{companyNumber}/financial-planning")]
public class TrustPlanningController(
    IEstablishmentApi establishmentApi,
    IFinancialPlanService financialPlanService,
    ILogger<TrustPlanningController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (logger.BeginScope(new { companyNumber }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustPlanning(companyNumber);
                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var trustQuery = new ApiQuery().AddIfNotNull("companyNumber", companyNumber);
                var schools = await establishmentApi.QuerySchools(trustQuery).GetResultOrDefault<School[]>() ?? [];

                var plans = await financialPlanService.List(schools.Select(x => x.URN).OfType<string>().ToArray());
                var viewModel = new TrustPlanningViewModel(trust, schools, plans.ToArray());

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