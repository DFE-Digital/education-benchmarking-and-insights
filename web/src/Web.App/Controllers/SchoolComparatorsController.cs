using Microsoft.AspNetCore.Authorization;
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
[Route("school/{urn}/comparators")]
[SchoolRequestTelemetry(TrackedRequestFeature.Comparators)]
public class SchoolComparatorsController(
    ILogger<SchoolComparatorsController> logger,
    IEstablishmentApi establishmentApi,
    IComparatorSetApi comparatorSetApi,
    ISchoolInsightApi schoolInsightApi,
    IUserDataService userDataService,
    ISchoolComparatorSetService schoolComparatorSetService) : Controller
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
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var set = await comparatorSetApi.GetDefaultSchoolAsync(urn).GetResultOrThrow<SchoolComparatorSet>();
                var pupil = await GetSchoolCharacteristics<SchoolCharacteristicPupil>(set.Pupil);
                var building = await GetSchoolCharacteristics<SchoolCharacteristicBuilding>(set.Building);
                var userData = await userDataService.GetSchoolDataAsync(User, urn);

                var viewModel = new SchoolComparatorsViewModel(school, pupil: pupil, building: building, hasCustomData: !string.IsNullOrEmpty(userData.CustomData));
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("user-defined")]
    [Authorize]
    [FeatureGate(FeatureFlags.UserDefinedComparators)]
    public async Task<IActionResult> UserDefined(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var userData = await userDataService.GetSchoolDataAsync(User, urn);
                SchoolCharacteristicUserDefined[]? schools = null;

                if (userData.ComparatorSet != null)
                {
                    var userDefinedSet = await comparatorSetApi.GetUserDefinedSchoolAsync(urn, userData.ComparatorSet)
                        .GetResultOrDefault<UserDefinedSchoolComparatorSet>();
                    if (userDefinedSet != null)
                    {
                        schools = await GetSchoolCharacteristics<SchoolCharacteristicUserDefined>(userDefinedSet.Set);
                    }
                }

                var viewModel = new SchoolComparatorsViewModel(school, userDefined: schools, userDefinedSetId: userData.ComparatorSet);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("revert")]
    [Authorize]
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
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolComparatorsViewModel(school);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error reverting school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("revert")]
    [Authorize]
    [FeatureGate(FeatureFlags.UserDefinedComparators)]
    public async Task<IActionResult> RevertSet(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

                var userData = await userDataService.GetSchoolDataAsync(User, urn);
                if (userData.ComparatorSet != null)
                {
                    await comparatorSetApi.RemoveUserDefinedSchoolAsync(urn, userData.ComparatorSet).EnsureSuccess();
                    schoolComparatorSetService.ClearUserDefinedComparatorSet(urn, userData.ComparatorSet);
                }

                schoolComparatorSetService.ClearUserDefinedComparatorSet(urn);
                schoolComparatorSetService.ClearUserDefinedCharacteristic(urn);
                return RedirectToAction("Index", "School", new
                {
                    urn
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error reverting school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<T[]?> GetSchoolCharacteristics<T>(IEnumerable<string> set)
    {
        var query = new ApiQuery();
        var schools = set as string[] ?? set.ToArray();
        if (schools.Length != 0)
        {
            foreach (var urn in schools)
            {
                query.AddIfNotNull("urns", urn);
            }
        }

        return await schoolInsightApi.GetCharacteristicsAsync(query).GetResultOrDefault<T[]>();
    }
}