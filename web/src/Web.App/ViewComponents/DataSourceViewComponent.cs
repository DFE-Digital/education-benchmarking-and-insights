using Microsoft.AspNetCore.Mvc;
using Web.App.Services;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class DataSourceViewComponent(IFinanceService financeService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        string organisationType,
        string sourceType,
        bool? isPartOfTrust,
        string[]? additionText,
        string wrapperClassName = "govuk-grid-row",
        string className = "govuk-grid-column-full")
    {
        var dataSource = sourceType switch
        {
            DataSourceTypes.Spending =>
                await GetSpendingDataSource(organisationType, isPartOfTrust == true),
            DataSourceTypes.Census =>
            [
                "Workforce data is taken from the workforce census.",
                "Pupil data is taken from the school census data set in January."
            ],
            DataSourceTypes.HighNeeds =>
            [
                "Pellentesque vel mattis enim, vel cursus ante."
            ],
            _ => throw new ArgumentOutOfRangeException(nameof(sourceType))
        };

        return View(new DataSourceViewModel(dataSource, additionText, wrapperClassName, className));
    }

    private async Task<string[]> GetSpendingDataSource(string organisationType, bool isPartOfTrust)
    {
        var years = await financeService.GetYears();

        return organisationType switch
        {
            OrganisationTypes.School when isPartOfTrust =>
            [
                $"This school's data covers the financial year September {years.Aar - 1} to August {years.Aar} academies accounts return (AAR)."
            ],
            OrganisationTypes.School when !isPartOfTrust =>
            [
                $"This school's data covers the financial year April {years.Cfr - 1} to March {years.Cfr} consistent financial reporting return (CFR)."
            ],
            OrganisationTypes.Trust =>
            [
                $"This trust's data covers the financial year September {years.Aar - 1} to August {years.Aar} academies accounts return (AAR)."
            ],
            OrganisationTypes.LocalAuthority =>
            [
                $"This data covers the financial year April {years.Cfr - 1} to March {years.Cfr} consistent financial reporting return (CFR)."
            ],
            _ => throw new ArgumentOutOfRangeException(nameof(organisationType))
        };
    }
}