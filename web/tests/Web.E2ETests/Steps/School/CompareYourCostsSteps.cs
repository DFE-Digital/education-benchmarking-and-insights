using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Web.E2ETests.Pages.School.Comparators;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School compare your costs")]
public class CompareYourCostsSteps(PageDriver driver)
{
    private ComparatorsPage? _comparatorsPage;
    private CompareYourCostsPage? _comparisonPage;
    private IDownload? _download;
    private HomePage? _schoolHomePage;

    [Given("I am on compare your costs page for school with URN '(.*)'")]
    public async Task GivenIAmOnCompareYourCostsPageForSchoolWithUrn(string urn)
    {
        var url = CompareYourCostsUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _comparisonPage = new CompareYourCostsPage(page);
        await _comparisonPage.IsDisplayed();
    }

    [Given("I am on compare your costs page for part year school with URN '(.*)'")]
    public async Task GivenIAmOnCompareYourCostsPageForPartYearSchoolWithURN(string urn)
    {
        var url = CompareYourCostsUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _comparisonPage = new CompareYourCostsPage(page);
        await _comparisonPage.IsDisplayed(true);
    }

    [Given("I am on compare your costs page for missing comparator school with URN '(.*)'")]
    public async Task GivenIAmOnCompareYourCostsPageForMissingComparatorSchoolWithURN(string urn)
    {
        var url = CompareYourCostsUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _comparisonPage = new CompareYourCostsPage(page);
        await _comparisonPage.IsDisplayed(false, true);
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

    [When("I click on copy image for '(.*)'")]
    public async Task WhenIClickOnCopyImageFor(string chartName)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.ClickCopyImage(ChartNameFromFriendlyName(chartName));
    }

    [Then("the '(.*)' chart image is copied")]
    public async Task ThenTheChartImageIsCopied(string chartName)
    {
        Assert.NotNull(_comparisonPage);
        await ChartCopied(chartName);
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
        var expected = GetExpectedTableData(table);
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsTableDataForChartDisplayed(ChartNameFromFriendlyName(chartName), expected);
    }

    [Then("the following is shown in '(.*)' sub category '(.*)'")]
    public async Task ThenTheFollowingIsShownInSubCategory(string chartName, string subCategoryName, Table table)
    {
        var expected = GetExpectedTableData(table);
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsTableDataForChartDisplayed(ChartNameFromFriendlyName(chartName), expected, subCategoryName);
    }

    [Then("save as image buttons are hidden")]
    public async Task ThenSaveAsImageButtonsAreHidden()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.AreSaveAsImageButtonsDisplayed(false);
    }

    [Then("copy image buttons are hidden")]
    public async Task ThenCopyImageButtonsAreHidden()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.AreCopyImageButtonsDisplayed(false);
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
        await _comparisonPage.IsSchoolDetailsPopUpVisible();
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
        await _comparisonPage.TabToSchoolName();
    }

    [When("I press the Enter key when focused on the school name")]
    public async Task WhenIPressTheEnterKeyWhenFocusedOnTheSchoolName()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.AssertSchoolNameFocused();
        _schoolHomePage = await _comparisonPage.PressEnterKey();
    }

    [Then("I am navigated to selected school home page")]
    public async Task ThenIAmNavigatedToSelectedSchoolHomePage()
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.IsDisplayed();
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

    [When("I click on sets of similar school link")]
    public async Task WhenIClickOnSetsOfSimilarSchoolLink()
    {
        Assert.NotNull(_comparisonPage);
        _comparatorsPage = await _comparisonPage.ClickComparatorSetDetails();
    }

    [Then("I am taken to comparators page")]
    public async Task ThenIAmTakenToComparatorsPage()
    {
        Assert.NotNull(_comparatorsPage);
        await _comparatorsPage.IsDisplayed();

    }

    [Then("pupil cost comparators are (.*)")]
    public async Task ThenPupilCostComparatorsAre(string pupilComparators)
    {
        Assert.NotNull(_comparatorsPage);
        await _comparatorsPage.CheckRunningCostComparators(pupilComparators == "not null");
    }

    [Then("building cost comparators are (.*)")]
    public async Task ThenBuildingCostComparatorsAre(string buildingComparators)
    {
        Assert.NotNull(_comparatorsPage);
        await _comparatorsPage.CheckBuildingCostComparators(buildingComparators == "not null");
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

    [Then("the benchmarking charts are not displayed")]
    public async Task ThenTheBenchmarkingChartsAreNotDisplayed()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.AreComparisonChartsAndTablesDisplayed(false);
    }

    [Given("I have created a custom comparator set for '(.*)' containing")]
    public async Task GivenIHaveCreatedACustomComparatorSetForContaining(string urn, DataTable table)
    {
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(CreateComparatorsSteps.CreateComparatorsByNameUrl(urn));
        var createComparatorsByNamePage = new CreateComparatorsByNamePage(page);

        var urns = table.Rows.SelectMany(row => row.Select(cell => cell.Value));
        foreach (var u in urns)
        {
            await createComparatorsByNamePage.TypeIntoSchoolSearchBox(u);
            await createComparatorsByNamePage.SelectItemFromSuggester();
            await createComparatorsByNamePage.ClickChooseSchoolButton();
        }

        await createComparatorsByNamePage.ClickCreateSetButton();

        var url = CompareYourCostsUrl(urn);
        await page.GotoAndWaitForLoadAsync(url);
        _comparisonPage = new CompareYourCostsPage(page);
    }

    [Then("the message stating reason for less schools is visible in '(.*)' section")]
    public async Task ThenTheMessageStatingReasonForLessSchoolsIsVisible(string subCategorySection)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsWarningTextVisible(subCategorySection);

    }

    [Then("all sections on the page have the correct dimension options")]
    public async Task AllSectionsOnPageHaveCorrectDimensionOptions()
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.HasCorrectDimensionValues();
    }

    private void ChartDownloaded(string chartName)
    {
        Assert.NotNull(_download);
        var downloadedFileName = ChartNameFromFriendlyName(chartName) switch
        {
            ComparisonChartNames.TotalExpenditure => "total-expenditure",
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        var downloadedFilePath = _download.SuggestedFilename;
        Assert.Equal($"{downloadedFileName}.png", downloadedFilePath);
    }

    private async Task ChartCopied(string chartName)
    {
        const string exp = "navigator.clipboard.read()";
        var page = await driver.Current;
        var actual = await page.EvaluateAsync<object[]>(exp);
        Assert.NotNull(actual);
        Assert.Single(actual);
    }

    private static ComparisonChartNames ChartNameFromFriendlyName(string chartName)
    {
        return chartName switch
        {
            "total expenditure" => ComparisonChartNames.TotalExpenditure,
            "total number of teachers" => ComparisonChartNames.Premises,
            "non educational support staff" => ComparisonChartNames.NonEducationalSupportStaff,
            "catering staff" => ComparisonChartNames.CateringStaffAndServices,
            "Teaching and teaching support staff" => ComparisonChartNames.TeachingAndTeachingSupplyStaff,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
    }

    private List<List<string>> GetExpectedTableData(Table table)
    {
        var expected = new List<List<string>>();
        var headers = table.Header.ToList();
        expected.Add(headers);
        expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));

        return expected;
    }

    private static string CompareYourCostsUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/comparison";
}