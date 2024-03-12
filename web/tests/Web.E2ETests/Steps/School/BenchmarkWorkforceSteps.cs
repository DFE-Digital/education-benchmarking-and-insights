using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School benchmark workforce data")]
public class BenchmarkWorkforceSteps(PageDriver driver)
{
    private BenchmarkWorkforcePage? _workforcePage;
    private IDownload? _download;

    [Given("I am on workforce page for school with URN '(.*)'")]
    public async Task GivenIAmOnWorkforcePageForSchoolWithUrn(string urn)
    {
        var url = BenchmarkWorkforceUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _workforcePage = new BenchmarkWorkforcePage(page);
        await _workforcePage.IsDisplayed();
    }

    [When("I click on save as image for '(.*)'")]
    public async Task WhenIClickOnSaveAsImageFor(string chartName)
    {
        Assert.NotNull(_workforcePage);
        var page = await driver.Current;
        var downloadTask = page.WaitForDownloadAsync();

        await _workforcePage.ClickSaveAsImage(ChartNameFromFriendlyName(chartName));

        _download = await downloadTask;
    }

    [Then("the '(.*)' chart image is downloaded")]
    public void ThenTheChartImageIsDownloaded(string chartName)
    {
        Assert.NotNull(_workforcePage);
        ChartDownloaded(chartName);
    }

    [When("I change '(.*)' dimension to '(.*)'")]
    public async Task WhenIChangeDimensionTo(string chartName, string value)
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.SelectDimensionForChart(ChartNameFromFriendlyName(chartName), value);
    }

    [Then("the '(.*)' dimension is '(.*)'")]
    public async Task ThenTheDimensionIs(string chartName, string value)
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.IsDimensionSelectedForChart(ChartNameFromFriendlyName(chartName), value);
    }

    [Then("the following headers are displayed for '(.*)'")]
    public async Task ThenTheFollowingHeadersAreDisplayedFor(string chartName, Table table)
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.AreTableHeadersForChartDisplayed(ChartNameFromFriendlyName(chartName),
            table.Header.ToArray());
    }

    [Then("the table view is showing")]
    private async Task ThenTheTableViewIsShowing()
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.AreTablesDisplayed();
    }

    [Then("save as image buttons are hidden")]
    public async Task ThenSaveAsImageButtonsAreHidden()
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.AreSaveAsImageButtonsDisplayed(false);
    }

    [Given("table view is selected")]
    [When("I click on view as table")]
    public async Task GivenTableViewIsSelected()
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.ClickViewAsTable();
    }

    [When("I click on view as chart")]
    private async Task WhenIClickOnViewAsChart()
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.ClickViewAsChart();
    }

    [Then("chart view is showing")]
    public async Task ThenChartViewIsShowing()
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.AreChartsDisplayed();
    }

    [Then("save as image buttons are displayed")]
    public async Task ThenSaveAsImageButtonsAreDisplayed()
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.AreSaveAsImageButtonsDisplayed();
    }

    [When("I click the dimension for '(.*)'")]
    private async Task WhenIClickTheDimensionFor(string chartName)
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.ClickDimension(ChartNameFromFriendlyName(chartName));
    }

    [Then("the dimension has '(.*)' for '(.*)'")]
    public async Task ThenTheDimensionHasFor(string[] options, string chartName)
    {
        Assert.NotNull(_workforcePage);
        await _workforcePage.HasDimensionValuesForChart(ChartNameFromFriendlyName(chartName), options);
    }

    [StepArgumentTransformation]
    public static string[] TransformTDropdownOptionsToListOfStrings(string commaSeperatedList)
    {
        return commaSeperatedList.Split(',').Select(option => option.Trim()).ToArray();
    }

    private void ChartDownloaded(string chartName)
    {
        Assert.NotNull(_download);
        var downloadedFileName = ChartNameFromFriendlyName(chartName) switch
        {
            WorkforceChartNames.SchoolWorkforce => "school workforce (full time equivalent)",
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        var downloadedFilePath = _download.SuggestedFilename;
        Assert.Equal($"{downloadedFileName}.png", downloadedFilePath);
    }

    private static WorkforceChartNames ChartNameFromFriendlyName(string chartName)
    {
        return chartName switch
        {
            "school workforce" => WorkforceChartNames.SchoolWorkforce,
            "total number of teachers" => WorkforceChartNames.TotalNumberOfTeacher,
            "senior leadership" => WorkforceChartNames.SeniorLeadership,
            "teaching assistant" => WorkforceChartNames.TeachingAssistant,
            "non class room support staff" => WorkforceChartNames.NonClassRoomSupportStaff,
            "auxiliary staff" => WorkforceChartNames.AuxiliaryStaff,
            "school workforce headcount" => WorkforceChartNames.SchoolWorkforceHeadcount,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
    }

    private static string BenchmarkWorkforceUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/workforce";
}