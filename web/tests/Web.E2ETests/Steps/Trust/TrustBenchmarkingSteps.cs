using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Web.E2ETests.Pages.Trust.Benchmarking;
using Xunit;

namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust Benchmarking")]
public class TrustBenchmarkingSteps(PageDriver driver)
{
    private CreateComparatorsByNamePage? _createComparatorsByNamePage;
    private CreateComparatorsByPage? _createComparatorsByPage;
    private TrustBenchmarkSpendingPage? _trustBenchmarkSpendingPage;


    [Given("I am on compare by page for trust with company number '(.*)'")]
    public async Task GivenIAmCompareByPageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = TrustBenchmarkingUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);
        _createComparatorsByPage = new CreateComparatorsByPage(page);
        await _createComparatorsByPage.IsDisplayed();

    }

    [Given("I am on create comparators page for trust with company number '(.*)'")]
    public async Task GivenIAmOnCreateComparatorsPageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = TrustBenchmarkingUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);
        _createComparatorsByPage = new CreateComparatorsByPage(page);
        await _createComparatorsByPage.IsDisplayed();
    }
    [When("I select the option By Name and continue")]
    public async Task WhenISelectTheOptionByNameAndClickContinue()
    {
        await SelectTheOptionByAndClickContinue(ComparatorsByTypes.Name);
    }
    [When("I select the trust with company number '(.*)' from suggester")]
    public async Task WhenISelectTheTrustWithCompanyNumberFromSuggester(string companyNumber)
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.TypeIntoSearchBox(companyNumber);
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

    [Then("the trust benchmark spending page is displayed")]
    public async Task ThenTheTrustBenchmarkSpendingPageIsDisplayed()
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);
        await _trustBenchmarkSpendingPage.IsDisplayed();
    }
    private async Task SelectTheOptionByAndClickContinue(ComparatorsByTypes type)
    {
        Assert.NotNull(_createComparatorsByPage);
        await _createComparatorsByPage.SelectComparatorsBy(type);
        _createComparatorsByNamePage = null;
        if (type == ComparatorsByTypes.Name)
        {
            _createComparatorsByNamePage = await _createComparatorsByPage.ClickContinue(type) as CreateComparatorsByNamePage;
        }

    }


    private static string TrustBenchmarkingUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}/comparators/create/by";
}