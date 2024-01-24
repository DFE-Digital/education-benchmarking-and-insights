using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class BenchmarkWorkforcePage
{ 
    private readonly IPage _page;
    private IDownload? _download;

    public BenchmarkWorkforcePage(PageHook page)
    {
        _page = page.Current;
    }

    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BreadCrumbs => _page.Locator(".govuk-breadcrumbs");
    private ILocator ChangeSchoolLink => _page.Locator("#change-school");
    private ILocator SaveImgCtas => _page.Locator("button", new PageLocatorOptions { HasText = "Save as image" });
    private ILocator ViewAsTableRadioBtn => _page.Locator("#mode-table");
  private ILocator ViewAsChartRadioBtn => _page.Locator("#mode-chart");
    private ILocator AllCharts => _page.Locator("canvas");
    private ILocator SchoolWorkforceDimension => _page.Locator("#school-workforce-dimension");
    private ILocator TotalNumberOfTeacherDimension => _page.Locator("#total-teachers-dimension");
    private ILocator SeniorLeadershipDimension => _page.Locator("#senior-leadership-dimension");
    private ILocator TeachingAssistantDimension => _page.Locator("#teaching-assistants-dimension");
    private ILocator NonClassRoomSupportStaffDimension => _page.Locator("#teachers-qualified-dimension");
    private ILocator AuxiliaryStaffDimension => _page.Locator("#auxiliary-staff-dimension");
    private ILocator SchoolWorkforceHeadcountDimension => _page.Locator("#auxiliary-staff-dimension");
    private ILocator TotalTeachersTable => _page.Locator("table").Nth(1);
    private ILocator AllTables => _page.Locator("table");
    private ILocator SaveImageSchoolWorkforce => _page.Locator("xpath=//*[@id='compare-workforce']/div[2]/div[2]/button");

    public async Task AssertPage()
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await PageH1Heading.ShouldBeVisible();
        await BreadCrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible();
        await ViewAsTableRadioBtn.ShouldBeVisible();
        var saveImagesCtas = await SaveImgCtas.AllAsync();
        Assert.True(saveImagesCtas.Count == 8,
            $"not all save as image buttons are showing on the page. Expected = 8 , actual = {saveImagesCtas.Count}");
        foreach (var cta in saveImagesCtas)
        {
            await cta.ShouldBeVisible();
        }

        var charts = await AllCharts.AllAsync();
        Assert.True(saveImagesCtas.Count == 8,
            $"not all charts are showing on the page. Expected = 8 , actual = {saveImagesCtas.Count}");

        foreach (var chart in charts)
        {
            await chart.ShouldBeVisible();
        }
        
    }

    public async Task AssertDropDownDimensions(ILocator dimensionsDropdown, string[] expectedOptions)
    {
        var actualOptions =
            await dimensionsDropdown.EvaluateAsync<string[]>(
                "(select) => Array.from(select.options).map(option => option.value)");
        Assert.Equal(expectedOptions, actualOptions);
    }

    public async Task ClickSaveImgBtn(string chartName)
    {
        var chartToDownload = chartName switch
        {
            "SchoolWorkforce" => SaveImageSchoolWorkforce,
            _ => throw new ArgumentException($"Unsupported chart name: {chartName}")
        };

        var downloadTask = _page.WaitForDownloadAsync();
        await chartToDownload.Click();
        _download = await downloadTask;
    }

    public void AssertImageDownload(string downloadedChart)
    {
        Assert.NotNull(_download);
        var downloadedFileName = downloadedChart switch
        {
            "SchoolWorkforce" => "school-workforce",
            _ => throw new ArgumentException($"Unsupported chart name: {downloadedChart}")
        };

        var downloadedFilePath = _download.SuggestedFilename;
        Assert.True(
            string.Equals($"{downloadedFileName}.png", downloadedFilePath, StringComparison.OrdinalIgnoreCase),
            $"Expected file name: {downloadedFileName}.png. Actual: {downloadedFilePath}"
        );
    }

    public async Task ChangeDimension(string chartName, string value)
    {
        await GetChartDimensionDropdown(chartName).SelectOption(value);
    }

    public async Task AssertDimensionValue(string chartName, string expectedValue)
    {
        await GetChartDimensionDropdown(chartName).ShouldHaveSelectedOption(expectedValue);
    }

    public ILocator GetChartDimensionDropdown(string chartName)
    {
        var chart = chartName switch
        {
            "SchoolWorkforce" => SchoolWorkforceDimension,
            "TotalNumberOfTeacher" => TotalNumberOfTeacherDimension,
            "SeniorLeadership" => SeniorLeadershipDimension,
            "TeachingAssistant" => TeachingAssistantDimension,
            "NonClassRoomSupportStaff" => NonClassRoomSupportStaffDimension,
            "AuxiliaryStaff" => AuxiliaryStaffDimension,
            "SchoolWorkforceHeadcount" => SchoolWorkforceHeadcountDimension,
            _ => throw new ArgumentException($"Unsupported chart name: {chartName}")
        };

        return chart;
    }

    public async Task ClickViewAsTable()
    {
        await ViewAsTableRadioBtn.Click();
    }

    public async Task CheckTableHeaders(string tableName, List<List<string>> expectedTableHeaders)
    {
        var table = tableName switch
        {
            "TotalNumberOfTeacher" => TotalTeachersTable,
            _ => throw new ArgumentException($"Unsupported table name: {tableName}")
        };
        await table.ShouldHaveTableHeaders(expectedTableHeaders);
    }

    public async Task AssertTableView()
    {
        var tables = await AllTables.AllAsync();
        Assert.True(tables.Count == 8,
            $"not all tables are showing on the page. Expected = 8 , actual = {tables.Count}");
        foreach (var table in tables)
        {
            await table.ShouldBeVisible();
        }
    }

    public async Task ClickViewAsChart()
    {
        await ViewAsChartRadioBtn.Click();
    }

    public async Task AssertChartView()
    {
        var charts = await AllCharts.AllAsync();
        Assert.True(charts.Count == 8,
            $"not all charts are showing on the page. Expected = 8 , actual = {charts.Count}");
        foreach (var table in charts)
        {
            await table.ShouldBeVisible();
        }
    }

    public async Task GotToPage(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}/workforce");
    }

    public async Task ClickOnDimensionDropdown(string chartName)
    {
        await GetChartDimensionDropdown(chartName).Click();
    }
}