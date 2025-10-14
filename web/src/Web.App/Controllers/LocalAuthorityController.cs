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
    private readonly RagRatingSummary[] _stub =
    [
        new()
        {
            OverallPhase = OverallPhaseTypes.Primary,
            URN = "123451",
            SchoolName = "Stub school 1",
            Red = 1,
            Amber = 3,
            Green = 0
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Primary,
            URN = "123452",
            SchoolName = "Stub school 2",
            Red = 1,
            Amber = 2,
            Green = 1
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Primary,
            URN = "123453",
            SchoolName = "Stub school 3",
            Red = 2,
            Amber = 1,
            Green = 2
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Primary,
            URN = "123454",
            SchoolName = "Stub school 4",
            Red = 2,
            Amber = 3,
            Green = 3
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Primary,
            URN = "123455",
            SchoolName = "Stub school 5",
            Red = 3,
            Amber = 2,
            Green = 4
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Primary,
            URN = "123456",
            SchoolName = "Stub school 6",
            Red = 3,
            Amber = 1,
            Green = 5
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Secondary,
            URN = "223451",
            SchoolName = "Stub school 7",
            Red = 1,
            Amber = 3,
            Green = 0
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Secondary,
            URN = "223452",
            SchoolName = "Stub school 8",
            Red = 1,
            Amber = 2,
            Green = 1
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Secondary,
            URN = "223453",
            SchoolName = "Stub school 9",
            Red = 2,
            Amber = 1,
            Green = 2
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Secondary,
            URN = "223454",
            SchoolName = "Stub school 10",
            Red = 2,
            Amber = 3,
            Green = 3
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Secondary,
            URN = "223455",
            SchoolName = "Stub school 11",
            Red = 3,
            Amber = 2,
            Green = 4
        },
        new()
        {
            OverallPhase = OverallPhaseTypes.Secondary,
            URN = "223456",
            SchoolName = "Stub school 12",
            Red = 3,
            Amber = 1,
            Green = 5
        }
    ];

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

    // todo: wire up to API properly
    private async Task<RagRatingSummary[]> RagRatings(string code) => await metricRagRatingApi
        .SummaryAsync(BuildQuery(code))
        .GetResultOrDefault<RagRatingSummary[]>() ?? _stub;

    private BacklinkInfo HomeLink(string code) => new(Url.Action("Index", new
    {
        code
    }));

    private static ApiQuery BuildQuery(string code)
    {
        var query = new ApiQuery();
        query.AddIfNotNull("code", code);
        query.AddIfNotNull("financeType", EstablishmentTypes.Maintained);
        return query;
    }
}