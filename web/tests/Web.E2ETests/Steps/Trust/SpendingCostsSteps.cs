using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Xunit;
namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust spending and costs")]
public class SpendingCostsSteps(PageDriver driver)
{
    private SpendingCostsPage? _spendingCostsPage;

    [Given("I am on spending and costs for trust with company number '(.*)'")]
    public async Task GivenIAmOnSpendingAndCostsForTrustWithCompanyNumber(string companyNumber)
    {
        var url = SpendingCostsUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _spendingCostsPage = new SpendingCostsPage(page);
        await _spendingCostsPage.IsDisplayed();
    }

    [Then("the priority categories are:")]
    public async Task ThenThePriorityCategoriesAre(DataTable table)
    {
        Assert.NotNull(_spendingCostsPage);

        foreach (var row in table.Rows)
        {
            await _spendingCostsPage.AssertCategoryPriority(row["Priority"], row["Category"], row["Commentary"]);
        }
    }

    private static string SpendingCostsUrl(string urn) => $"{TestConfiguration.ServiceUrl}/trust/{urn}/spending-and-costs";
}