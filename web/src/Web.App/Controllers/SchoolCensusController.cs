using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
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
[Route("school/{urn}/census")]
public class SchoolCensusController(
    IEstablishmentApi establishmentApi,
    ILogger<SchoolCensusController> logger,
    IUserDataService userDataService)
    : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkWorkforce)]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCensus(urn);

                var school = School(urn);
                var userData = UserData(urn);

                await Task.WhenAll(school, userData);

                var viewModel = new SchoolCensusViewModel(school.Result, userData.Result.ComparatorSet, userData.Result.CustomData);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school census: {DisplayUrl}", Request.GetDisplayUrl());
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
                var userData = await UserData(urn);
                var customDataId = userData.CustomData;
                if (string.IsNullOrEmpty(customDataId))
                {
                    return RedirectToAction("Index", "School", new
                    {
                        urn
                    });
                }

                //TODO: Remove duplicate call for user data
                var userCustomData = await userDataService.GetCustomDataAsync(User, customDataId, urn);
                if (userCustomData?.Status != "complete")
                {
                    return RedirectToAction("Index", "School", new
                    {
                        urn
                    });
                }

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedDataCensus(urn);

                var school = await School(urn);
                var viewModel = new SchoolCensusViewModel(school, customDataId: customDataId);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying custom school census: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }


    private async Task<School> School(string urn) => await establishmentApi
        .GetSchool(urn)
        .GetResultOrThrow<School>();

    private async Task<(string? CustomData, string? ComparatorSet)> UserData(string urn) => await userDataService
        .GetSchoolDataAsync(User, urn);
}