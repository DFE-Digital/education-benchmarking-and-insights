using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School senior leadership")]
public class SchoolSeniorLeadershipSteps(PageDriver driver)
{
    private SchoolSeniorLeadershipPage? _seniorLeadershipPage;
    private HomePage? _schoolHomePage;
    private IDownload? _download;

    [Given("I am on school senior leadership page for school with URN '(.*)'")]
    public async Task GivenIAmOnSeniorLeadershipPageForSchoolWithUrn(string urn)
    {
        _seniorLeadershipPage = await LoadSchoolSeniorLeadershipPageForSchoolWithUrn(urn);
        await _seniorLeadershipPage.IsDisplayed();
    }

    [Given("the focused element is the save as image control")]
    public async Task GivenTheFocusedElementIsTheImageSaveControl()
    {
        Assert.NotNull(_seniorLeadershipPage);
        await _seniorLeadershipPage.FocusImageSaveControl();
    }

    [When("I click on the school name on the chart")]
    public async Task WhenIClickOnTheSchoolNameOnTheChart()
    {
        Assert.NotNull(_seniorLeadershipPage);
        _schoolHomePage = await _seniorLeadershipPage.ClickOnSchoolName();
    }

    [When("I enter on the school name on the chart")]
    public async Task WhenIEnterOnTheSchoolNameOnTheChart()
    {
        Assert.NotNull(_seniorLeadershipPage);
        _schoolHomePage = await _seniorLeadershipPage.EnterOnSchoolName();
    }

    [When("I hover on a bar for the school with urn '(.*)' in a chart")]
    public async Task WhenIHoverOnABarForTheSchoolWithUrnInAChart(string urn)
    {
        Assert.NotNull(_seniorLeadershipPage);
        await _seniorLeadershipPage.HoverOnChartBar(urn);
    }

    [When("I press tab to select the school with urn '(.*)' in a chart")]
    public async Task WhenIPressTabToSelectTheSchoolWithUrnInAChart(string urn)
    {
        Assert.NotNull(_seniorLeadershipPage);
        await _seniorLeadershipPage.PressTab();
    }

    [When("I click on save as image")]
    public async Task WhenIClickOnSaveAsImageFor()
    {
        Assert.NotNull(_seniorLeadershipPage);
        var page = await driver.Current;
        _download = await page.RunAndWaitForDownloadAsync(() => _seniorLeadershipPage.ClickImageSaveControl());
    }

    [Then("I should see the school senior leadership chart")]
    public async Task ThenIShouldSeeTheSchoolSeniorLeadershipChart()
    {
        Assert.NotNull(_seniorLeadershipPage);

        await _seniorLeadershipPage.AssertChartVisible("School Senior Leadership");
    }

    [Then("I am navigated to selected school home page")]
    public async Task ThenIAmNavigatedToSelectedSchoolHomePage()
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.IsDisplayed(isMissingRags: true);
    }

    [Then("the tooltip for '(.*)' is correctly displayed")]
    public async Task ThenTheTooltipForIsCorrectlyDisplayed(string name)
    {
        Assert.NotNull(_seniorLeadershipPage);
        await _seniorLeadershipPage.TooltipIsDisplayed(name);
    }

    [Then("the '(.*)' chart image is downloaded")]
    public void ThenTheChartImageIsDownloaded(string downloadedFileName)
    {
        Assert.NotNull(_seniorLeadershipPage);
        ChartDownloaded(downloadedFileName);
    }

    private void ChartDownloaded(string downloadedFileName)
    {
        Assert.NotNull(_download);

        var downloadedFilePath = _download.SuggestedFilename;
        Assert.Equal($"{downloadedFileName}.png", downloadedFilePath);
    }

    private async Task<SchoolSeniorLeadershipPage> LoadSchoolSeniorLeadershipPageForSchoolWithUrn(string urn)
    {
        var url = SchoolSeniorLeadershipUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        await driver.WaitForPendingRequests(500);

        return new SchoolSeniorLeadershipPage(page);
    }

    private static string SchoolSeniorLeadershipUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/census/senior-leadership";
}