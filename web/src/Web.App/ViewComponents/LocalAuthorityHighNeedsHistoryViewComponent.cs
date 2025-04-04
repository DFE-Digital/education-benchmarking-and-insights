using Microsoft.AspNetCore.Mvc;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthorityHighNeedsHistoryViewComponent(
    ILocalAuthoritiesApi localAuthoritiesApi,
    ILogger<LocalAuthorityHighNeedsHistoryViewComponent> logger) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string identifier, string? commentary, CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(identifier, "Actuals");
        var history = await localAuthoritiesApi
            .GetHighNeedsHistory(query, cancellationToken)
            .GetResultOrDefault<HighNeedsHistory<HighNeedsYear>>();

        if (history is { Outturn.Length: > 0, Budget.Length: > 0 })
        {
            return View(new LocalAuthorityHighNeedsHistoryViewModel(identifier, history, commentary));
        }

        logger.LogWarning("Local authority high needs history could not be displayed for {Code}", identifier);
        return View("MissingData");
    }

    private static ApiQuery BuildQuery(string code, string dimension)
    {
        var query = new ApiQuery()
            .AddIfNotNull(nameof(code), code)
            .AddIfNotNull("dimension", dimension);

        return query;
    }
}