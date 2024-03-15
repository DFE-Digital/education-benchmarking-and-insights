using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School compare your costs")]
public class CompareYourCostsSteps(PageDriver driver)
{
    private CompareYourCostsPage? _comparisonPage;
    private IDownload? _download;

    [Given("I am on compare your costs page for school with URN '(.*)'")]
    public async Task GivenIAmOnCompareYourCostsPageForSchoolWithUrn(string urn)
    {
        var url = CompareYourCostsUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _comparisonPage = new CompareYourCostsPage(page);
        await _comparisonPage.IsDisplayed();
    }

    [When("I click on save as image for '(.*)'")]
    public async Task WhenIClickOnSaveAsImageFor(string chartName)
    {
        Assert.NotNull(_comparisonPage);
        var page = await driver.Current;
        var downloadTask = page.WaitForDownloadAsync();

        await _comparisonPage.ClickSaveAsImage(ChartNameFromFriendlyName(chartName));

        _download = await downloadTask;
    }

    [Then("the '(.*)' chart image is downloaded")]
    public void ThenTheChartImageIsDownloaded(string chartName)
    {
        Assert.NotNull(_comparisonPage);
        ChartDownloaded(chartName);
    }

    [Given("the '(.*)' dimension is '(.*)'")]
    [When("I change '(.*)' dimension to '(.*)'")]
    public async Task GivenTheDimensionIs(string chartName, string value)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.SelectDimensionForChart(ChartNameFromFriendlyName(chartName), value);
    }

    [Then("the '(.*)' dimension is '(.*)'")]
    public async Task ThenTheDimensionIs(string chartName, string value)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsDimensionSelectedForChart(ChartNameFromFriendlyName(chartName), value);
    }

    [Given("table view is selected")]
    [When("I click on view as table")]
    public async Task GivenTableViewIsSelected()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.ClickViewAsTable();
    }

    [Then("the following is shown for '(.*)'")]
    public async Task ThenTheFollowingIsShownFor(string chartName, Table table)
    {
        var expected = new List<List<string>>();
        {
            var headers = table.Header.ToList();

            expected.Add(headers);
            expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));
        }

        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsTableDataForChartDisplayed(ChartNameFromFriendlyName(chartName), expected);
    }

    [Then("save as image buttons are hidden")]
    public async Task ThenSaveAsImageButtonsAreHidden()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.AreSaveAsImageButtonsDisplayed(false);
    }

    [When("I click on show all sections")]
    [Given("all sections are shown")]
    public async Task WhenIClickOnShowAllSections()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.ClickShowAllSections();
    }

    [Then("all sections on the page are expanded")]
    public async Task ThenAllSectionsOnThePageAreExpanded()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.AreSectionsExpanded();
    }

    [Then("the show all text changes to hide all sections")]
    public async Task ThenTheShowAllTextChangesToHideAllSections()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsShowHideAllSectionsText("Hide all sections");
    }

    [Then("are showing table view")]
    public async Task ThenAreShowingTableView()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.AreTablesShown();
    }

    [When("I click section link for '(.*)'")]
    public async Task WhenIClickSectionLinkFor(string chartName)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.ClickSectionLink(ChartNameFromFriendlyName(chartName));
    }

    [Then("the section '(.*)' is hidden")]
    public async Task ThenTheSectionIsHidden(string chartName)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsSectionVisible(ChartNameFromFriendlyName(chartName), false, "Show", "chart");
    }

    [When("I click on how we choose similar schools")]
    public async Task WhenIClickOnHowWeChooseSimilarSchools()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.ClickComparatorSetDetails();
    }

    [Then("the details section is expanded")]
    public async Task ThenTheDetailsSectionIsExpanded()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsDetailsSectionVisible();
    }

    private void ChartDownloaded(string chartName)
    {
        Assert.NotNull(_download);
        var downloadedFileName = ChartNameFromFriendlyName(chartName) switch
        {
            ComparisonChartNames.TotalExpenditure => "total expenditure",
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        var downloadedFilePath = _download.SuggestedFilename;
        Assert.Equal($"{downloadedFileName}.png", downloadedFilePath);
    }

    private static ComparisonChartNames ChartNameFromFriendlyName(string chartName)
    {
        return chartName switch
        {
            "total expenditure" => ComparisonChartNames.TotalExpenditure,
            "total number of teachers" => ComparisonChartNames.Premises,
            "non educational support staff" => ComparisonChartNames.NonEducationalSupportStaff,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
    }

    private static string CompareYourCostsUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/comparison";
}