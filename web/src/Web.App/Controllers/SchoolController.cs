using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;


[Controller]
[Route("school/{urn}")]
public class SchoolController(
    ILogger<SchoolController> logger,
    IEstablishmentApi establishmentApi,
    IBalanceApi balanceApi,
    IMetricRagRatingApi metricRagRatingApi,
    IUserDataService userDataService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn, [FromQuery(Name = "comparator-generated")] bool? comparatorGenerated)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolHome(urn);

                var school = School(urn);
                var balance = SchoolBalance(urn);
                var userData = userDataService.GetSchoolDataAsync(User.UserId(), urn);

                await Task.WhenAll(school, balance, userData);

                var ratings = string.IsNullOrEmpty(userData.Result.ComparatorSet)
                    ? await RagRatingsDefault(urn)
                    : await RagRatingsUserDefined(userData.Result.ComparatorSet);

                var viewModel = new SchoolViewModel(school.Result, balance.Result, ratings, comparatorGenerated, userData.Result.ComparatorSet, userData.Result.CustomData);
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
    [Route("history")]
    public async Task<IActionResult> History(string urn)
    {
        using (logger.BeginScope(new { urn }))
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
                logger.LogError(e, "An error displaying school history: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("details")]
    public async Task<IActionResult> Details(string urn)
    {
        using (logger.BeginScope(new { urn }))
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
    public async Task<IActionResult> Resources(string urn)
    {

        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(urn);

                var school = await School(urn);
                var ratings = await RagRatingsDefault(urn);

                var viewModel = new SchoolViewModel(school, ratings);
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
    [FeatureGate(FeatureFlags.CustomData)]
    public async Task<IActionResult> CustomData(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                var userData = await userDataService.GetSchoolDataAsync(User.UserId(), urn);
                if (string.IsNullOrEmpty(userData.CustomData))
                {
                    return RedirectToAction("Index", "School", new { urn });
                }

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomData(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolViewModel(school);

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

    private async Task<SchoolBalance?> SchoolBalance(string urn) => await balanceApi
        .School(urn)
        .GetResultOrDefault<SchoolBalance>();

    private async Task<School> School(string urn) => await establishmentApi
        .GetSchool(urn)
        .GetResultOrThrow<School>();

    private async Task<RagRating[]> RagRatingsDefault(string urn) => await metricRagRatingApi
        .GetDefaultAsync(new ApiQuery().AddIfNotNull("urns", urn))
        .GetResultOrThrow<RagRating[]>();

    private async Task<RagRating[]> RagRatingsUserDefined(string comparatorSetId) => await metricRagRatingApi
        .UserDefinedAsync(comparatorSetId)
        .GetResultOrThrow<RagRating[]>();

    private BacklinkInfo HomeLink(string urn) => new(Url.Action("Index", new { urn }));
}