using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School benchmark IT spending")]
public class BenchmarkItSpendSteps(PageDriver driver)
{
    private BenchmarkItSpendPage? _itSpendPage;
    private HomePage? _schoolHomePage;

    [Given("I am on it spend page for school with URN '(.*)'")]
    public async Task GivenIAmOnItSpendPageForSchoolWithUrn(string urn)
    {
        _itSpendPage = await LoadItSpendPageForSchoolWithUrn(urn);
        await _itSpendPage.IsDisplayed();
    }

    [Given("the focused element is the last filter button")]
    public async Task GivenTheFocusedElementIsTheLastFilterButton()
    {
        Assert.NotNull(_itSpendPage);
        await _itSpendPage.FocusLastFilterButton();
    }

    [When("I click on the school name on the chart")]
    public async Task WhenIClickOnTheSchoolNameOnTheChart()
    {
        Assert.NotNull(_itSpendPage);
        _schoolHomePage = await _itSpendPage.ClickOnSchoolName();
    }

    [When("I enter on the school name on the chart")]
    public async Task WhenIEnterOnTheSchoolNameOnTheChart()
    {
        Assert.NotNull(_itSpendPage);
        _schoolHomePage = await _itSpendPage.EnterOnSchoolName();
    }

    [When("I hover on a bar for the school with urn '(.*)' in a chart")]
    public async Task WhenIHoverOnABarForTheSchoolWithUrnInAChart(string urn)
    {
        Assert.NotNull(_itSpendPage);
        await _itSpendPage.HoverOnChartBar(urn);
    }

    [When("I press tab to select the school with urn '(.*)' in a chart")]
    public async Task WhenIPressTabToSelectTheSchoolWithUrnInAChart(string urn)
    {
        Assert.NotNull(_itSpendPage);
        await _itSpendPage.PressTab();
    }

    [Then("I should see the following IT spend charts:")]
    public async Task ThenIShouldSeeTheFollowingITSpendCharts(Table table)
    {
        Assert.NotNull(_itSpendPage);

        var expectedTitles = table.Rows.Select(row => row["Chart Title"]);
        await _itSpendPage.AssertChartsVisible(expectedTitles);
    }

    [Then("I am navigated to selected school home page")]
    public async Task ThenIAmNavigatedToSelectedSchoolHomePage()
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.IsDisplayed();
    }

    [Then("the tooltip for '(.*)' is correctly displayed")]
    public async Task ThenTheTooltipForIsCorrectlyDisplayed(string name)
    {
        Assert.NotNull(_itSpendPage);
        await _itSpendPage.TooltipIsDisplayed(name);
    }

    [Then("the tooltip for '(.*)' is correctly displayed with part year warning for (.*) months")]
    public async Task ThenTheTooltipForIsCorrectlyDisplayedWithPartYearWarningForMonths(string name, int months)
    {
        Assert.NotNull(_itSpendPage);
        await _itSpendPage.TooltipIsDisplayedWithPartYearWarning(name, months);
    }

    private async Task<BenchmarkItSpendPage> LoadItSpendPageForSchoolWithUrn(string urn)
    {
        var url = BenchmarkItSpendUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        await driver.WaitForPendingRequests(500);

        return new BenchmarkItSpendPage(page);
    }

    private static string BenchmarkItSpendUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/benchmark-it-spending";
}