using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthorityHighNeedsNationalRankingsViewComponent(IEstablishmentApi establishmentApi) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string identifier, string sort = "asc", int count = 5, CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(sort);
        var result = await establishmentApi
            .GetLocalAuthoritiesNationalRank(query, cancellationToken)
            .GetResultOrDefault<LocalAuthorityRanking>();

        return View(new LocalAuthorityHighNeedsNationalRankingsViewModel(identifier, result, count));
    }

    private static ApiQuery BuildQuery(string? sort)
    {
        var query = new ApiQuery()
            .AddIfNotNull("sort", sort);

        return query;
    }
}