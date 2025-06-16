using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Web.E2ETests.Pages.Trust.Benchmarking;
using Web.E2ETests.Pages.Trust.Comparators;
using Xunit;

namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "View Trust comparator set")]
public class ComparatorsSteps(PageDriver driver)
{
    private CreateComparatorsByNamePage? _createComparatorsByNamePage;
    private CreateComparatorsByPage? _createComparatorsByPage;
    private TrustBenchmarkSpendingPage? _trustBenchmarkSpendingPage;

    [Given("I have no previous comparators selected for company number '(.*)'")]
    public async Task GivenIHaveNoPreviousComparatorsSelectedForCompanyNumber(string companyNumber)
    {
        var url = RevertComparatorsUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        var revertComparatorsPage = new RevertComparatorsPage(page);
        await revertComparatorsPage.IsDisplayed();
        await revertComparatorsPage.ClickContinue();
    }

    [Given("I am on compare by page for trust with company number '(.*)'")]
    public async Task GivenIAmOnCompareByPageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = CreateComparatorsByUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _createComparatorsByPage = new CreateComparatorsByPage(page);
        await _createComparatorsByPage.IsDisplayed();
    }

    [When("I select the option By Name and click continue")]
    public async Task WhenISelectTheOptionByNameAndClickContinue()
    {
        Assert.NotNull(_createComparatorsByPage);
        await _createComparatorsByPage.SelectComparatorsBy(ComparatorsByTypes.Name);
        _createComparatorsByNamePage = await _createComparatorsByPage.ClickContinue(ComparatorsByTypes.Name) as CreateComparatorsByNamePage;
    }

    [When("I select the trust with company number '(.*)' from suggester")]
    public async Task WhenISelectTheTrustWithCompanyNumberFromSuggester(string companyNumber)
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.TypeIntoTrustSearchBox(companyNumber);
        await _createComparatorsByNamePage.SelectItemFromSuggester();
    }

    [When("I click the choose trust button")]
    public async Task WhenIClickTheChooseTrustButton()
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.ClickChooseTrustButton();
    }

    [When("I click the create set button")]
    public async Task WhenIClickTheCreateSetButton()
    {
        Assert.NotNull(_createComparatorsByNamePage);
        _trustBenchmarkSpendingPage = await _createComparatorsByNamePage.ClickCreateSetButton();
    }

    [When("I click on view as table")]
    public async Task WhenIClickOnViewAsTable()
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);
        await _trustBenchmarkSpendingPage.IsDisplayed();
        await _trustBenchmarkSpendingPage.ClickViewAsTable();
    }

    [When("I click on exclude central spending")]
    public async Task WhenIClickOnExcludeCentralSpending()
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);
        await _trustBenchmarkSpendingPage.ClickExcludeCentralSpending();
    }

    [When("I click on show all sections")]
    public async Task WhenIClickOnShowAllSections()
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);
        await _trustBenchmarkSpendingPage.ClickShowAllSections();
    }

    [Then("the table for '(.*)' contains the following:")]
    public async Task ThenTheTableForContainsTheFollowing(string category, DataTable table)
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);
        await _trustBenchmarkSpendingPage.TableContainsValues(category, table);
    }

    [Then("all sections on the page have the correct dimension options:")]
    public async Task ThenAllSectionsOnThePageHaveTheCorrectDimensionOptions(DataTable table)
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);

        foreach (var row in table.Rows)
        {
            var chartName = Enum.Parse<ComparisonChartNames>(row["Chart name"]);
            await _trustBenchmarkSpendingPage.HasDimensionValuesForChart(chartName, row["Options"].Split(",", StringSplitOptions.TrimEntries));
        }
    }

    [Then("the '(.*)' chart table contains the following:")]
    public async Task ThenSubChartTableContainsTheFollowing(string subChartName, DataTable table)
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);
        await _trustBenchmarkSpendingPage.HighExecutivePaySubChartTableHasExpectedValues(subChartName, table);
    }

    [Then("the '(.*)' chart table has a warning message stating reason for less rows is visible")]
    public async Task ThenSubChartTableHasWarning(string subChartName)
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);
        await _trustBenchmarkSpendingPage.IsSubChartNameWarningTextVisible(subChartName);
    }

    private static string RevertComparatorsUrl(string urn)
    {
        return $"{TestConfiguration.ServiceUrl}/trust/{urn}/comparators/revert";
    }
    private static string CreateComparatorsByUrl(string urn)
    {
        return $"{TestConfiguration.ServiceUrl}/trust/{urn}/comparators/create/by";
    }
}