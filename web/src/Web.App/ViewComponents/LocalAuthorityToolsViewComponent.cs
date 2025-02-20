using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthorityFinanceToolsViewComponent(IFeatureManager featureManager) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string identifier, FinanceTools[] tools)
    {
        var highNeedsEnabled = await featureManager.IsEnabledAsync(FeatureFlags.HighNeeds);
        if (!highNeedsEnabled)
        {
            tools = tools.Where(t => t != FinanceTools.HighNeeds).ToArray();
        }

        return View(new FinanceToolsViewModel(identifier, tools));
    }
}