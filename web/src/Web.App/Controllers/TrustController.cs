using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
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
[FeatureGate(FeatureFlags.Trusts)]
[Route("trust/{companyNumber}")]
[ValidateCompanyNumber]
public class TrustController(
    ILogger<TrustController> logger,
    IEstablishmentApi establishmentApi,
    IBalanceApi balanceApi,
    IMetricRagRatingApi metricRagRatingApi,
    ICommercialResourcesService commercialResourcesService)
    : Controller
{
    [HttpGet]
    [TrustRequestTelemetry(TrackedRequestFeature.Home)]
    public async Task<IActionResult> Index(string companyNumber,
        [FromQuery(Name = "comparator-reverted")] bool? comparatorReverted)
    {
        using (logger.BeginScope(new { companyNumber }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustHome(companyNumber);

                var trust = await Trust(companyNumber);
                var balance = await TrustBalance(companyNumber);
                var ratings = await RagRatings(companyNumber);

                var viewModel = new TrustViewModel(trust, balance, ratings, comparatorReverted);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("details")]
    [TrustRequestTelemetry(TrackedRequestFeature.Details)]
    public async Task<IActionResult> Details(string companyNumber)
    {
        using (logger.BeginScope(new { companyNumber }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(companyNumber);

                var trust = await Trust(companyNumber);
                var viewModel = new TrustViewModel(trust);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("history")]
    [TrustRequestTelemetry(TrackedRequestFeature.History)]
    public async Task<IActionResult> History(string companyNumber)
    {
        using (logger.BeginScope(new { companyNumber }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(companyNumber);

                var trust = await Trust(companyNumber);

                var viewModel = new TrustViewModel(trust);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust history: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("find-ways-to-spend-less")]
    [TrustRequestTelemetry(TrackedRequestFeature.Resources)]
    public async Task<IActionResult> Resources(string companyNumber)
    {
        using (logger.BeginScope(new { companyNumber }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(companyNumber);

                var trust = await Trust(companyNumber);
                var resources = await commercialResourcesService.GetSubCategoryLinks();

                var viewModel = new TrustResourcesViewModel(trust, resources);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust resources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private static ApiQuery BuildQuery(string companyNumber)
    {
        var query = new ApiQuery();
        query.AddIfNotNull("companyNumber", companyNumber);
        return query;
    }

    private async Task<RagRating[]?> RagRatings(string companyNumber) => await metricRagRatingApi
        .GetDefaultAsync(BuildQuery(companyNumber))
        .GetResultOrDefault<RagRating[]>();

    private async Task<TrustBalance?> TrustBalance(string companyNumber) => await balanceApi
        .Trust(companyNumber)
        .GetResultOrDefault<TrustBalance>();

    private async Task<Trust> Trust(string companyNumber) => await establishmentApi
        .GetTrust(companyNumber)
        .GetResultOrThrow<Trust>();

    private BacklinkInfo HomeLink(string companyNumber) => new(Url.Action("Index", new { companyNumber }));
}