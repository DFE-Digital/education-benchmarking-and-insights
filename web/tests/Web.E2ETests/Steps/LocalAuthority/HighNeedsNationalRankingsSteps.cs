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

    [When("I click on table view")]
    public async Task WhenIClickOnTableView()
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.ClickViewAsTable();
    }

    [Then("the national rankings table is displayed with the following values:")]
    public async Task ThenTheNationalRankingsTableIsDisplayedWithTheFollowingValues(DataTable table)
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.TableContainsValues(table);
    }

    [When("I click on save as image")]
    public async Task WhenIClickOnSaveAsImage()
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        var page = await driver.Current;
        _download = await page.RunAndWaitForDownloadAsync(() => _highNeedsNationalRankingsPage.ClickSaveAsImage());
    }

    [Then("the chart image is downloaded")]
    public void ThenTheChartImageIsDownloaded()
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        Assert.NotNull(_download);
        var downloadedFilePath = _download.SuggestedFilename;
        Assert.Equal("national-ranking.png", downloadedFilePath);
    }

    [When("I click on copy image")]
    public async Task WhenIClickOnCopyImage()
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.ClickCopyImage();
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

    [Then("the missing ranking warning message should not be displayed")]
    public async Task ThenTheMissingRankingWarningMessageShouldNotBeDisplayed()
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.DoesNotContainWarningMessage();
    }

    [Then("the missing ranking warning message should be displayed")]
    public async Task ThenTheMissingRankingWarningMessageShouldBeDisplayed()
    {
        Assert.NotNull(_highNeedsNationalRankingsPage);
        await _highNeedsNationalRankingsPage.ContainsWarningMessage("There isn't enough information available to rank the current local authority.");
    }

    private static string LocalAuthorityHighNeedsNationalRankingsUrl(string laCode)
    {
        return $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs/national-rank";
    }
}