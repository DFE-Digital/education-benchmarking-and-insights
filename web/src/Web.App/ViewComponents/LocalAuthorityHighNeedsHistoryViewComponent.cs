using Microsoft.AspNetCore.Mvc;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthorityHighNeedsHistoryViewComponent(ILocalAuthoritiesApi localAuthoritiesApi) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string identifier, CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(identifier);
        var history = await localAuthoritiesApi
            .GetHighNeedsHistory(query, cancellationToken)
            .GetResultOrDefault<HighNeedsHistory<LocalAuthorityHighNeedsYear>>();

        return View(new LocalAuthorityHighNeedsHistoryViewModel(identifier, history));
    }

    private static ApiQuery BuildQuery(params string[] code)
    {
        var query = new ApiQuery();
        foreach (var c in code)
        {
            query.AddIfNotNull(nameof(code), c);
        }

        return query;
    }
}