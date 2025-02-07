using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Save all images modal")]
public class SaveAllImagesModalSteps(PageDriver driver)
{
    private SpendingCostsPage? _spendingCostsPage;
    private IDownload? _download;

    [Given(@"I am on spending and costs page for school with URN '(.*)'")]
    public async Task GivenIAmOnSpendingAndCostsPageForSchoolWithUrn(string urn)
    {
        var url = SpendingCostsUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _spendingCostsPage = new SpendingCostsPage(page);
        await _spendingCostsPage.IsDisplayed();
    }

    [Then("the save all images button is visible")]
    public async Task ThenTheSaveAllImagesButtonIsVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveAllImagesButtonDisplayed();
    }

    [When("I click the save all images button")]
    public async Task WhenIClickTheSaveAllImagesButton()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.ClickSaveAllImagesButton();
    }

    [When("I click the start button")]
    public async Task WhenIClickTheStartButton()
    {
        Assert.NotNull(_spendingCostsPage);
        var page = await driver.Current;
        var downloadTask = page.WaitForDownloadAsync(new TimeSpan(0, 1, 0));
        await _spendingCostsPage.ClickSaveAllImagesModalOkButton();
        _download = await downloadTask;
    }

    [When("I click the cancel button")]
    public async Task WhenIClickTheCancelButton()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.ClickSaveAllImagesModalCancelButton();
    }

    [When("I click the close button")]
    public async Task WhenIClickTheCloseButton()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.ClickSaveAllImagesModalCloseButton();
    }

    [When("I press the Escape key")]
    public async Task WhenIPressTheEscapeKey()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.PressEscapeKey();
    }

    [Then("the save all images modal is visible")]
    public async Task ThenTheSaveAllImagesModalIsVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveAllImagesModalDisplayed(true);
    }

    [Then("the save all images modal is not visible")]
    public async Task ThenTheSaveAllImagesModalIsNotVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveAllImagesModalDisplayed(false);
    }

    [Then("the start button is enabled")]
    public async Task ThenTheStartButtonIsEnabled()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveAllImagesModalStartButtonEnabled(true);
    }

    [Then("the start button is disabled")]
    public async Task ThenTheStartButtonIsDisabled()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveAllImagesModalStartButtonEnabled(false);
    }

    [Then("the cancel button is visible")]
    public async Task ThenTheCancelButtonIsVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveAllImagesModalCancelButtonVisible();
    }

    [Then("the close button is visible")]
    public async Task ThenTheCloseButtonIsVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveAllImagesModalCloseButtonVisible();
    }

    [Then("the '(.*)' file is downloaded")]
    public void ThenTheFileIsDownloaded(string fileName)
    {
        Assert.NotNull(_spendingCostsPage);
        var downloadedFilePath = _download?.SuggestedFilename;
        Assert.Equal(fileName, downloadedFilePath);
    }

    private static string SpendingCostsUrl(string urn) =>
        $"{TestConfiguration.ServiceUrl}/school/{urn}/spending-and-costs";
}