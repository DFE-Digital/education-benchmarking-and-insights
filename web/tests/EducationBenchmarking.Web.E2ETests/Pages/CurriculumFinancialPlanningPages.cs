using EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class CurriculumFinancialPlanningPages(
    StartPage start,
    HelpPage help,
    SelectYearPage selectYear,
    PrePopulatedDataPage prePopulatedData)
{
    public StartPage Start { get; } = start;
    public HelpPage Help { get; } = help;
    public SelectYearPage SelectYear { get; } = selectYear;
    public PrePopulatedDataPage PrePopulatedData { get; } = prePopulatedData;
}