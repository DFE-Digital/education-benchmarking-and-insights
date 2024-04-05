using Microsoft.AspNetCore.Mvc;
using Web.App.Services;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class DataSourceViewComponent(IFinanceService financeService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string kind, bool isPartOfTrust, string[]? additionText)
    {
        var years = await financeService.GetYears();
        var dataSource = kind switch
        {
            OrganisationTypes.School when isPartOfTrust =>
                $"Data for this school covers the financial year September {years.Aar - 1} to August {years.Aar}. It comes from the academies accounts return (AAR) return.",
            OrganisationTypes.School when !isPartOfTrust =>
                $"Data for this school covers the financial year April {years.Cfr - 1} to {years.Cfr} 2023. It comes from the consistent financial reporting (CFR) return.",
            OrganisationTypes.Trust =>
                $"Data for this trust covers the financial year September {years.Aar - 1} to August {years.Aar}. It comes from the academies accounts return (AAR) return.",
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };


        return View(new DataSourceViewModel(dataSource, additionText));
    }
}