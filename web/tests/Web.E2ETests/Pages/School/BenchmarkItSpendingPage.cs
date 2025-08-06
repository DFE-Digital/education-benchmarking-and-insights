using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public enum SubCategoryNames
{
    Connectivity,
    ITSupport,
    LaptopsDesktopsTablets
}
public class BenchmarkItSpendPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ChartContainers => page.Locator(Selectors.SsrChartContainer);
    private ILocator ChartContainer(string chartName) => page.Locator($"[data-title=\"{chartName}\"]");
    private ILocator SchoolLinksInCharts => page.Locator(Selectors.SsrSchoolNamesLinksInCharts);
    private ILocator SubCategoriesAccordionSectionToggle => page.Locator(Selectors.AccordionSectionToggleText);
    private ILocator ConnectivitySubCategoryCheckbox => page.Locator(Selectors.ConnectivityCheckBox);
    private ILocator ITSupportSubCategoryCheckBox => page.Locator(Selectors.ITSupportCheckBox);
    private ILocator LaptopDesktopSubCategoryCheckbox => page.Locator(Selectors.LaptopDesktopCheckbox);
    private ILocator ApplyFilterButton => page.Locator(Selectors.Button, new PageLocatorOptions
    {
        HasText = "Apply filters"
    });

    private ILocator AppliedFiltersCount => page.Locator($"{Selectors.GovHint}.app-filter__selected-hint");
    private ILocator ComparatorSetDetails =>
        page.Locator(Selectors.GovLink,
            new PageLocatorOptions
            {
                HasText = "We've chosen 2 sets of similar schools"
            });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await ComparatorSetDetails.ShouldBeVisible();
    }

    public async Task<HomePage> ClickOnSchoolName()
    {
        await SchoolLinksInCharts.First.Click();
        return new HomePage(page);
    }

    public async Task<HomePage> EnterOnSchoolName()
    {
        await SchoolLinksInCharts.First.FocusAsync();
        await page.Keyboard.PressAsync("Enter");

        return new HomePage(page);
    }

    public async Task AssertChartsVisible(IEnumerable<string> expectedTitles)
    {
        var titles = expectedTitles.ToArray();

        await AssertChartCount(titles.Length);
        await AssertVisibleCharts(titles);
    }
    
    public async Task SelectSubCategories(List<SubCategoryNames> categoriesToSelect)
    {
        await EnsureSubCategoriesAreExpanded();
        foreach (var category in categoriesToSelect)
        {
            var checkbox = SubCategorySelector(category);

            if (!await checkbox.IsCheckedAsync())
            {
                await checkbox.Click();
            }
        }
    }
    
    public async Task ClickApplyFilter()
    {
        await ApplyFilterButton.Click();
    }
    
    public async Task AssertFilterCount(string expectedCount)
    {
        await AppliedFiltersCount.TextEqual(expectedCount);
    }

    private async Task EnsureSubCategoriesAreExpanded()
    {
        if (await SubCategoriesAccordionSectionToggle.TextContentAsync() == "Show")
        {
            await SubCategoriesAccordionSectionToggle.Click();
        }
    }

    private async Task AssertVisibleCharts(IEnumerable<string> expectedTitles)
    {
        foreach (var title in expectedTitles)
        {
            await ChartContainer(title).ShouldBeVisible();
        }
    }

    private async Task AssertChartCount(int expectedCount)
    {
        var count = await ChartContainers.CountAsync();
        Assert.Equal(expectedCount, count);
    }

    private ILocator SubCategorySelector(SubCategoryNames subCategory)
    {
        var categoryName = subCategory switch
        {
            SubCategoryNames.Connectivity => ConnectivitySubCategoryCheckbox,
            SubCategoryNames.ITSupport => ITSupportSubCategoryCheckBox,
            SubCategoryNames.LaptopsDesktopsTablets => LaptopDesktopSubCategoryCheckbox,
            _ => throw new ArgumentOutOfRangeException(nameof(subCategory))

        };
        return categoryName;
    }
    
}