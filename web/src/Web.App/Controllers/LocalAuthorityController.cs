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
[Route("local-authority/{code}")]
[ValidateLaCode]
public class LocalAuthorityController(
    ILogger<LocalAuthorityController> logger,
    IEstablishmentApi establishmentApi,
    IMetricRagRatingApi metricRagRatingApi,
    ICommercialResourcesService commercialResourcesService)
    : Controller
{
    [HttpGet]
    [LocalAuthorityRequestTelemetry(TrackedRequestFeature.Home)]
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityHome(code);

                var authority = await LocalAuthority(code);
                var ragRatings = await RagRatings(code);

                var viewModel = new LocalAuthorityViewModel(authority, ragRatings);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("find-ways-to-spend-less")]
    [LocalAuthorityRequestTelemetry(TrackedRequestFeature.Resources)]
    public async Task<IActionResult> Resources(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(code);

                var authority = await LocalAuthority(code);
                var resources = await commercialResourcesService.GetSubCategoryLinks();

                var viewModel = new LocalAuthorityResourcesViewModel(authority, resources);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority resources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<LocalAuthority> LocalAuthority(string code) => await establishmentApi
        .GetLocalAuthority(code)
        .GetResultOrThrow<LocalAuthority>();

    private async Task<RagRatingSummary[]> RagRatings(string code) => await metricRagRatingApi
        .SummaryAsync(BuildQuery(code))
        .GetResultOrDefault<RagRatingSummary[]>() ?? [];

    private BacklinkInfo HomeLink(string code) => new(Url.Action("Index", new
    {
        code
    }));

    private static ApiQuery BuildQuery(string code)
    {
        var query = new ApiQuery();
        query.AddIfNotNull("laCode", code);
        return query;
    }
}