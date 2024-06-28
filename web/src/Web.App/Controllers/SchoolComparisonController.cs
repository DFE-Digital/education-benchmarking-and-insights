using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/comparison")]
public class SchoolComparisonController(
    IEstablishmentApi establishmentApi,
    ILogger<SchoolComparisonController> logger,
    IUserDataService userDataService)
    : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.Comparators)]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparison(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var userData = await userDataService.GetSchoolDataAsync(User.UserId(), urn);
                var viewModel = new SchoolComparisonViewModel(school, userData.ComparatorSet, userData.CustomData);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("custom-data")]
    [SchoolAuthorization]
    [FeatureGate(FeatureFlags.CustomData)]
    [SchoolRequestTelemetry(TrackedRequestFeature.CustomisedData)]
    public async Task<IActionResult> CustomData(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var userData = await userDataService.GetSchoolDataAsync(User.UserId(), urn);
                var customDataId = userData.CustomData;
                if (string.IsNullOrEmpty(customDataId))
                {
                    return RedirectToAction("Index", "School", new
                    {
                        urn
                    });
                }

                var userCustomData = await userDataService.GetCustomDataAsync(User.UserId(), customDataId, urn);
                if (userCustomData?.Status != "complete")
                {
                    return RedirectToAction("Index", "School", new
                    {
                        urn
                    });
                }

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedDataComparison(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();

                var viewModel = new SchoolComparisonViewModel(school, customDataId: customDataId);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying custom school comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}