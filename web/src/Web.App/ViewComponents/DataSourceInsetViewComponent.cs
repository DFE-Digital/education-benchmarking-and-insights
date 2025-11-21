using Microsoft.AspNetCore.Mvc;
using Web.App.Builders;
using Web.App.Domain.Content;
using Web.App.Services;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class DataSourceInsetViewComponent(IFinanceService financeService, IUriBuilder uriBuilder) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        string organisationType,
        string sourceType,
        bool? isPartOfTrust,
        bool showKs4Progress = false,
        string? urn = null,
        string[]? additionText = null
    )
    {
        var years = await financeService.GetYears();
        var compareSchoolPerformanceUrl = uriBuilder.CompareSchoolPerformanceUrl(urn);

        var dataSource = sourceType switch
        {
            DataSourceTypes.Spending =>
                GetSpendingDataSource(organisationType, isPartOfTrust == true, years),
            DataSourceTypes.Census =>
            [
                "Workforce data is taken from the workforce census. Pupil data is taken from the school census data set in January."
            ],
            _ => []
        };

        return View(new DataSourceInsetViewModel(dataSource, showKs4Progress, years.Ks4Progress, additionText, compareSchoolPerformanceUrl));
    }

    private static string[] GetSpendingDataSource(string organisationType, bool isPartOfTrust, FinanceYears years)
    {
        var aarDataSource = $"This {organisationType.ToLower()}'s data covers the financial year September {years.Aar - 1} to August {years.Aar} academies accounts return (AAR).";
        const string matDataSource = "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.";

        return organisationType switch
        {
            OrganisationTypes.School when isPartOfTrust =>
            [
                aarDataSource,
                matDataSource
            ],
            OrganisationTypes.School when !isPartOfTrust =>
            [
                $"This school's data covers the financial year April {years.Cfr - 1} to March {years.Cfr} consistent financial reporting return (CFR)."
            ],
            _ => []
        };
    }
}