using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Save chart images modal")]
public class SaveImagesModalSteps(PageDriver driver)
{
    private IDownload? _download;
    private SpendingCostsPage? _spendingCostsPage;

    [Given(@"I am on spending and costs page for school with URN '(.*)'")]
    public async Task GivenIAmOnSpendingAndCostsPageForSchoolWithUrn(string urn)
    {
        var url = SpendingCostsUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _spendingCostsPage = new SpendingCostsPage(page);
        await _spendingCostsPage.IsDisplayed();
    }

    [Then("the save chart images button is visible")]
    public async Task ThenTheSaveChartImagesButtonIsVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveImagesButtonDisplayed();
    }

    [When("I click the save chart images button")]
    public async Task WhenIClickTheSaveChartImagesButton()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.ClickSaveImagesButton();
    }

    [When("I click the start button")]
    public async Task WhenIClickTheStartButton()
    {
        Assert.NotNull(_spendingCostsPage);
        var page = await driver.Current;
        _download = await page.RunAndWaitForDownloadAsync(() => _spendingCostsPage.ClickSaveImagesModalOkButton(), new TimeSpan(0, 2, 0));
    }

    [When("I click the start button without any items selected")]
    public async Task WhenIClickTheStartButtonWithoutAnyItemsSelected()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.ClickSaveImagesModalOkButton();
    }

    [When("I click the cancel button")]
    public async Task WhenIClickTheCancelButton()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.ClickSaveImagesModalCancelButton();
    }

    [When("I click the close button")]
    public async Task WhenIClickTheCloseButton()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.ClickSaveImagesModalCloseButton();
    }

    [When("I press the Escape key")]
    public async Task WhenIPressTheEscapeKey()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.PressEscapeKey();
    }

    [Then("the save chart images modal is visible")]
    public async Task ThenTheSaveChartImagesModalIsVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveImagesModalDisplayed(true);
    }

    [Then("the save chart images modal is not visible")]
    public async Task ThenTheSaveChartImagesModalIsNotVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveImagesModalDisplayed(false);
    }

    [Then("the start button is enabled")]
    public async Task ThenTheStartButtonIsEnabled()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveImagesModalStartButtonEnabled(true);
    }

    [Then("the start button is disabled")]
    public async Task ThenTheStartButtonIsDisabled()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveImagesModalStartButtonEnabled(false);
    }

    [Then("the cancel button is visible")]
    public async Task ThenTheCancelButtonIsVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveImagesModalCancelButtonVisible();
    }

    [Then("the close button is visible")]
    public async Task ThenTheCloseButtonIsVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveImagesModalCloseButtonVisible();
    }

    [Then("the '(.*)' file is downloaded")]
    public void ThenTheFileIsDownloaded(string fileName)
    {
        Assert.NotNull(_spendingCostsPage);
        var downloadedFilePath = _download?.SuggestedFilename;
        Assert.Equal(fileName, downloadedFilePath);
    }

    [When("I uncheck the following items:")]
    public async Task WhenIUncheckTheFollowingItems(DataTable table)
    {
        Assert.NotNull(_spendingCostsPage);

        foreach (var row in table.Rows)
        {
            await _spendingCostsPage.ToggleSaveImagesModalCheckbox(row["Title"], false);
        }
    }

    [Then("the validation error is displayed")]
    public async Task ThenTheValidationErrorIsDisplayed()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveImagesModalValidationErrorMessageDisplayed();
    }

    private static string SpendingCostsUrl(string urn) =>
        $"{TestConfiguration.ServiceUrl}/school/{urn}/spending-and-costs";
}