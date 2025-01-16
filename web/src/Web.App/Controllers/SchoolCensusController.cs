using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/census")]
public class SchoolCensusController(
    IEstablishmentApi establishmentApi,
    ICensusApi censusApi,
    IComparatorSetApi comparatorSetApi,
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

                var school = await School(urn);
                var census = await Census(urn);
                var userData = await UserData(urn);
                var defaultComparatorSet = await comparatorSetApi.GetDefaultSchoolAsync(urn).GetResultOrDefault<SchoolComparatorSet>();

                var viewModel = new SchoolCensusViewModel(school, userData.ComparatorSet, userData.CustomData, census, defaultComparatorSet);
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
                var userCustomData = await userDataService.GetCustomDataActiveAsync(User, urn);
                if (userCustomData?.Status != Pipeline.JobStatus.Complete)
                {
                    return RedirectToAction("Index", "School", new
                    {
                        urn
                    });
                }

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedDataCensus(urn);

                var school = await School(urn);
                var viewModel = new SchoolCensusViewModel(school, customDataId: userCustomData.Id);

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

    private async Task<Census?> Census(string urn) => await censusApi
        .Get(urn)
        .GetResultOrDefault<Census>();

    private async Task<(string? CustomData, string? ComparatorSet)> UserData(string urn) => await userDataService
        .GetSchoolDataAsync(User, urn);
}