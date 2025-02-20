using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority high needs benchmarking")]
public class HighNeedsBenchmarkingSteps(PageDriver driver)
{
    private HighNeedsBenchmarkingPage? _highNeedsBenchmarkingPage;
    private HighNeedsHistoricDataPage? _highNeedsHistoricDataPage;
    private HighNeedsNationalRankingsPage? _highNeedsNationalRankingsPage;
    private HighNeedsStartBenchmarkingPage? _highNeedsStartBenchmarkingPage;

    [Given("I am on local authority high needs benchmarking for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHighNeedsBenchmarkingForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHighNeedsBenchmarkingUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _highNeedsBenchmarkingPage = new HighNeedsBenchmarkingPage(page);
        await _highNeedsBenchmarkingPage.IsDisplayed();
    }

    [When("I click on start benchmarking")]
    public async Task WhenIClickOnStartBenchmarking()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        _highNeedsStartBenchmarkingPage = await _highNeedsBenchmarkingPage.ClickStartBenchmarking();
    }

    [When("I click on view national rankings")]
    public async Task WhenIClickOnViewNationalRankings()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        _highNeedsNationalRankingsPage = await _highNeedsBenchmarkingPage.ClickViewNationalRankings();
    }

    [When("I click on view historic data")]
    public async Task WhenIClickOnViewHistoricData()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        _highNeedsHistoricDataPage = await _highNeedsBenchmarkingPage.ClickViewHistoricData();
    }

    [Then("the start benchmarking page is displayed")]
    public async Task ThenTheStartBenchmarkingPageIsDisplayed()
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        await _highNeedsStartBenchmarkingPage.IsDisplayed();
    }

    [Then("the national rankings page is displayed")]
    public async Task ThenTheNationalRankingsPageIsDisplayed()
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.IsDisplayed();
    }

    [Then("the historic data page is displayed")]
    public async Task ThenTheHistoricDataPageIsDisplayed()
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.IsDisplayed();
    }

    private static string LocalAuthorityHighNeedsBenchmarkingUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs";
}