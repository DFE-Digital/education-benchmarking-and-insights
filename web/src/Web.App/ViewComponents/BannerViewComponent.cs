using Microsoft.AspNetCore.Mvc;
using Web.App.Services;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class BannerViewComponent(IBannerService service) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!HttpContext.Items.TryGetValue(BannerTargets.Key, out var target)
            || target is not string parsedTarget
            || string.IsNullOrWhiteSpace(parsedTarget))
        {
            return await Task.FromResult(new EmptyContentView());
        }

        var banner = await service.GetBannerOrDefault(parsedTarget);
        var vm = new BannerViewModel(banner);
        return await Task.FromResult(View(vm));
    }
}