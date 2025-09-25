using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust.Benchmarking;
using Web.E2ETests.Pages.Trust.Comparators;
using Xunit;

namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust benchmark IT spending")]
public class BenchmarkItSpendingSteps(PageDriver driver)
{
    private CreateComparatorsByNamePage? _createComparatorsByNamePage;
    private CreateComparatorsByPage? _createComparatorsByPage;
    private BenchmarkItSpendingPage? _benchmarkItSpendingPage;
    private ViewComparatorsPage? _viewComparatorsPage;

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

    [Given("I am on IT spend for trust with company number '(.*)'")]
    public async Task GivenIAmOnITSpendForTrustWithCompanyNumber(string companyNumber)
    {
        var url = BenchmarkItSpendingUrl(companyNumber);
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
        _benchmarkItSpendingPage = await _createComparatorsByNamePage.ClickCreateSetButton(p => new BenchmarkItSpendingPage(p));
    }

    [When("I click on view comparator set")]
    public async Task WhenIClickOnViewComparatorSet()
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        _viewComparatorsPage = await _benchmarkItSpendingPage.ClickViewComparatorSetLink();
    }

    [Then("the IT spend for trust page for company number '(.*)' is displayed")]
    public async Task ThenTheITSpendForTrustPageForCompanyNumberIsDisplayed(string companyNumber)
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        await _benchmarkItSpendingPage.IsDisplayed();
    }

    [Then("the comparator set page is displayed")]
    public async Task ThenTheComparatorSetPageIsDisplayed()
    {
        Assert.NotNull(_viewComparatorsPage);
        await _viewComparatorsPage.IsDisplayed();
    }

    private static string RevertComparatorsUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}/comparators/revert";
    private static string BenchmarkItSpendingUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}/benchmark-it-spending";
}