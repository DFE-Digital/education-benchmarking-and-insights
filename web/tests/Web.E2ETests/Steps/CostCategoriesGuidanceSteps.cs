using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Xunit;
namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Cost categories guidance")]
public class CostCategoriesGuidanceSteps(PageDriver driver)
{
    private CostCategoriesGuidancePage? _costCategoriesGuidancePage;

    [Given("I am on cost categories guidance page")]
    public async Task GivenIAmOnCostCategoriesGuidancePage()
    {
        var url = CostCategoriesGuidanceUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _costCategoriesGuidancePage = new CostCategoriesGuidancePage(page);
        await _costCategoriesGuidancePage.IsDisplayed();
    }

    [Then("the sub categories for each category are")]
    public async Task ThenTheSubCategoriesForEachCategoryAre(DataTable table)
    {
        Assert.NotNull(_costCategoriesGuidancePage);

        var categories = new Dictionary<string, List<string>>();
        foreach (var row in table.Rows)
        {
            if (!categories.TryGetValue(row["Category"], out var value))
            {
                value = [];
                categories.Add(row["Category"], value);
            }

            if (!string.IsNullOrWhiteSpace(row["Sub-category"]))
            {
                value.Add(row["Sub-category"]);
            }
        }

        foreach (var row in categories)
        {
            await _costCategoriesGuidancePage.AssertSubCategories(row.Key, row.Value.ToArray());
        }
    }

    private static string CostCategoriesGuidanceUrl() => $"{TestConfiguration.ServiceUrl}/guidance/cost-categories";
}