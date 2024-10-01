using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Xunit;
using HomePage = Web.E2ETests.Pages.School.HomePage;
namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust compare your costs")]
public class CompareYourCostsSteps(PageDriver driver)
{
    private CompareYourCostsPage? _comparisonPage;
    private IDownload? _download;
    private HomePage? _schoolHomePage;

    [Given("I am on compare your costs page for trust with company number '(.*)'")]
    public async Task GivenIAmOnCompareYourCostsPageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = CompareYourCostsUrl(companyNumber);
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

    [Given("Section '(.*)' is visible")]
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

    [Then(@"additional information is displayed")]
    public async Task ThenAdditionalInformationIsDisplayed()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsTrustDetailsPopUpVisible();
    }

    [When("I hover over a chart bar")]
    public async Task WhenIHoverOverAChartBar()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.HoverOnGraphBar();
    }

    [When("I hover over the nth chart bar (.*)")]
    public async Task WhenIHoverOverTheNthChartBar(int nth)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.HoverOnGraphBar(nth);
    }

    [When("I select the school name on the chart")]
    public async Task WhenISelectTheSchoolNameOnTheChart()
    {
        Assert.NotNull(_comparisonPage);
        _schoolHomePage = await _comparisonPage.ClickSchoolName();
    }

    [When("I tab to the school name on the chart")]
    public async Task WhenITabToTheSchoolNameOnTheChart()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.TabToTrustName();
    }

    [When("I press the Enter key when focused on the school name")]
    public async Task WhenIPressTheEnterKeyWhenFocusedOnTheSchoolName()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.AssertSchoolNameFocused();
        _schoolHomePage = await _comparisonPage.PressEnterKey();
    }

    [Then("I am navigated to selected school home page with Trust name '(.*)'")]
    public async Task ThenIAmNavigatedToSelectedSchoolHomePageWithTrustName(string trustName)
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.IsDisplayed(trustName: trustName);
    }

    [When("I click on display as Net")]
    public async Task WhenIClickOnDisplayAsNet()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.ClickViewAsNet();
    }

    [Then("I can view the associated tooltip")]
    public async Task ThenICanViewTheAssociatedTooltip()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.TooltipIsDisplayed();
    }

    [Then("additional information contains")]
    public async Task ThenAdditionalInformationContains(DataTable table)
    {
        var expected = new List<List<string>>();
        {
            var headers = table.Header.ToList();

            expected.Add(headers);
            expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));
        }

        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsTableDataForTooltipDisplayed(expected);
    }

    [Then("additional information shows part year warning for (.*) months")]
    public async Task ThenAdditionalInformationShowsPartYearWarningForMonths(int months)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsPartYearWarningInTooltipDisplayed(months);
    }

    [Then("the nth chart bar (.*) displays the establishment name '(.*)'")]
    public async Task ThenTheNthChartBarDisplaysTheEstablishmentName(int nth, string name)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsGraphTickTextEqual(nth, name);
    }

    [Then("the nth chart bar (.*) displays the warning icon")]
    public async Task ThenTheNthChartBarDisplaysTheWarningIcon(int nth)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsWarningIconDisplayedOnGraphTick(nth);
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
            "catering staff" => ComparisonChartNames.CateringStaffAndServices,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
    }

    private static string CompareYourCostsUrl(string urn) => $"{TestConfiguration.ServiceUrl}/trust/{urn}/comparison";
}