using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[SchoolAuthorization]
[FeatureGate(FeatureFlags.CurriculumFinancialPlanning)]
[Route("school/{urn}/financial-planning")]
public class SchoolPlanningController(
    IEstablishmentApi establishmentApi,
    IFinancialPlanService financialPlanService,
    ILogger<SchoolPlanningController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolPlanning(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plans = await financialPlanService.List(urn);
                var viewModel = new SchoolPlanViewModel(school, plans);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("{year:int}")]
    public async Task<IActionResult> View(string urn, int year, string? referrer)
    {
        using (logger.BeginScope(new { urn, year, referrer }))
        {
            try
            {
                var backAction = referrer == Referrers.TeachingPeriodsManager
                    ? Url.Action("TeachingPeriodsManager", "SchoolPlanningCreate", new { urn, year })
                    : Url.Action("Index", new { urn });

                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(backAction);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await financialPlanService.Get(urn, year);
                var viewModel = new SchoolDeploymentPlanViewModel(school, plan, referrer);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}