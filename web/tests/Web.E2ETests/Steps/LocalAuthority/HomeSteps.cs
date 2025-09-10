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
    private HighNeedsDashboardPage? _highNeedsBenchmarkingPage;
    private HomePage? _localAuthorityHomePage;

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

    [Then("the benchmark census page is displayed")]
    public async Task ThenTheBenchmarkCensusPageIsDisplayed()
    {
        Assert.NotNull(_benchmarkCensusPage);
        await _benchmarkCensusPage.IsDisplayed();
    }

    [When("I click on high needs benchmarking")]
    public async Task WhenIClickOnHighNeedsBenchmarking()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _highNeedsBenchmarkingPage = await _localAuthorityHomePage.ClickHighNeedsBenchmarking();
    }

    [Then("the high needs benchmarking dashboard page is displayed")]
    public async Task ThenTheHighNeedsBenchmarkingDashboardPageIsDisplayed()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.IsDisplayed();
    }

    [Then("the service banner displays the title '(.*)', heading '(.*)' and body '(.*)'")]
    public async Task ThenTheServiceBannerDisplaysTheTitleHeadingAndBody(string title, string heading, string body)
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.HasBanner(title, heading, body);
    }

    private static string LocalAuthorityHomeUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}";
}