using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Web.E2ETests.Pages.Trust.Benchmarking;
using Xunit;

namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust Benchmarking")]
public class TrustBenchmarkingSteps(PageDriver driver)
{
    private HomePage? _trustHomePage;
    private CreateComparatorsByNamePage? _createComparatorsByNamePage;
    private CreateComparatorsByPage? _createComparatorsByPage;
    private TrustBenchmarkSpendingPage? _trustBenchmarkSpendingPage;


    [Given("I am on trust homepage for trust with company number '(.*)'")]
    public async Task GivenIAmOnTrustHomepageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = TrustHomeUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _trustHomePage = new HomePage(page);
        await _trustHomePage.IsDisplayed();
    }
    
    [Given("I click on trust benchmarking link")]
    public async Task GivenIClickOnTrustBenchmarkingLink()
    {
        Assert.NotNull(_trustHomePage);
        var navigationResponse = await _trustHomePage.ClickTrustBenchmarkingLink();
        if (navigationResponse is TrustBenchmarkSpendingPage trustBenchmarkSpendingPage)
        {
            _trustBenchmarkSpendingPage = trustBenchmarkSpendingPage;
           await _trustBenchmarkSpendingPage.IsDisplayed();

        }
        else if (navigationResponse is CreateComparatorsByPage createComparatorsByPage)
        {
            _createComparatorsByPage = createComparatorsByPage;
            await _createComparatorsByPage.IsDisplayed();
        }
        else
        {
            throw new Exception("Unexpected page type.");
        }
    }
    [Given("I am on create comparators page for trust with company number '(.*)'")]
    public async Task GivenIAmOnCreateComparatorsPageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = TrustBenchmarkingUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);
        _createComparatorsByPage = new CreateComparatorsByPage(page);
        await _createComparatorsByPage.IsDisplayed();
        
        
        //delete comparators set if already created
        
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

    private static string TrustHomeUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}";

    private static string TrustBenchmarkingUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}/comparators";


    
}