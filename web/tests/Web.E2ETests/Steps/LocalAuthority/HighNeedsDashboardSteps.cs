using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority high needs dashboard")]
public class HighNeedsBenchmarkingDashboardSteps(PageDriver driver)
{
    private HighNeedsDashboardPage? _highNeedsBenchmarkingPage;
    private HighNeedsGlossaryPage? _highNeedsGlossaryPage;
    private HighNeedsHistoricDataPage? _highNeedsHistoricDataPage;
    private HighNeedsNationalRankingsPage? _highNeedsNationalRankingsPage;
    private HighNeedsStartBenchmarkingPage? _highNeedsStartBenchmarkingPage;

    [Given("I am on local authority high needs dashboard for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHighNeedsDashboardForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHighNeedsDashboardUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _highNeedsBenchmarkingPage = new HighNeedsDashboardPage(page);
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

    [When("I click on Glossary of terms link")]
    public async Task WhenIClickOnGlossaryOfTermsLink()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        _highNeedsGlossaryPage = await _highNeedsBenchmarkingPage.ClickOnHighNeedsGlossaryLink();
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

    [Then("the high needs glossary page is displayed")]
    public async Task ThenTheHighNeedsGlossaryPageIsDisplayed()
    {
        Assert.NotNull(_highNeedsGlossaryPage);
        await _highNeedsGlossaryPage.IsDisplayed();
    }

    private static string LocalAuthorityHighNeedsDashboardUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs";
}