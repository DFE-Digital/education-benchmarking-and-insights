﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/spending-and-costs")]
[ValidateUrn]
public class SchoolSpendingController(
    ILogger<SchoolController> logger,
    IEstablishmentApi establishmentApi,
    IExpenditureApi expenditureApi,
    ISchoolComparatorSetService schoolComparatorSetService,
    IMetricRagRatingApi metricRagRatingApi,
    IUserDataService userDataService,
    ICommercialResourcesService commercialResourcesService,
    IFeatureManager featureManager)
    : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.SpendingPriorities)]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolSpending(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var userData = await userDataService.GetSchoolDataAsync(User, urn);
                RagRating[] ratings;
                SchoolExpenditure[] pupilExpenditure = [];
                SchoolExpenditure[] areaExpenditure = [];

                if (string.IsNullOrEmpty(userData.ComparatorSet))
                {
                    ratings = await metricRagRatingApi.GetDefaultAsync(new ApiQuery().AddIfNotNull("urns", urn)).GetResultOrThrow<RagRating[]>();

                    var set = await schoolComparatorSetService.ReadComparatorSet(urn);
                    pupilExpenditure = set is { Pupil.Length: > 0 }
                        ? await expenditureApi.QuerySchools(BuildQuery(set.Pupil)).GetResultOrThrow<SchoolExpenditure[]>()
                        : [];
                    areaExpenditure = set is { Building.Length: > 0 }
                        ? await expenditureApi.QuerySchools(BuildQuery(set.Building)).GetResultOrThrow<SchoolExpenditure[]>()
                        : [];
                }
                else
                {
                    ratings = await metricRagRatingApi.UserDefinedAsync(userData.ComparatorSet).GetResultOrThrow<RagRating[]>();
                    var userSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(urn, userData.ComparatorSet);
                    if (userSet is { Set.Length: > 0 })
                    {
                        var expenditures = await expenditureApi.QuerySchools(BuildQuery(userSet.Set)).GetResultOrThrow<SchoolExpenditure[]>();

                        pupilExpenditure = expenditures;
                        areaExpenditure = expenditures;
                    }
                }

                var resources = await commercialResourcesService.GetCategoryLinks();
                var viewModel = new SchoolSpendingViewModel(school, ratings, pupilExpenditure, areaExpenditure, resources, userData.ComparatorSet, userData.CustomData);
                var viewName = await GetViewName(nameof(Index));

                return View(viewName, viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school spending and costs: {DisplayUrl}", Request.GetDisplayUrl());
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
                    return RedirectToAction("Index", "School", new { urn });
                }

                var customDataId = userCustomData.Id!;
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedDataSpending(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();

                var rating = await metricRagRatingApi.CustomAsync(customDataId).GetResultOrThrow<RagRating[]>();

                var set = await schoolComparatorSetService.ReadComparatorSet(urn, customDataId);

                var defaultPupilResult = set is { Pupil.Length: > 0 }
                    ? await expenditureApi.QuerySchools(BuildQuery(set.Pupil.Where(x => x != urn))).GetResultOrThrow<SchoolExpenditure[]>()
                    : [];

                var defaultAreaResult = set is { Building.Length: > 0 }
                    ? await expenditureApi.QuerySchools(BuildQuery(set.Building.Where(x => x != urn))).GetResultOrThrow<SchoolExpenditure[]>()
                    : [];

                var customPupilResult = await expenditureApi.SchoolCustom(urn, customDataId, new ApiQuery().AddIfNotNull("dimension", "PerUnit")).GetResultOrThrow<SchoolExpenditure>();
                var customAreaResult = await expenditureApi.SchoolCustom(urn, customDataId, new ApiQuery().AddIfNotNull("dimension", "PerUnit")).GetResultOrThrow<SchoolExpenditure>();

                var pupilExpenditure = defaultPupilResult.Append(customPupilResult);
                var areaExpenditure = defaultAreaResult.Append(customAreaResult);

                var resources = await commercialResourcesService.GetCategoryLinks();
                var viewModel = new SchoolSpendingViewModel(school, rating, pupilExpenditure, areaExpenditure, resources);
                var viewName = await GetViewName(nameof(CustomData));

                return View(viewName, viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying custom school spending and costs: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<string> GetViewName(string baseViewName)
    {
        var useSsr = await featureManager.IsEnabledAsync(FeatureFlags.SchoolSpendingPrioritiesSsrCharts);
        return useSsr ? $"{baseViewName}Ssr" : baseViewName;
    }

    private static ApiQuery BuildQuery(IEnumerable<string> urns)
    {
        var query = new ApiQuery().AddIfNotNull("dimension", "PerUnit");
        foreach (var urn in urns)
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }
}