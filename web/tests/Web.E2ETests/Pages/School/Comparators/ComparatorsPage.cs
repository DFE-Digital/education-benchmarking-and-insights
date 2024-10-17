using Microsoft.Playwright;
using Xunit;
namespace Web.E2ETests.Pages.School.Comparators;

public class ComparatorsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator CustomComparatorLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "Choose your own set of schools"
        });
    private ILocator RunningCostTab => page.Locator(Selectors.RunningCostCategoriesTab);
    private ILocator BuildingCostTab => page.Locator(Selectors.BuildingCostCategoriesTab);
    private ILocator RunningCostComparators => page.Locator(Selectors.Table).First.Locator("tr");
    private ILocator BuildingCostComparators => page.Locator(Selectors.Table).Nth(1).Locator("tr");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await CustomComparatorLink.ShouldBeVisible();
        await RunningCostTab.ShouldBeVisible();
        await BuildingCostTab.ShouldBeVisible();
    }

    public async Task CheckRunningCostComparators(bool isPresent) => Assert.Equal(isPresent ? 31 : 1, await RunningCostComparators.Count());

    public async Task CheckBuildingCostComparators(bool isPresent) => Assert.Equal(isPresent ? 31 : 1, await BuildingCostComparators.Count());
}