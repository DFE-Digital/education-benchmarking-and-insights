using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Pages.School;

public enum WorkforceChartNames
{
    SchoolWorkforce,
    TotalNumberOfTeacher,
    SeniorLeadership,
    TeachingAssistant,
    NonClassRoomSupportStaff,
    AuxiliaryStaff,
    SchoolWorkforceHeadcount
}

public class BenchmarkWorkforcePage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator BreadCrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator ChangeSchoolLink => page.Locator(Selectors.ChangeSchoolLink);
    private ILocator ViewAsTableRadio => page.Locator(Selectors.ModeTable);
    private ILocator ViewAsChartRadio => page.Locator(Selectors.ModeChart);
    private ILocator Charts => page.Locator(Selectors.Canvas);
    private ILocator SchoolWorkforceDimension => page.Locator(Selectors.SchoolWorkforceDimension);
    private ILocator TotalNumberOfTeacherDimension => page.Locator(Selectors.TotalNumberOfTeacherDimension);
    private ILocator SeniorLeadershipDimension => page.Locator(Selectors.SeniorLeadershipDimension);
    private ILocator TeachingAssistantDimension => page.Locator(Selectors.TeachingAssistantDimension);
    private ILocator NonClassRoomSupportStaffDimension => page.Locator(Selectors.NonClassRoomSupportStaffDimension);
    private ILocator AuxiliaryStaffDimension => page.Locator(Selectors.AuxiliaryStaffDimension);
    private ILocator SchoolWorkforceHeadcountDimension => page.Locator(Selectors.SchoolWorkforceHeadcountDimension);
    private ILocator TotalTeachersTable => page.Locator(Selectors.Table).Nth(1);
    private ILocator Tables => page.Locator(Selectors.Table);
    private ILocator SaveImageSchoolWorkforce => page.Locator(Selectors.SchoolWorkforceSaveAsImage);

    private ILocator SaveAsImageButtons =>
        page.Locator(Selectors.Button, new PageLocatorOptions { HasText = "Save as image" });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await BreadCrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible();
        await ViewAsTableRadio.ShouldBeVisible().ShouldBeChecked(false);
        await ViewAsChartRadio.ShouldBeVisible().ShouldBeChecked();

        await AreSaveAsImageButtonsDisplayed();
        await AreChartsDisplayed();
    }

    public async Task ClickSaveAsImage(WorkforceChartNames chartName)
    {
        var chartToDownload = chartName switch
        {
            WorkforceChartNames.SchoolWorkforce => SaveImageSchoolWorkforce,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        await chartToDownload.Click();
    }

    public async Task SelectDimensionForChart(WorkforceChartNames chartName, string value)
    {
        await ChartDimensionDropdown(chartName).SelectOption(value);
    }

    public async Task IsDimensionSelectedForChart(WorkforceChartNames chartName, string value)
    {
        await ChartDimensionDropdown(chartName).ShouldHaveSelectedOption(value);
    }

    public async Task ClickViewAsTable()
    {
        await ViewAsTableRadio.Click();
    }

    public async Task ClickViewAsChart()
    {
        await ViewAsChartRadio.Click();
    }

    public async Task AreTableHeadersForChartDisplayed(WorkforceChartNames chartName, string[] expected)
    {
        var table = chartName switch
        {
            WorkforceChartNames.TotalNumberOfTeacher => TotalTeachersTable,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        await table.ShouldHaveTableHeaders(expected);
    }

    public async Task AreTablesDisplayed()
    {
        var tables = await Tables.AllAsync();
        Assert.Equal(8, tables.Count);
        await tables.ShouldBeVisible();
    }

    public async Task ClickDimension(WorkforceChartNames chartName)
    {
        await ChartDimensionDropdown(chartName).Click();
    }

    public async Task AreSaveAsImageButtonsDisplayed(bool isVisible = true)
    {
        var buttons = await SaveAsImageButtons.AllAsync();
        if (isVisible)
        {
            Assert.Equal(8, buttons.Count);
            await buttons.ShouldBeVisible();
        }
        else
        {
            Assert.Empty(buttons);
            await buttons.ShouldNotBeVisible();
        }
    }

    public async Task AreChartsDisplayed()
    {
        var charts = await Charts.AllAsync();
        Assert.Equal(8, charts.Count);
        await charts.ShouldBeVisible();
    }

    public async Task HasDimensionValuesForChart(WorkforceChartNames chartName, string[] expected)
    {
        const string exp = "(select) => Array.from(select.options).map(option => option.value)";
        var dropdown = ChartDimensionDropdown(chartName);
        var actual = await dropdown.EvaluateAsync<string[]>(exp);

        Assert.Equal(expected, actual);
    }

    private ILocator ChartDimensionDropdown(WorkforceChartNames chartName)
    {
        var chart = chartName switch
        {
            WorkforceChartNames.SchoolWorkforce => SchoolWorkforceDimension,
            WorkforceChartNames.TotalNumberOfTeacher => TotalNumberOfTeacherDimension,
            WorkforceChartNames.SeniorLeadership => SeniorLeadershipDimension,
            WorkforceChartNames.TeachingAssistant => TeachingAssistantDimension,
            WorkforceChartNames.NonClassRoomSupportStaff => NonClassRoomSupportStaffDimension,
            WorkforceChartNames.AuxiliaryStaff => AuxiliaryStaffDimension,
            WorkforceChartNames.SchoolWorkforceHeadcount => SchoolWorkforceHeadcountDimension,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        return chart;
    }
}