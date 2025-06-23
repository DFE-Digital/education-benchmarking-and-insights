using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority high needs national rankings")]
public class HighNeedsNationalRankingsSteps(PageDriver driver)
{
    private HighNeedsNationalRankingsPage? _highNeedsNationalRankingsPage;
    private IDownload? _download;

    [Given("I am on local authority high needs national rankings for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHighNeedsNationalRankingsForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHighNeedsNationalRankingsUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _highNeedsNationalRankingsPage = new HighNeedsNationalRankingsPage(page);
        await _highNeedsNationalRankingsPage.IsDisplayed();
    }

    [When("I click on table view for '(.*)'")]
    public async Task WhenIClickOnTableView(string chartName)
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.ClickTab(chartName);
        await _highNeedsNationalRankingsPage.ClickViewAsTable(chartName);
    }

    [When("I click on tab '(.*)'")]
    public async Task WhenIClickOnTab(string tab)
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.ClickTab(tab);
    }

    [Then("the national rankings table for '(.*)' is displayed with the following values:")]
    public async Task ThenTheNationalRankingsTableIsDisplayedWithTheFollowingValues(string chartName, DataTable table)
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.TableContainsValues(chartName, table);
    }

    [When("I click on save as image for '(.*)'")]
    public async Task WhenIClickOnSaveAsImage(string chartName)
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        var page = await driver.Current;
        _download = await page.RunAndWaitForDownloadAsync(() => _highNeedsNationalRankingsPage.ClickSaveAsImage(chartName));
    }

    [Then("the chart image is downloaded for '(.*)'")]
    public void ThenTheChartImageIsDownloaded(string chartName)
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        Assert.NotNull(_download);
        var downloadedFilePath = _download.SuggestedFilename;
        Assert.Matches($"{chartName}.*\\.png$", downloadedFilePath);
    }

    [When("I click on copy image for '(.*)'")]
    public async Task WhenIClickOnCopyImage(string chartName)
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.ClickCopyImage(chartName);
    }

    [Then("the chart image is copied")]
    public async Task ThenTheChartImageIsCopied()
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        const string exp = "navigator.clipboard.read()";
        var page = await driver.Current;
        var actual = await page.EvaluateAsync<object[]>(exp);
        Assert.NotNull(actual);
        Assert.Single(actual);
    }

    [Then("the missing ranking warning message should not be displayed for '(.*)'")]
    public async Task ThenTheMissingRankingWarningMessageShouldNotBeDisplayed(string chartName)
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.DoesNotContainWarningMessage(chartName);
    }

    [Then("the missing ranking warning message should be displayed for '(.*)'")]
    public async Task ThenTheMissingRankingWarningMessageShouldBeDisplayed(string chartName)
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.ContainsWarningMessage(chartName, "There isn't enough information available to rank the current local authority.");
    }

    private static string LocalAuthorityHighNeedsNationalRankingsUrl(string laCode)
    {
        return $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs/national-rank";
    }
}