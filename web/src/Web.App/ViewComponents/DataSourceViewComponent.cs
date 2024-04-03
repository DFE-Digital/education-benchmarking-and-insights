using Microsoft.AspNetCore.Mvc;
using Web.App.Services;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class DataSourceViewComponent(IFinanceService financeService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(bool isPartOfTrust, string[]? additionText)
    {
        var years = await financeService.GetYears();
        var dataSource = isPartOfTrust
            ? $"Data for this school covers the financial year September {years.Aar - 1} to August {years.Aar}. It comes from the academies accounts return (AAR) return."
            : $"Data for this school covers the financial year April {years.Cfr - 1} to {years.Cfr} 2023. It comes from the consistent financial reporting (CFR) return.";
        return View(new DataSourceViewModel(dataSource, additionText));
    }
}