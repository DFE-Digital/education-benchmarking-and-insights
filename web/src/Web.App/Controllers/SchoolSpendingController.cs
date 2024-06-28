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
[Route("school/{urn}/spending-and-costs")]
public class SchoolSpendingController(
    ILogger<SchoolController> logger,
    IEstablishmentApi establishmentApi,
    IExpenditureApi expenditureApi,
    ISchoolComparatorSetService schoolComparatorSetService,
    IMetricRagRatingApi metricRagRatingApi,
    IUserDataService userDataService)
    : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.Spending)]
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
                var userData = await userDataService.GetSchoolDataAsync(User.UserId(), urn);
                RagRating[] ratings;
                SchoolExpenditure[] pupilExpenditure;
                SchoolExpenditure[] areaExpenditure;

                if (string.IsNullOrEmpty(userData.ComparatorSet))
                {
                    ratings = await metricRagRatingApi.GetDefaultAsync(new ApiQuery().AddIfNotNull("urns", urn)).GetResultOrThrow<RagRating[]>();

                    var set = await schoolComparatorSetService.ReadComparatorSet(urn);
                    pupilExpenditure = await expenditureApi.QuerySchools(BuildQuery(set.Pupil)).GetResultOrThrow<SchoolExpenditure[]>();
                    areaExpenditure = await expenditureApi.QuerySchools(BuildQuery(set.Building)).GetResultOrThrow<SchoolExpenditure[]>();
                }
                else
                {
                    ratings = await metricRagRatingApi.UserDefinedAsync(userData.ComparatorSet).GetResultOrThrow<RagRating[]>();
                    var userSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(urn, userData.ComparatorSet);
                    var expenditures = await expenditureApi.QuerySchools(BuildQuery(userSet.Set)).GetResultOrThrow<SchoolExpenditure[]>();

                    pupilExpenditure = expenditures;
                    areaExpenditure = expenditures;
                }

                var viewModel = new SchoolSpendingViewModel(school, ratings, pupilExpenditure, areaExpenditure,
                    userData.ComparatorSet, userData.CustomData);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school spending and costs: {DisplayUrl}",
                    Request.GetDisplayUrl());
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


                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedDataSpending(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();

                var rating = await metricRagRatingApi.CustomAsync(customDataId).GetResultOrThrow<RagRating[]>();

                var set = await schoolComparatorSetService.ReadComparatorSet(urn, customDataId);

                var defaultPupilResult = await expenditureApi.QuerySchools(BuildQuery(set.Pupil.Where(x => x != urn))).GetResultOrThrow<SchoolExpenditure[]>();
                var defaultAreaResult = await expenditureApi.QuerySchools(BuildQuery(set.Building.Where(x => x != urn))).GetResultOrThrow<SchoolExpenditure[]>();

                var customPupilResult = await expenditureApi.SchoolCustom(urn, customDataId).GetResultOrThrow<SchoolExpenditure>();
                var customAreaResult = await expenditureApi.SchoolCustom(urn, customDataId).GetResultOrThrow<SchoolExpenditure>();

                var pupilExpenditure = defaultPupilResult.Append(customPupilResult);
                var areaExpenditure = defaultAreaResult.Append(customAreaResult);

                var viewModel = new SchoolSpendingViewModel(school, rating, pupilExpenditure, areaExpenditure);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying custom school spending and costs: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
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