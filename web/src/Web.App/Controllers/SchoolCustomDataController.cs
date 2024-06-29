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
[FeatureGate(FeatureFlags.CustomData)]
[Route("school/{urn}/custom-data")]
[SchoolRequestTelemetry(TrackedRequestFeature.CustomisedData)]
public class SchoolCustomDataController(
    IEstablishmentApi establishmentApi,
    IUserDataService userDataService,
    ICustomDataService customDataService,
    ILogger<SchoolCustomDataController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomData(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolCustomDataLandingViewModel(school);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school data: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("revert")]
    [SchoolAuthorization]
    [FeatureGate(FeatureFlags.UserDefinedComparators)]
    public async Task<IActionResult> Revert(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomData(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolCustomDataLandingViewModel(school);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error reverting school custom data: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("revert")]
    [SchoolAuthorization]
    [FeatureGate(FeatureFlags.UserDefinedComparators)]
    public async Task<IActionResult> RevertCustomData(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomData(urn);

                var userData = await userDataService.GetSchoolDataAsync(User.UserId(), urn);
                if (userData.CustomData != null)
                {
                    await customDataService.RemoveCustomData(urn, userData.CustomData);
                    customDataService.ClearCustomDataFromSession(urn);
                }

                return RedirectToAction("Index", "School", new
                {
                    urn
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error reverting school custom data: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}