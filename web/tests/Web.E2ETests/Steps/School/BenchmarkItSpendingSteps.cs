using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School benchmark IT spending")]
public class BenchmarkItSpendSteps(PageDriver driver)
{
    private BenchmarkItSpendPage? _itSpendPage;
    private HomePage? _schoolHomePage;

    [Given("I am on it spend page for school with URN '(.*)'")]
    public async Task GivenIAmOnItSpendPageForSchoolWithUrn(string urn)
    {
        _itSpendPage = await LoadItSpendPageForSchoolWithUrn(urn);
        await _itSpendPage.IsDisplayed();
    }

    [When("I click on the school name on the chart")]
    public async Task WhenIClickOnTheSchoolNameOnTheChart()
    {
        Assert.NotNull(_itSpendPage);
        _schoolHomePage = await _itSpendPage.ClickOnSchoolName();
    }

    [When("I enter on the school name on the chart")]
    public async Task WhenIEnterOnTheSchoolNameOnTheChart()
    {
        Assert.NotNull(_itSpendPage);
        _schoolHomePage = await _itSpendPage.EnterOnSchoolName();
    }

    [Then("I should see the following IT spend charts:")]
    public async Task ThenIShouldSeeTheFollowingCharts(Table table)
    {
        Assert.NotNull(_itSpendPage);

        var expectedTitles = table.Rows.Select(row => row["Chart Title"]);
        await _itSpendPage.AssertChartsVisible(expectedTitles);
    }

    [Then("I am navigated to selected school home page")]
    public async Task ThenIAmNavigatedToSelectedSchoolHomePage()
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.IsDisplayed();
    }
    
    [When("I select the following subcategories:")]
    [Given("I select the following subcategories:")]
    public async Task WhenISelectTheFollowingSubcategories(Table table)
    {
        Assert.NotNull(_itSpendPage);
        var categoriesToSelect = table.Rows.Select(row => row["Subcategory"]);
        var subCategory = categoriesToSelect
            .Select(SubCategoryNameFromFriendlyName)
            .ToList();
        await _itSpendPage.SelectSubCategories(subCategory);
    }
    
    [When("I click the Apply filters button")]
    [Given("I click the Apply filters button")]
    public async Task WhenIClickTheApplyFiltersButton()
    {
        Assert.NotNull(_itSpendPage);
        await _itSpendPage.ClickApplyFilter();
    }
    
    [Then("the filter count should show '(.*)'")]
    public async Task ThenTheFilterCountShouldShow(string text)
    {
        Assert.NotNull(_itSpendPage);
        await _itSpendPage.AssertFilterCount(text);
    }
    
    [When("I click the clear button")]
    public async Task WhenIClickTheClearButton()
    {
        Assert.NotNull(_itSpendPage);
        await _itSpendPage.CLickClearFilter();
    }

    private static SubCategoryNames SubCategoryNameFromFriendlyName(String subCategoryName)
    {
        return subCategoryName switch
        {
            "Connectivity (E20A)" => SubCategoryNames.Connectivity,
            "IT support (E20G)" => SubCategoryNames.ITSupport,
            "Laptops, desktops and tablets (E20E)" => SubCategoryNames.LaptopsDesktopsTablets,
            _ => throw new ArgumentOutOfRangeException(nameof(subCategoryName))
        };
    }

    private async Task<BenchmarkItSpendPage> LoadItSpendPageForSchoolWithUrn(string urn)
    {
        var url = BenchmarkItSpendUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        await driver.WaitForPendingRequests(500);

        return new BenchmarkItSpendPage(page);
    }

    private static string BenchmarkItSpendUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/benchmark-it-spending";
}