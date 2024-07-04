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

    [Given("the priority order of charts is")]
    public async Task GivenThePriorityOrderOfChartsIs(Table table)
    {
        Assert.NotNull(_spendingCostsPage);
        var expectedOrder = new List<string[]>();
        foreach (var row in table.Rows)
        {
            string[] chartPriorityArray = {
                row["Name"],
                row["Priority"]
            };
            expectedOrder.Add(chartPriorityArray);
        }

        await _spendingCostsPage.CheckOrderOfCharts(expectedOrder);
    }

    [When("I click on view all '(.*)' link")]
    public async Task WhenIClickOnViewAllLink(string linkToClick)
    {
        Assert.NotNull(_spendingCostsPage);
        _compareYourCostsPage = await _spendingCostsPage.ClickOnLink(CostCategoryFromFriendlyName(linkToClick));
    }

    [Then("I am directed to compare your costs page")]
    public async Task ThenIAmDirectedToCompareYourCostsPage()
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed();
    }

    [Then("the accordion '(.*)'is expanded")]
    public async Task ThenTheAccordionIsExpanded(string chartName)
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsSectionVisible(ChartNameFromFriendlyName(chartName), true, "Hide", "chart");
    }
    private static string SpendingCostsUrl(string urn) =>
        $"{TestConfiguration.ServiceUrl}/school/{urn}/spending-and-costs";

    private static CostCategoryNames CostCategoryFromFriendlyName(string linkName)
    {
        return linkName switch
        {
            "Teaching and Teaching support staff" => CostCategoryNames.TeachingAndTeachingSupplyStaff,
            "Administrative supplies" => CostCategoryNames.AdministrativeSupplies,
            "Catering staff and supplies" => CostCategoryNames.CateringStaffAndServices,
            "Educational ICT" => CostCategoryNames.EducationalIct,
            "Educational supplies" => CostCategoryNames.EducationalSupplies,
            "Non-educational support staff" => CostCategoryNames.NonEducationalSupportStaff,
            "Other costs" => CostCategoryNames.Other,
            "Premises staff and services" => CostCategoryNames.PremisesStaffAndServices,
            "Utilities" => CostCategoryNames.Utilities,
            _ => throw new ArgumentOutOfRangeException(nameof(linkName))
        };
    }
    private static ComparisonChartNames ChartNameFromFriendlyName(string chartName)
    {
        return chartName switch
        {
            "Teaching and Teaching support staff" => ComparisonChartNames.TeachingAndTeachingSupplyStaff,
            "Administrative supplies" => ComparisonChartNames.AdministrativeSupplies,
            "Catering staff and supplies" => ComparisonChartNames.CateringStaffAndServices,
            "Educational ICT" => ComparisonChartNames.EducationalIct,
            "Educational supplies" => ComparisonChartNames.EducationalSupplies,
            "Non-educational support staff" => ComparisonChartNames.NonEducationalSupportStaff,
            "Other costs" => ComparisonChartNames.Other,
            "Premises staff and services" => ComparisonChartNames.Premises,
            "Utilities" => ComparisonChartNames.Utilities,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
    }
}