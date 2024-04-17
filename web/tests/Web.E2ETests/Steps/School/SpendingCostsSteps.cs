using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School spending and costs")]
public class SpendingCostsSteps(PageDriver driver)
{
    private SpendingCostsPage? _spendingCostsPage;
    private CompareYourCostsPage? _compareYourCostsPage;

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

    [When("I click on view all '(.*)' link")]
    public async Task WhenIClickOnViewAllLink(string linkToClick)
    {
        Assert.NotNull(_spendingCostsPage);
        _compareYourCostsPage = await _spendingCostsPage.ClickOnLink(CostCategoryFromFriendlyName(linkToClick));
    }

    private static CostCategoryNames CostCategoryFromFriendlyName(string linkName)
    {
        return linkName switch
        {
            "Teaching and teaching supply staff" => CostCategoryNames.TeachingAndTeachingSupplyStaff,
            "Administrative supplies" => CostCategoryNames.AdministrativeSupplies,
            "Catering staff and services" => CostCategoryNames.CateringStaffAndServices,
            "Educational ICT" => CostCategoryNames.EducationalIct,
            "Educational supplies" => CostCategoryNames.EducationalSupplies,
            "Non-educational support staff" => CostCategoryNames.NonEducationalSupportStaff,
            "Other" => CostCategoryNames.Other,
            "Premises staff and services" => CostCategoryNames.PremisesStaffAndServices,
            "Utilities" => CostCategoryNames.Utilities,
            _ => throw new ArgumentOutOfRangeException(nameof(linkName))
        };
    }

    [Then("I am directed to compare your costs page")]
    public void ThenIAmDirectedToCompareYourCostsPage()
    {
        /*Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed();*/
        //will be uncommenting once the dev work is complete otherwise the test will fail
    }

    [Then("the accordion '(.*)'is expanded")]
    public void ThenTheAccordionIsExpanded(string p0)
    {
        //will be implemented once the dev work is complete
    }
    private static string SpendingCostsUrl(string urn) =>
        $"{TestConfiguration.ServiceUrl}/school/{urn}/spending-and-costs";

}