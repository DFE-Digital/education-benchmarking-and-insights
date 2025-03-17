using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthorityHighNeedsNationalRankingsViewComponent(
    IEstablishmentApi establishmentApi,
    ILogger<LocalAuthorityHighNeedsNationalRankingsViewComponent> logger) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string identifier, string ranking, string commentary, string valueLabel, string? sort = null, int count = 5, CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(ranking, sort);
        var result = await establishmentApi
            .GetLocalAuthoritiesNationalRank(query, cancellationToken)
            .GetResultOrDefault<LocalAuthorityRanking>();

        if (result?.Ranking is { Length: > 0 })
        {
            return View(new LocalAuthorityHighNeedsNationalRankingsViewModel(identifier, commentary, valueLabel, result, count));
        }

        logger.LogWarning("Local authority national rankings could not be displayed for {Code}", identifier);
        return View("MissingData");
    }

    private static ApiQuery BuildQuery(string ranking, string? sort)
    {
        var query = new ApiQuery()
            .AddIfNotNull("ranking", ranking)
            .AddIfNotNull("sort", sort);

        return query;
    }
}