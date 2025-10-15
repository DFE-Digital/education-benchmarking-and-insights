using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority homepage")]
public class HomeSteps(PageDriver driver)
{
    private BenchmarkCensusPage? _benchmarkCensusPage;
    private CompareYourCostsPage? _compareYourCostsPage;
    private HomePage? _localAuthorityHomePage;
    private HighNeedsStartBenchmarkingPage? _benchmarkHighNeedsPage;
    private HighNeedsHistoricDataPage? _highNeedsHistoryPage;

    [Given("I am on local authority homepage for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHomepageForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHomeUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _localAuthorityHomePage = new HomePage(page);
        await _localAuthorityHomePage.IsDisplayed();
    }

    [When("I click on compare your costs")]
    public async Task WhenIClickOnCompareYourCosts()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _compareYourCostsPage = await _localAuthorityHomePage.ClickCompareYourCosts();
    }

    [Then("the compare your costs page is displayed")]
    public async Task ThenTheCompareYourCostsPageIsDisplayed()
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed();
    }

    [When("I click on benchmark census data")]
    public async Task WhenIClickOnBenchmarkCensusData()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _benchmarkCensusPage = await _localAuthorityHomePage.ClickBenchmarkCensus();
    }

    [When("I click on benchmark high needs")]
    public async Task WhenIClickOnBenchmarkHighNeeds()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _benchmarkHighNeedsPage = await _localAuthorityHomePage.ClickBenchmarkHighNeeds();
    }

    [When("I click on high needs historic data")]
    public async Task WhenIClickOnHighNeedsHistoricData()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _highNeedsHistoryPage = await _localAuthorityHomePage.ClickHighNeedsHistory();
    }

    [Then("the benchmark census page is displayed")]
    public async Task ThenTheBenchmarkCensusPageIsDisplayed()
    {
        Assert.NotNull(_benchmarkCensusPage);
        await _benchmarkCensusPage.IsDisplayed();
    }

    [Then("the service banner displays the title '(.*)', heading '(.*)' and body '(.*)'")]
    public async Task ThenTheServiceBannerDisplaysTheTitleHeadingAndBody(string title, string heading, string body)
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.HasBanner(title, heading, body);
    }

    [Then("the High needs benchmarking page is displayed")]
    public async Task ThenTheHighNeedsBenchmarkingPageIsDisplayed()
    {
        Assert.NotNull(_benchmarkHighNeedsPage);
        await _benchmarkHighNeedsPage.IsDisplayed();
    }

    [Then("the High needs historic data page is displayed")]
    public async Task ThenTheHighNeedsHistoricDataPageIsDisplayed()
    {
        Assert.NotNull(_highNeedsHistoryPage);
        await _highNeedsHistoryPage.IsDisplayed();
    }

    [Then("the schools accordion should be displayed")]
    public async Task ThenTheSchoolsAccordionShouldBeDisplayed()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.IsSchoolsAccordionDisplayed();
    }

    [Then("the schools accordion should not be displayed")]
    public async Task ThenTheSchoolsAccordionShouldNotBeDisplayed()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.IsSchoolsAccordionDisplayed(false);
    }

    [Then("the priority school RAGs section should be displayed for '(.*)' containing the following rows:")]
    public async Task ThenThePrioritySchoolRaGsSectionShouldBeDisplayedForContainingTheFollowingRows(string overallPhase, DataTable table)
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.ContainsPriorityRagsForPhase(overallPhase, table);
    }

    private static string LocalAuthorityHomeUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}";
}