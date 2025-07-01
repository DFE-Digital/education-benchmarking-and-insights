using System.Net;
using Microsoft.AspNetCore.Mvc;
using Web.App.Services;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class BannerViewComponent(IBannerService service) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (HttpContext.Response.StatusCode != (int)HttpStatusCode.OK
            || !HttpContext.Items.TryGetValue(BannerTargets.Key, out var target)
            || target is not string parsedTarget
            || string.IsNullOrWhiteSpace(parsedTarget))
        {
            return new EmptyContentView();
        }

        if (parsedTarget == BannerTargets.SchoolHomePrefix)
        {
            if (!ViewData.TryGetValue(ViewDataKeys.IsPartOfTrust, out var isPartOfTrust))
            {
                return new EmptyContentView();
            }

            parsedTarget = $"{BannerTargets.SchoolHomePrefix}{(isPartOfTrust is true ? "Academy" : "Maintained")}";
        }

        var banner = await service.GetBannerOrDefault(parsedTarget);
        var vm = new BannerViewModel(banner);
        return View(vm);
    }
}