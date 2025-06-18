using Microsoft.AspNetCore.Mvc;
using Web.App.Domain.LocalAuthorities;
using Web.App.Domain.NonFinancial;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Apis.NonFinancial;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthorityHighNeedsHeadlinesViewComponent(
    IEducationHealthCarePlansApi educationHealthCarePlansApi,
    ILocalAuthoritiesApi localAuthoritiesApi,
    ILogger<LocalAuthorityHighNeedsHeadlinesViewComponent> logger) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string identifier, string commentary, CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(identifier, "Actuals");
        var plans = (await educationHealthCarePlansApi
                .GetEducationHealthCarePlans(query, cancellationToken)
                .GetResultOrDefault<LocalAuthorityNumberOfPlans[]>() ?? [])
            .FirstOrDefault();

        var highNeeds = (await localAuthoritiesApi
            .GetHighNeeds(query, cancellationToken)
            .GetResultOrDefault<LocalAuthority<HighNeeds>[]>() ?? [])
            .FirstOrDefault();

        if (plans != null && highNeeds != null)
        {
            return View(new LocalAuthorityHighNeedsHeadlinesViewModel(plans, highNeeds, commentary));
        }

        logger.LogWarning("Local authority high needs key information could not be displayed for {Code}", identifier);
        return View("MissingData", "Key information");
    }

    private static ApiQuery BuildQuery(string code, string dimension)
    {
        var query = new ApiQuery()
            .AddIfNotNull(nameof(code), code)
            .AddIfNotNull(nameof(dimension), dimension);

        return query;
    }
}