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
    ILocalAuthoritiesApi localAuthoritiesApi,
    IEducationHealthCarePlansApi educationHealthCarePlansApi,
    ILogger<LocalAuthorityHighNeedsHeadlinesViewComponent> logger) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string identifier, CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(identifier, "Actuals");
        var highNeeds = (await localAuthoritiesApi
                .GetHighNeeds(query, cancellationToken)
                .GetResultOrDefault<LocalAuthority<HighNeeds>[]>() ?? [])
            .FirstOrDefault();
        var plans = (await educationHealthCarePlansApi
                .GetEducationHealthCarePlans(query, cancellationToken)
                .GetResultOrDefault<LocalAuthorityNumberOfPlans[]>() ?? [])
            .FirstOrDefault();

        if (highNeeds?.Outturn != null && plans != null)
        {
            return View(new LocalAuthorityHighNeedsHeadlinesViewModel(highNeeds, plans));
        }

        logger.LogWarning("Local authority high needs headlines could not be displayed for {Code}", identifier);
        return View("MissingData");
    }

    private static ApiQuery BuildQuery(string code, string dimension)
    {
        var query = new ApiQuery()
            .AddIfNotNull(nameof(code), code)
            .AddIfNotNull(nameof(dimension), dimension);

        return query;
    }
}