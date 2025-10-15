using Microsoft.AspNetCore.Mvc;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthoritySchoolFinancialViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string code) => await Task.FromResult(View(new LocalAuthoritySchoolFinancialViewModel(code)));
}