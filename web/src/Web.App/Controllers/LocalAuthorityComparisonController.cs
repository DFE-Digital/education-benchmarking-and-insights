using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.LocalAuthorities)]
[Route("local-authority/{code}/comparison")]
[LocalAuthorityRequestTelemetry(TrackedRequestFeature.BenchmarkCosts)]
public class LocalAuthorityComparisonController(
    IEstablishmentApi establishmentApi,
    ILogger<LocalAuthorityComparisonController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityComparison(code);

                var localAuthority = LocalAuthority(code);
                var schools = Schools(code);

                await Task.WhenAll(localAuthority, schools);

                var phases = Phases(schools.Result);
                var viewModel = new LocalAuthorityComparisonViewModel(localAuthority.Result, phases);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private static string[] Phases(School[] schools) => schools
        .GroupBy(x => x.OverallPhase)
        .OrderByDescending(x => x.Count())
        .Select(x => x.Key)
        .OfType<string>()
        .ToArray();

    private async Task<LocalAuthority> LocalAuthority(string code) => await establishmentApi
        .GetLocalAuthority(code)
        .GetResultOrThrow<LocalAuthority>();

    private async Task<School[]> Schools(string code) => await establishmentApi
        .QuerySchools(new ApiQuery().AddIfNotNull("laCode", code))
        .GetResultOrThrow<School[]>();
}