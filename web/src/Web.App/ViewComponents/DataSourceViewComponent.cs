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
                $"This school's data covers the financial year September {years.Aar - 1} to August {years.Aar} academies accounts return (AAR).",
            OrganisationTypes.School when !isPartOfTrust =>
                $"This school's data covers the financial year April {years.Cfr - 1} to March {years.Cfr} consistent financial reporting return (CFR).",
            OrganisationTypes.Trust =>
                $"This trust's data covers the financial year September {years.Aar - 1} to August {years.Aar} academies accounts return (AAR).",
            OrganisationTypes.LocalAuthority =>
                $"This local authorities data covers the financial year April {years.Cfr - 1} to March {years.Cfr} consistent financial reporting return (CFR).",
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };


        return View(new DataSourceViewModel(dataSource, additionText));
    }
}