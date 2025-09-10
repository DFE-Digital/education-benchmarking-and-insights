using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.LocalAuthority;

public enum CensusChartNames
{
    SchoolWorkforce,
    TotalNumberOfTeacher,
    SeniorLeadership,
    TeachingAssistant,
    NonClassRoomSupportStaff,
    AuxiliaryStaff,
    SchoolWorkforceHeadcount
}

public class BenchmarkCensusPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ViewAsTableRadio => page.Locator(Selectors.ModeTable);
    private ILocator ViewAsChartRadio => page.Locator(Selectors.ModeChart);
    private ILocator Charts => page.Locator(Selectors.Charts);
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
    private ILocator CopyImageSchoolWorkforce => page.Locator(Selectors.SchoolWorkforceCopyImage);

    private ILocator SaveAsImageButtons =>
        page.Locator(Selectors.Button, new PageLocatorOptions
        {
            HasTextRegex = Regexes.SaveAsImageRegex()
        });

    private ILocator CopyImageButtons =>
        page.Locator(Selectors.Button, new PageLocatorOptions
        {
            HasTextRegex = Regexes.CopyImageRegex()
        });

    private ILocator ChartBars => page.Locator(Selectors.ChartBars);
    private ILocator AdditionalDetailsPopUps => page.Locator(Selectors.AdditionalDetailsPopUps);
    private ILocator SchoolLinksInCharts => page.Locator(Selectors.SchoolNamesLinksInCharts);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();

        await ViewAsTableRadio.ShouldBeVisible().ShouldBeChecked(false);
        await ViewAsChartRadio.ShouldBeVisible().ShouldBeChecked();

        await AreSaveAsImageButtonsDisplayed();
        await AreCopyImageButtonsDisplayed(false);
        await AreChartsDisplayed();
    }

    public async Task ClickSaveAsImage(CensusChartNames chartName)
    {
        var chartToDownload = chartName switch
        {
            CensusChartNames.SchoolWorkforce => SaveImageSchoolWorkforce,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        await chartToDownload.Click();
    }

    public async Task ClickCopyImage(CensusChartNames chartName)
    {
        var chartToCopy = chartName switch
        {
            CensusChartNames.SchoolWorkforce => CopyImageSchoolWorkforce,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        await chartToCopy.Click();
    }

    public async Task SelectDimensionForChart(CensusChartNames chartName, string value)
    {
        await ChartDimensionDropdown(chartName).SelectOption(value);
    }

    public async Task IsDimensionSelectedForChart(CensusChartNames chartName, string value)
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

    public async Task AreTableHeadersForChartDisplayed(CensusChartNames chartName, string[] expected)
    {
        await page.WaitForRequestFinishedAsync();

        var table = chartName switch
        {
            CensusChartNames.TotalNumberOfTeacher => TotalTeachersTable,
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

    public async Task ClickDimension(CensusChartNames chartName)
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

    public async Task AreCopyImageButtonsDisplayed(bool isVisible = true)
    {
        var buttons = await CopyImageButtons.AllAsync();
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

    public async Task HasDimensionValuesForChart(CensusChartNames chartName, string[] expected)
    {
        const string exp = "(select) => Array.from(select.options).map(option => option.label)";
        var dropdown = ChartDimensionDropdown(chartName);
        var actual = await dropdown.EvaluateAsync<string[]>(exp);

        Assert.Equal(expected, actual);
    }

    public async Task IsSchoolDetailsPopUpVisible()
    {
        await AdditionalDetailsPopUps.First.ShouldBeVisible();
    }

    public async Task HoverOnGraphBar()
    {
        await ChartBars.First.HoverAsync();
    }

    public async Task<School.HomePage> ClickSchoolName()
    {
        await SchoolLinksInCharts.First.Click();
        return new School.HomePage(page);
    }

    public async Task AreComparisonChartsAndTablesDisplayed(bool displayed = true)
    {
        var locator = page.Locator(Selectors.ComparisonChartsAndTables);
        if (displayed)
        {
            await locator.ShouldBeVisible();
        }
        else
        {
            await locator.ShouldNotBeVisible();
        }
    }

    private ILocator ChartDimensionDropdown(CensusChartNames chartName)
    {
        var chart = chartName switch
        {
            CensusChartNames.SchoolWorkforce => SchoolWorkforceDimension,
            CensusChartNames.TotalNumberOfTeacher => TotalNumberOfTeacherDimension,
            CensusChartNames.SeniorLeadership => SeniorLeadershipDimension,
            CensusChartNames.TeachingAssistant => TeachingAssistantDimension,
            CensusChartNames.NonClassRoomSupportStaff => NonClassRoomSupportStaffDimension,
            CensusChartNames.AuxiliaryStaff => AuxiliaryStaffDimension,
            CensusChartNames.SchoolWorkforceHeadcount => SchoolWorkforceHeadcountDimension,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        return chart;
    }
}