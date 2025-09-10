using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}")]
[ValidateUrn]
public class SchoolController(
    ILogger<SchoolController> logger,
    IEstablishmentApi establishmentApi,
    IBalanceApi balanceApi,
    IMetricRagRatingApi metricRagRatingApi,
    IUserDataService userDataService,
    ICensusApi censusApi,
    ICommercialResourcesService commercialResourcesService)
    : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.Home)]
    public async Task<IActionResult> Index(
        string urn,
        [FromQuery(Name = "comparator-generated")] bool? comparatorGenerated,
        [FromQuery(Name = "comparator-reverted")] bool? comparatorReverted)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolHome(urn);

                var school = await School(urn);
                var isNonLeadFederation = !string.IsNullOrEmpty(school.FederationLeadURN) && school.FederationLeadURN != urn;

                if (isNonLeadFederation)
                {
                    var census = await Census(urn);
                    var viewModel = new NonLeadFederationSchoolViewModel(school, census);
                    return View("NonLeadFederation", viewModel);
                }
                else
                {
                    var balance = await SchoolBalance(urn);
                    var (customData, comparatorSet) = await UserData(urn);
                    var ratings = string.IsNullOrEmpty(comparatorSet)
                        ? await RagRatingsDefault(urn)
                        : await RagRatingsUserDefined(comparatorSet);

                    var viewModel = new SchoolViewModel(school, balance, ratings, comparatorGenerated, comparatorReverted, comparatorSet, customData);
                    return View(viewModel);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("history")]
    [SchoolRequestTelemetry(TrackedRequestFeature.History)]
    public async Task<IActionResult> History(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(urn);

                var school = await School(urn);
                var viewModel = new SchoolHistoryViewModel(school);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school history: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("details")]
    [SchoolRequestTelemetry(TrackedRequestFeature.Details)]
    public async Task<IActionResult> Details(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(urn);

                var school = await School(urn);
                var viewModel = new SchoolViewModel(school);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("find-ways-to-spend-less")]
    [SchoolRequestTelemetry(TrackedRequestFeature.Resources)]
    public async Task<IActionResult> Resources(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(urn);

                var school = School(urn);
                var ratings = RagRatingsDefault(urn);
                var catResources = commercialResourcesService.GetCategoryLinks();
                var subCatResources = commercialResourcesService.GetSubCategoryLinks();

                await Task.WhenAll(school, ratings, catResources);

                var parameters = new SchoolResourcesViewModelParams
                {
                    School = school.Result,
                    Ratings = ratings.Result,
                    CategoryResources = catResources.Result,
                    SubCategoryResources = subCatResources.Result
                };

                var viewModel = new SchoolResourcesViewModel(parameters);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school resources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("customised-data")]
    [SchoolAuthorization]
    [SchoolRequestTelemetry(TrackedRequestFeature.CustomisedData)]
    public async Task<IActionResult> CustomData(string urn, [FromQuery(Name = "custom-data-generated")] bool? customDataGenerated)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var userCustomData = await userDataService.GetCustomDataActiveAsync(User, urn);
                if (userCustomData?.Status == Pipeline.JobStatus.Pending)
                {
                    return RedirectToAction("Submitted", "SchoolCustomDataChange", new
                    {
                        urn
                    });
                }

                if (userCustomData?.Status != Pipeline.JobStatus.Complete)
                {
                    return RedirectToAction("Index", "School", new
                    {
                        urn
                    });
                }

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedData(urn);

                var school = await School(urn);
                var viewModel = new SchoolViewModel(school, customDataGenerated);

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

    private async Task<(string? CustomData, string? ComparatorSet)> UserData(string urn) => await userDataService
        .GetSchoolDataAsync(User, urn);

    private async Task<SchoolBalance?> SchoolBalance(string urn) => await balanceApi
        .School(urn)
        .GetResultOrDefault<SchoolBalance>();

    private async Task<School> School(string urn) => await establishmentApi
        .GetSchool(urn)
        .GetResultOrThrow<School>();

    private async Task<Census> Census(string urn) => await censusApi
        .Get(urn)
        .GetResultOrThrow<Census>();

    private async Task<RagRating[]> RagRatingsDefault(string urn) => await metricRagRatingApi
        .GetDefaultAsync(new ApiQuery().AddIfNotNull("urns", urn))
        .GetResultOrThrow<RagRating[]>();

    private async Task<RagRating[]> RagRatingsUserDefined(string comparatorSetId) => await metricRagRatingApi
        .UserDefinedAsync(comparatorSetId)
        .GetResultOrThrow<RagRating[]>();

    private BacklinkInfo HomeLink(string urn) => new(Url.Action("Index", new
    {
        urn
    }));
}