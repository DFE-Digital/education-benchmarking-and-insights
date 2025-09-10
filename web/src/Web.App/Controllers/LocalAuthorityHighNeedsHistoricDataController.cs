using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.HighNeeds)]
[Route("local-authority/{code}/high-needs/history")]
[ValidateLaCode]
public class LocalAuthorityHighNeedsHistoricDataController(
    ILogger<LocalAuthorityHighNeedsHistoricDataController> logger,
    IEstablishmentApi establishmentApi,
    ILocalAuthoritiesApi localAuthoritiesApi)
    : Controller
{
    [HttpGet]
    [LocalAuthorityRequestTelemetry(TrackedRequestFeature.HighNeeds)]
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

                // #254461: Get the 'current' year's 'per population' HighNeeds result to determine whether to 
                // display historic data across all years. This excludes local authorities that have missing
                // population data. This will likely be refactored into Establishment API as part of future work.
                var query = BuildQuery([code], "PerHead");
                var highNeeds = await localAuthoritiesApi
                    .GetHighNeeds(query)
                    .GetResultOrDefault<LocalAuthority<HighNeeds>[]>();

                var viewModel = new LocalAuthorityHighNeedsHistoricDataViewModel(authority, highNeeds);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority high needs history: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<LocalAuthority> LocalAuthority(string code) => await establishmentApi
        .GetLocalAuthority(code)
        .GetResultOrThrow<LocalAuthority>();

    private static ApiQuery BuildQuery(string[] codes, string dimension)
    {
        var query = new ApiQuery();
        foreach (var c in codes)
        {
            query.AddIfNotNull("code", c);
        }

        query.AddIfNotNull("dimension", dimension);
        return query;
    }
}