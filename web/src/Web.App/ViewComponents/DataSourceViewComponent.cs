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
        bool? isMat,
        string[]? additionText,
        string? pageTitle,
        string wrapperClassName = "govuk-grid-row",
        string className = "govuk-grid-column-full"
        )
    {
        var dataSource = sourceType switch
        {
            DataSourceTypes.Spending =>
                await GetSpendingDataSource(organisationType, isPartOfTrust == true, isMat == true),
            DataSourceTypes.Census =>
            [
                "Workforce data is taken from the workforce census.",
                "Pupil data is taken from the school census data set in January."
            ],
            DataSourceTypes.HighNeeds => await GetHighNeedsDataSource(pageTitle),
            _ => throw new ArgumentOutOfRangeException(nameof(sourceType))
        };

        return View(new DataSourceViewModel(dataSource, additionText, wrapperClassName, className));
    }

    private async Task<string[]> GetSpendingDataSource(string organisationType, bool isPartOfTrust, bool isMat)
    {
        var years = await financeService.GetYears();
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
            OrganisationTypes.Trust when isMat =>
            [
                aarDataSource,
                matDataSource
            ],
            OrganisationTypes.Trust when !isMat =>
            [
                aarDataSource
            ],
            OrganisationTypes.LocalAuthority =>
            [
                $"This data covers the financial year April {years.Cfr - 1} to March {years.Cfr} consistent financial reporting return (CFR)."
            ],
            _ => throw new ArgumentOutOfRangeException(nameof(organisationType))
        };
    }

    private async Task<string[]> GetHighNeedsDataSource(string? pageTitle)
    {
        var years = await financeService.GetYears();
        return pageTitle switch
        {
            PageTitles.LocalAuthorityHighNeedsHistoricData or PageTitles.LocalAuthorityHighNeedsBenchmarking =>
            [
                $"This data includes section 251 data (s251) for period {years.S251 - 1}-{years.S251} and special educational needs (SEN) data for January {years.S251}. It also includes budgeted and outturn spend per head, using aggregated s251 categories.",
                "The outturn does not include place funding for pupils with special educational needs taught in academies."
            ],
            PageTitles.LocalAuthorityHighNeeds =>
            [
                $"This data includes section 251 data (s251) for period {years.S251 - 1}-{years.S251} and special educational needs (SEN) data for January {years.S251}. The outturn does not include place funding for pupils with special educational needs taught in academies."
            ],
            PageTitles.LocalAuthorityHighNeedsNationalRankings =>
            [
                $"This data includes section 251 data (s251) for period {years.S251 - 1}-{years.S251}. The outturn does not include place funding for pupils with special educational needs taught in academies."
            ],
            _ => throw new ArgumentOutOfRangeException(nameof(pageTitle))

        };
    }
}