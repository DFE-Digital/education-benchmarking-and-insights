using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust.Benchmarking;
using Web.E2ETests.Pages.Trust.Comparators;
using Xunit;

namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust create comparator set")]
public class CreateComparatorsSteps(PageDriver driver)
{
    private CreateComparatorsByCharacteristicPage? _createComparatorsByCharacteristicPage;
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

    [Given("I am on compare by page with redirectUri '(.*)' for trust with company number '(.*)'")]
    public async Task GivenIAmOnCompareByPageWithRedirectUriForTrustWithCompanyNumber(string redirectUri, string companyNumber)
    {
        var url = CreateComparatorsByUrl(companyNumber, redirectUri);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _createComparatorsByPage = new CreateComparatorsByPage(page);
        await _createComparatorsByPage.IsDisplayed();
    }

    [When("I select the option By Characteristic and click continue")]
    public async Task WhenISelectTheOptionByCharacteristicAndClickContinue()
    {
        await SelectTheOptionByAndClickContinue(ComparatorsByTypes.Characteristic);
    }

    [When("I select the option By Name and click continue")]
    public async Task WhenISelectTheOptionByNameAndClickContinue()
    {
        await SelectTheOptionByAndClickContinue(ComparatorsByTypes.Name);
    }

    [When("I select the trust with urn '(.*)' from suggester")]
    public async Task WhenISelectTheTrustWithUrnFromSuggester(string urn)
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.TypeIntoTrustSearchBox(urn);
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
        _trustBenchmarkSpendingPage = await _createComparatorsByNamePage.ClickCreateSetButton(p => new TrustBenchmarkSpendingPage(p));
    }

    [Then("the create comparators by page is displayed")]
    public async Task ThenTheCreateComparatorsByPageIsDisplayed()
    {
        Assert.NotNull(_createComparatorsByPage);
        await _createComparatorsByPage.IsDisplayed();
    }

    [Then("the create comparators by characteristic page is displayed")]
    public async Task ThenTheCreateComparatorsByCharacteristicPageIsDisplayed()
    {
        Assert.NotNull(_createComparatorsByCharacteristicPage);
        await _createComparatorsByCharacteristicPage.IsDisplayed();
    }

    [Then("the create comparators by name page is displayed")]
    public async Task ThenTheCreateComparatorsByNamePageIsDisplayed()
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.IsDisplayed();
    }

    [Then("the trust benchmark spending page is displayed")]
    public async Task ThenTheTrustBenchmarkSpendingPageIsDisplayed()
    {
        Assert.NotNull(_trustBenchmarkSpendingPage);
        await _trustBenchmarkSpendingPage.IsDisplayed();
    }

    [Then("the '(.*)' page is displayed")]
    public void ThenThePageIsDisplayed(string url)
    {
        Assert.Equal($"{TestConfiguration.ServiceUrl}{url}?comparator-generated=true", driver.Current.Result.Url);
    }

    private async Task SelectTheOptionByAndClickContinue(ComparatorsByTypes type)
    {
        Assert.NotNull(_createComparatorsByPage);
        await _createComparatorsByPage.SelectComparatorsBy(type);
        _createComparatorsByNamePage = null;
        _createComparatorsByCharacteristicPage = null;

        if (type == ComparatorsByTypes.Characteristic)
        {
            _createComparatorsByCharacteristicPage = await _createComparatorsByPage.ClickContinue(type) as CreateComparatorsByCharacteristicPage;
        }
        else
        {
            _createComparatorsByNamePage = await _createComparatorsByPage.ClickContinue(type) as CreateComparatorsByNamePage;
        }
    }

    private static string RevertComparatorsUrl(string urn) => $"{TestConfiguration.ServiceUrl}/trust/{urn}/comparators/revert";

    private static string CreateComparatorsByUrl(string urn, string? redirectUri = null) => $"{TestConfiguration.ServiceUrl}/trust/{urn}/comparators/create/by{(redirectUri == null ? string.Empty : $"?redirectUri={redirectUri}")}";
}