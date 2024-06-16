using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.Trusts)]
[Route("trust/{companyNumber}")]
public class TrustController(
    ILogger<TrustController> logger,
    IEstablishmentApi establishmentApi,
    IBalanceApi balanceApi,
    IMetricRagRatingApi metricRagRatingApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (logger.BeginScope(new { companyNumber }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustHome(companyNumber);

                var trust = Trust(companyNumber);
                var balance = TrustBalance(companyNumber);
                var schools = TrustSchools(companyNumber);

                await Task.WhenAll(trust, balance, schools);

                var ratings = await RagRatings(schools.Result);

                var viewModel = new TrustViewModel(trust.Result, balance.Result, schools.Result, ratings);
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
    public async Task<IActionResult> Details(string companyNumber)
    {
        using (logger.BeginScope(new { companyNumber }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(companyNumber);

                var trust = await Trust(companyNumber);
                var schools = await TrustSchools(companyNumber);

                var viewModel = new TrustViewModel(trust, schools);
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
    public async Task<IActionResult> Resources(string companyNumber)
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
                logger.LogError(e, "An error displaying trust resources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private static ApiQuery BuildQuery(School[] schools)
    {
        var query = new ApiQuery();
        foreach (var school in schools)
        {
            query.AddIfNotNull("urns", school.URN);
        }

        return query;
    }

    private async Task<RagRating[]> RagRatings(School[] schools) => await metricRagRatingApi
        .GetDefaultAsync(BuildQuery(schools))
        .GetResultOrThrow<RagRating[]>();

    private async Task<TrustBalance> TrustBalance(string companyNumber) => await balanceApi
        .Trust(companyNumber)
        .GetResultOrThrow<TrustBalance>();

    private async Task<School[]> TrustSchools(string companyNumber) => await establishmentApi
        .QuerySchools(new ApiQuery().AddIfNotNull("companyNumber", companyNumber))
        .GetResultOrDefault<School[]>() ?? [];

    private async Task<Trust> Trust(string companyNumber) => await establishmentApi
        .GetTrust(companyNumber)
        .GetResultOrThrow<Trust>();

    private BacklinkInfo HomeLink(string companyNumber) => new(Url.Action("Index", new { companyNumber }));
}