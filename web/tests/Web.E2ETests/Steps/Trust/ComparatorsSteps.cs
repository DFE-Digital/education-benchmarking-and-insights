using Web.E2ETests.Drivers;
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

    [Then("the table for '(.*)' contains the following:")]
    public async Task ThenTheTableForContainsTheFollowing(string category, DataTable table)
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);
        await _trustBenchmarkSpendingPage.TableContainsValues(category, table);
    }

    private static string CreateComparatorsByUrl(string urn) => $"{TestConfiguration.ServiceUrl}/trust/{urn}/comparators/create/by";
}