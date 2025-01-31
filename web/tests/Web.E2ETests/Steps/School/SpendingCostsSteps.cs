using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School spending and costs")]
public class SpendingCostsSteps(PageDriver driver)
{
    private CompareYourCostsPage? _compareYourCostsPage;
    private CostCategoriesGuidancePage? _costCategoriesGuidancePage;
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

    [Then("the priority order of charts is")]
    public async Task ThenThePriorityOrderOfChartsIs(DataTable table)
    {
        Assert.NotNull(_spendingCostsPage);
        var expectedOrder = new List<string[]>();
        foreach (var row in table.Rows)
        {
            string[] chartPriorityArray = { row["Name"], row["Priority"] };
            expectedOrder.Add(chartPriorityArray);
        }

        await _spendingCostsPage.AssertOrderOfCharts(expectedOrder);
    }

    [Then("the RAG commentary for each category is")]
    public async Task ThenTheRagCommentaryForEachCategoryIs(DataTable table)
    {
        Assert.NotNull(_spendingCostsPage);
        foreach (var row in table.Rows)
        {
            await _spendingCostsPage.AssertRagCommentary(row["Name"], row["Commentary"]);
        }
    }

    [Then("the commercial resources for each category are")]
    public async Task ThenTheCommercialResourcesForEachCategoryAre(DataTable table)
    {
        Assert.NotNull(_spendingCostsPage);

        var commercialResources = new Dictionary<string, List<string>>();
        foreach (var row in table.Rows)
        {
            if (!commercialResources.TryGetValue(row["Name"], out var value))
            {
                value = [];
                commercialResources.Add(row["Name"], value);
            }

            if (!string.IsNullOrWhiteSpace(row["Resource"]))
            {
                value.Add(row["Resource"]);
            }
        }

        foreach (var row in commercialResources)
        {
            await _spendingCostsPage.AssertCommercialResources(row.Key, row.Value.ToArray());
        }
    }

    [Then("the category commentary is")]
    public async Task ThenTheCategoryCommentaryIs(DataTable table)
    {
        Assert.NotNull(_spendingCostsPage);
        foreach (var row in table.Rows)
        {
            await _spendingCostsPage.AssertCategoryCommentary(row["Name"], row["Commentary"]);
        }
    }

    [When("I click on view all '(.*)' link")]
    public async Task WhenIClickOnViewAllLink(string linkToClick)
    {
        Assert.NotNull(_spendingCostsPage);
        _compareYourCostsPage = await _spendingCostsPage.ClickOnLink(CostCategoryFromFriendlyName(linkToClick));
    }

    [When("I click on View more details on cost categories link")]
    public async Task WhenIClickOnViewMoreDetailsOnCostCategoriesLink()
    {
        Assert.NotNull(_spendingCostsPage);
        _costCategoriesGuidancePage = await _spendingCostsPage.ClickOnCostCategoriesGuidanceLink();
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

    [Then("I am directed to cost categories guidance page")]
    public async Task ThenIAmDirectedToCostCategoriesGuidancePage()
    {
        Assert.NotNull(_costCategoriesGuidancePage);
        await _costCategoriesGuidancePage.IsDisplayed();
    }

    [Then("the '(.*)' category should display:")]
    public async Task ThenTheCategoryShouldDisplay(string costCategory, Table table)
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.AssertCostCategoryData(CostCategoryFromFriendlyName(costCategory), table);
    }

    [Then("the message stating reason for less schools is visible for '(.*)'")]
    public async Task ThenTheMessageStatingReasonForLessSchoolsIsVisible(string categoryName)
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsWarningMessageVisibleForCategory(CostCategoryFromFriendlyName(categoryName));
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

    [Then("the save all images modal is visible")]
    public async Task ThenTheSaveAllImagesModalIsVisible()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsSaveAllImagesModalDisplayed(true);
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