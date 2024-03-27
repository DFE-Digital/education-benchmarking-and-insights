using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School spending and costs")]
public class SpendingCostsSteps(PageDriver driver)
{
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
    
    [When("I click on how we choose similar schools")]
    public async Task WhenIClickOnHowWeChooseSimilarSchools()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.ClickComparatorSetDetails();
    }

    [Then("the details section is expanded")]
    public async Task ThenTheDetailsSectionIsExpanded()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsDetailsSectionVisible();
    }

    [Given("the order of charts is")]
    public async Task GivenTheOrderOfChartsIs(Table table)
    {
        Assert.NotNull(_spendingCostsPage);
        var expectedOrder = new List<string>(); 
        foreach (var row in table.Rows)
        {
            expectedOrder.Add(row["Name"]);
        }
        await _spendingCostsPage.CheckOrderOfCharts(expectedOrder);
    }
    private static string SpendingCostsUrl(string urn) =>
        $"{TestConfiguration.ServiceUrl}/school/{urn}/spending-and-costs";
    
}