using EducationBenchmarking.Web.E2ETests.Helpers;
using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class WorkforcePage
{
    private readonly IPage _page;
    private IDownload? _download;

    public WorkforcePage(IPage page)
    {
        _page = page;
    }

    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BreadCrumbs => _page.Locator(".govuk-breadcrumbs");
    private ILocator ChangeSchoolLink => _page.Locator("#change-school");

    private ILocator SaveImgBtns => _page.Locator("button", new PageLocatorOptions()
    {
        HasText = "Save as image"
    });

    private ILocator ViewAsTable => _page.Locator("button", new PageLocatorOptions()
    {
        HasText = "View as table"
    });

    private ILocator AllCharts => _page.Locator("canvas");
    private ILocator SchoolWorkforceDimension => _page.Locator("#school-workforce-dimension");
    private ILocator TotalNumberOfTeacherDimension => _page.Locator("#total-teachers-dimension");
    private ILocator SeniorLeadershipDimension => _page.Locator("#senior-leadership-dimension");
    private ILocator TeachingAssistantDimension => _page.Locator("#teaching-assistants-dimension");
    private ILocator NonClassRoomSupportStaffDimension => _page.Locator("#teachers-qualified-dimension");
    private ILocator AuxiliaryStaffDimension => _page.Locator("#auxiliary-staff-dimension");
    private ILocator SchoolWorkforceHeadcountDimension => _page.Locator("#auxiliary-staff-dimension");


    private ILocator SaveImageSchoolWorkforce =>
        _page.Locator("xpath=//*[@id='compare-workforce']/div[2]/div[2]/button");

    public async Task AssertPage()
    {
        await PageH1Heading.ShouldBeVisible();
        await BreadCrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible();
        await ViewAsTable.ShouldBeVisible();
        var saveImagesCtas = await SaveImgBtns.AllAsync();
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

        string[] schoolWorkForceExpectedOptions = { "total", "headcount per FTE", "pupils per staff role" };
        await AssertDropDownDimensions(SchoolWorkforceDimension, schoolWorkForceExpectedOptions);
        string[] dimensionsDropDownOptions =
            { "total", "headcount per FTE", "percentage of workforce", "pupils per staff role" };
        await AssertDropDownDimensions(TotalNumberOfTeacherDimension, dimensionsDropDownOptions);
        await AssertDropDownDimensions(SeniorLeadershipDimension, dimensionsDropDownOptions);
        await AssertDropDownDimensions(TeachingAssistantDimension, dimensionsDropDownOptions);
        await AssertDropDownDimensions(NonClassRoomSupportStaffDimension, dimensionsDropDownOptions);
        await AssertDropDownDimensions(AuxiliaryStaffDimension, dimensionsDropDownOptions);
        string[] schoolWorkforceHeadcountDimensionOptions =
            { "total", "percentage of workforce", "pupils per staff role" };
        await AssertDropDownDimensions(SchoolWorkforceHeadcountDimension, schoolWorkforceHeadcountDimensionOptions);

        string defaultOption = "pupils per staff role";
        await SchoolWorkforceDimension.ShouldHaveSelectedOption(defaultOption);
        await TotalNumberOfTeacherDimension.ShouldHaveSelectedOption(defaultOption);
        await SeniorLeadershipDimension.ShouldHaveSelectedOption(defaultOption);
        await TeachingAssistantDimension.ShouldHaveSelectedOption(defaultOption);
        await NonClassRoomSupportStaffDimension.ShouldHaveSelectedOption(defaultOption);
        await AuxiliaryStaffDimension.ShouldHaveSelectedOption(defaultOption);
        await SchoolWorkforceHeadcountDimension.ShouldHaveSelectedOption(defaultOption);
    }

    private async Task AssertDropDownDimensions(ILocator dimensionsDropdown, string[] expectedOptions)
    {
        string[] actualOptions =
            await dimensionsDropdown.EvaluateAsync<string[]>(
                "(select) => Array.from(select.options).map(option => option.value)");
        Assert.Equal(expectedOptions, actualOptions);
    }

    public async Task ClickSaveImgBtn(string chartName)
    {
        ILocator chartToDownload;
        switch (chartName)
        {
            case "school workforce":
                chartToDownload = SaveImageSchoolWorkforce;
                break;
            default:
                throw new ArgumentException($"Unsupported chart name: {chartName}");
        }

        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var downloadTask = _page.WaitForDownloadAsync();
        await chartToDownload.Click();
        _download = await downloadTask;
    }

    public void AssertImageDownload(string downloadedChart)
    {
        string downloadedFileName;
        if (_download == null)
        {
            throw new ArgumentNullException(nameof(_download));
        }

        switch (downloadedChart)
        {
            case "school workforce":
                downloadedFileName = "school-workforce";
                break;
            default:
                throw new ArgumentException($"Unsupported chart name: {downloadedChart}");
        }

        var downloadedFilePath = _download.SuggestedFilename;
        Assert.True(
            string.Equals($"{downloadedFileName}.png", downloadedFilePath, StringComparison.OrdinalIgnoreCase),
            $"Expected file name: {downloadedFileName}.png. Actual: {downloadedFilePath}"
        );
    }

    public async Task ChangeDimension(string chartName, string value)
    {
        ILocator chart;
        switch (chartName)
        {
            case "school workforce":
                chart = SchoolWorkforceDimension;
                break;
            default:
                throw new ArgumentException($"Unsupported chart name: {chartName}");
        }

        await chart.SelectOption(value);
    }

    public async Task AssertDimensionValue(string chartName, string expectedValue)
    {
        ILocator chart;
        switch (chartName)
        {
            case "school workforce":
                chart = SchoolWorkforceDimension;
                break;
            default:
                throw new ArgumentException($"Unsupported chart name: {chartName}");
        }

        await chart.ShouldHaveSelectedOption(expectedValue);
    }
}