using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School.CustomData;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School create custom data")]
public class CreateCustomDataSteps(PageDriver driver)
{
    private ChangeFinancialDataPage? _changeFinancialDataPage;
    private ChangeNonFinancialDataPage? _changeNonFinancialDataPage;
    private ChangeWorkforceDataPage? _changeWorkforceDataPage;
    private CreateCustomDataPage? _createCustomDataPage;
    private CreateCustomDataSubmittedPage? _createCustomDataSubmittedPage;

    [Given("I am on create custom data page for school with URN '(.*)'")]
    public async Task GivenIAmOnCreateCustomDataPageForSchoolWithURN(string urn)
    {
        var url = CreateCustomDataUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _createCustomDataPage = new CreateCustomDataPage(page);
        await _createCustomDataPage.IsDisplayed();
    }

    [When("I click start now")]
    public async Task WhenIClickStartNow()
    {
        Assert.NotNull(_createCustomDataPage);
        _changeFinancialDataPage = await _createCustomDataPage.ClickStartNow();
    }

    [When("I supply the following financial data:")]
    public async Task WhenISupplyTheFollowingFinancialData(Table table)
    {
        Assert.NotNull(_changeFinancialDataPage);

        var rags = table.Rows.ToDictionary(row => row["Cost"], row => row["Value"]);
        foreach (var row in rags)
        {
            await _changeFinancialDataPage.TypeIntoCustomDataFieldForCost(row.Key, row.Value);
        }
    }

    [When("I supply the following non-financial data:")]
    public async Task WhenISupplyTheFollowingNonFinancialData(Table table)
    {
        Assert.NotNull(_changeNonFinancialDataPage);

        var rags = table.Rows.ToDictionary(row => row["Item"], row => row["Value"]);
        foreach (var row in rags)
        {
            await _changeNonFinancialDataPage.TypeIntoCustomDataFieldForItem(row.Key, row.Value);
        }
    }

    [When("I supply the following workforce data:")]
    public async Task WhenISupplyTheFollowingWorkforceData(Table table)
    {
        Assert.NotNull(_changeWorkforceDataPage);

        var rags = table.Rows.ToDictionary(row => row["Item"], row => row["Value"]);
        foreach (var row in rags)
        {
            await _changeWorkforceDataPage.TypeIntoCustomDataFieldForItem(row.Key, row.Value);
        }
    }

    [When("I click continue")]
    public async Task WhenIClickContinue()
    {
        if (_changeNonFinancialDataPage != null)
        {
            _changeWorkforceDataPage = await _changeNonFinancialDataPage.ClickContinue();
            return;
        }

        Assert.NotNull(_changeFinancialDataPage);
        _changeNonFinancialDataPage = await _changeFinancialDataPage.ClickContinue();
    }

    [When("I save the custom data")]
    public async Task WhenISaveTheCustomData()
    {
        Assert.NotNull(_changeWorkforceDataPage);
        _createCustomDataSubmittedPage = await _changeWorkforceDataPage.ClickSaveChangesButton();
    }

    [Then("the change financial data page is displayed")]
    public async Task ThenTheChangeFinancialDataPageIsDisplayed()
    {
        Assert.NotNull(_changeFinancialDataPage);
        await _changeFinancialDataPage.IsDisplayed();
    }

    [Then("the submitted page is displayed")]
    public async Task ThenTheSubmittedPageIsDisplayed()
    {
        Assert.NotNull(_createCustomDataSubmittedPage);
        await _createCustomDataSubmittedPage.IsDisplayed();
    }

    private static string CreateCustomDataUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/custom-data";
}