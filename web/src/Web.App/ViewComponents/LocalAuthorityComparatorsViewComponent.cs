using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthorityComparatorsViewComponent(IEstablishmentApi establishmentApi) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string code, string[] comparators)
    {
        var localAuthorities = await establishmentApi
            .GetLocalAuthorities()
            .GetResultOrThrow<IEnumerable<LocalAuthority>>();
        return View(new LocalAuthorityComparatorsViewModel(code, localAuthorities, comparators));
    }
}