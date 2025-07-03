using Microsoft.AspNetCore.Mvc;
using Web.App.Services;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class BannerViewComponent(IBannerService service) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string target, string? columnClass = null)
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            return new EmptyContentView();
        }

        var banner = await service.GetBannerOrDefault(target);
        var vm = new BannerViewModel(banner, columnClass);
        return View(vm);
    }
}