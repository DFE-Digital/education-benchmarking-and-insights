using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School benchmark pupil and workforce data")]
public class BenchmarkCensusSteps(PageDriver driver)
{
    private BenchmarkCensusPage? _censusPage;
    private IDownload? _download;
    private HomePage? _schoolHomePage;

    [Given("I am on census page for school with URN '(.*)'")]
    public async Task GivenIAmOnCensusPageForSchoolWithUrn(string urn)
    {
        var url = BenchmarkCensusUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        await driver.WaitForPendingRequests(500);

        _censusPage = new BenchmarkCensusPage(page);
        await _censusPage.IsDisplayed();
    }

    [When("I click on save as image for '(.*)'")]
    public async Task WhenIClickOnSaveAsImageFor(string chartName)
    {
        Assert.NotNull(_censusPage);
        var page = await driver.Current;
        var downloadTask = page.WaitForDownloadAsync();

        await _censusPage.ClickSaveAsImage(ChartNameFromFriendlyName(chartName));

        _download = await downloadTask;
    }

    [Then("the '(.*)' chart image is downloaded")]
    public void ThenTheChartImageIsDownloaded(string chartName)
    {
        Assert.NotNull(_censusPage);
        ChartDownloaded(chartName);
    }

    [When("I change '(.*)' dimension to '(.*)'")]
    public async Task WhenIChangeDimensionTo(string chartName, string value)
    {
        Assert.NotNull(_censusPage);
        await _censusPage.SelectDimensionForChart(ChartNameFromFriendlyName(chartName), value);
    }

    [Then("the '(.*)' dimension is '(.*)'")]
    public async Task ThenTheDimensionIs(string chartName, string value)
    {
        Assert.NotNull(_censusPage);
        await _censusPage.IsDimensionSelectedForChart(ChartNameFromFriendlyName(chartName), value);
    }

    [Then("the following headers are displayed for '(.*)'")]
    public async Task ThenTheFollowingHeadersAreDisplayedFor(string chartName, Table table)
    {
        Assert.NotNull(_censusPage);
        await _censusPage.AreTableHeadersForChartDisplayed(ChartNameFromFriendlyName(chartName),
            table.Header.ToArray(), !table.Header.Contains("Pupils per staff role"));
    }

    [Then("the table view is showing")]
    private async Task ThenTheTableViewIsShowing()
    {
        Assert.NotNull(_censusPage);
        await _censusPage.AreTablesDisplayed();
    }

    [Then("save as image buttons are hidden")]
    public async Task ThenSaveAsImageButtonsAreHidden()
    {
        Assert.NotNull(_censusPage);
        await _censusPage.AreSaveAsImageButtonsDisplayed(false);
    }

    [Given("table view is selected")]
    [When("I click on view as table")]
    public async Task GivenTableViewIsSelected()
    {
        Assert.NotNull(_censusPage);
        await _censusPage.ClickViewAsTable();
    }

    [When("I click on view as chart")]
    private async Task WhenIClickOnViewAsChart()
    {
        Assert.NotNull(_censusPage);
        await _censusPage.ClickViewAsChart();
    }

    [Then("chart view is showing")]
    public async Task ThenChartViewIsShowing()
    {
        Assert.NotNull(_censusPage);
        await _censusPage.AreChartsDisplayed();
    }

    [Then("save as image buttons are displayed")]
    public async Task ThenSaveAsImageButtonsAreDisplayed()
    {
        Assert.NotNull(_censusPage);
        await _censusPage.AreSaveAsImageButtonsDisplayed();
    }

    [When("I click the dimension for '(.*)'")]
    private async Task WhenIClickTheDimensionFor(string chartName)
    {
        Assert.NotNull(_censusPage);
        await _censusPage.ClickDimension(ChartNameFromFriendlyName(chartName));
    }

    [Then("the dimension has '(.*)' for '(.*)'")]
    public async Task ThenTheDimensionHasFor(string[] options, string chartName)
    {
        Assert.NotNull(_censusPage);
        await _censusPage.HasDimensionValuesForChart(ChartNameFromFriendlyName(chartName), options);
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
            CensusChartNames.SchoolWorkforce => "school workforce (full time equivalent)",
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        var downloadedFilePath = _download.SuggestedFilename;
        Assert.Equal($"{downloadedFileName}.png", downloadedFilePath);
    }

    private static CensusChartNames ChartNameFromFriendlyName(string chartName)
    {
        return chartName switch
        {
            "school workforce" => CensusChartNames.SchoolWorkforce,
            "total number of teachers" => CensusChartNames.TotalNumberOfTeacher,
            "senior leadership" => CensusChartNames.SeniorLeadership,
            "teaching assistant" => CensusChartNames.TeachingAssistant,
            "non class room support staff" => CensusChartNames.NonClassRoomSupportStaff,
            "auxiliary staff" => CensusChartNames.AuxiliaryStaff,
            "school workforce headcount" => CensusChartNames.SchoolWorkforceHeadcount,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
    }


    [Then(@"additional information is displayed")]
    public async Task ThenAdditionalInformationIsDisplayed()
    {
        Assert.NotNull(_censusPage);
        await _censusPage.IsSchoolDetailsPopUpVisible();
    }

    [When("I hover over a chart bar")]
    public async Task WhenIHoverOverChartBar()
    {
        Assert.NotNull(_censusPage);
        await _censusPage.HoverOnGraphBar();

    }

    [When("I select the school name on the chart")]
    public async Task WhenISelectTheSchoolNameOnTheChart()
    {
        Assert.NotNull(_censusPage);
        _schoolHomePage = await _censusPage.ClickSchoolName();
    }

    [Then("I am navigated to selected school home page")]
    public async Task ThenIAmNavigatedToSelectedSchoolHomePage()
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.IsDisplayed();
    }

    private static string BenchmarkCensusUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/census";


}