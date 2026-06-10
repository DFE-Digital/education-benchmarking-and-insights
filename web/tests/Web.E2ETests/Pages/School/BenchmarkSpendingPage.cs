using Microsoft.Playwright;
using Web.E2ETests.Pages.School.Comparators;

namespace Web.E2ETests.Pages.School;

public class BenchmarkSpendingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ViewAsTableRadio => page.Locator(Selectors.ViewAsTable);
    private ILocator ViewAsChartRadio => page.Locator(Selectors.ViewAsChart);
    private ILocator ResultAsSelector => page.Locator(Selectors.BenchmarkSpendingResultAs);
    private ILocator ApplyButton => page.Locator(Selectors.Button, new PageLocatorOptions { HasText = "Apply" });
    private ILocator SaveImagesButton => page.Locator(Selectors.Button, new PageLocatorOptions { HasText = "Save chart images" });

    private ILocator SaveImagesModal => page.Locator(Selectors.Modal);

    private ILocator ChartContainers => page.Locator(Selectors.SsrChartContainer);
    private ILocator SchoolLinksInCharts => page.Locator(Selectors.SsrOrgNamesLinksInCharts);
    private ILocator ChartBars => page.Locator(Selectors.BenchmarkSpendingChartBars);
    private ILocator ChartTooltip => page.Locator(Selectors.EnhancementSchoolChartTooltip);

    private ILocator ComparatorSetDetails =>
        page.Locator(Selectors.GovLink,
            new PageLocatorOptions
            {
                HasText = "view the 2 sets of similar schools we've chosen"
            });

    private ILocator MissingComparatorSetMessage => page.Locator(Selectors.MissingComparatorSetMessage);

    private ILocator SchoolPerformanceCheckbox(string banding) => page.Locator(Selectors.GovCheckboxLabel,
        new PageLocatorOptions
        {
            HasText = banding
        });
    private ILocator ChartContainer(string chartName) =>
        page.Locator($"section[data-testid='subcategory-section']:has-text(\"{chartName}\")");

    public async Task IsDisplayed(bool isMissingComparatorSet = false)
    {
        await PageH1Heading.ShouldBeVisible();

        if (!isMissingComparatorSet)
        {
            //await ChartContainers.First.ShouldBeVisible();
            await SaveImagesButton.ShouldBeVisible();
            await ViewAsTableRadio.ShouldBeVisible();
            await ViewAsChartRadio.ShouldBeVisible();
            await ComparatorSetDetails.ShouldBeVisible();

            return;
        }

        await MissingComparatorSetMessage.ShouldContainText(
            "There is not enough information available to create a set of similar schools.");
    }

    public async Task ClickViewAsTable()
    {
        await ViewAsTableRadio.Click();
        await ClickApplyButton();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task SelectResultAs(string resultValue)
    {
        await ResultAsSelector.SelectOption(resultValue);
    }

    public async Task ClickApplyButton()
    {
        await ApplyButton.Nth(1).Click();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task IsTableDataForChartDisplayed(string chartName, List<List<string>> expectedData)
    {
        var table = ChartContainer(chartName).Locator("table.govuk-table");
        await table.ShouldBeVisible();
        await table.ShouldHaveTableContent(expectedData, true);
    }

    public async Task AreComparisonChartsAndTablesDisplayed(bool displayed = true)
    {
        var locator = page.Locator(Selectors.BenchmarkSpendingChartsAndTables);
        if (displayed)
        {
            var count = await locator.CountAsync();
            Xunit.Assert.True(count > 0, "Expected at least one chart container to be visible");
        }
        else
        {
            var count = await locator.CountAsync();
            Xunit.Assert.Equal(0, count);
        }
    }

    public async Task IsSchoolDetailsPopUpVisible()
    {
        await ChartTooltip.ShouldBeVisible();
    }

    public async Task HoverOnGraphBar()
    {
        await ChartBars.First.HoverAsync();
    }

    public async Task<HomePage> ClickSchoolName()
    {
        await SchoolLinksInCharts.First.Click();
        return new HomePage(page);
    }

    public async Task TabToSchoolName()
    {
        await ResultAsSelector.FocusAsync();
        await page.Keyboard.PressAsync("Tab"); // Apply button
        await page.Keyboard.PressAsync("Tab"); // First school link
    }

    public async Task AssertSchoolNameFocused()
    {
        await Assertions.Expect(SchoolLinksInCharts.First).ToBeFocusedAsync();
    }

    public async Task<HomePage> PressEnterKey()
    {
        await page.Keyboard.PressAsync(Keyboard.EnterKey);
        return new HomePage(page);
    }

    public async Task TooltipIsDisplayed()
    {
        await ChartTooltip.ShouldBeVisible();
    }

    public async Task<ComparatorsPage> ClickComparatorSetDetails()
    {
        await ComparatorSetDetails.Click();
        return new ComparatorsPage(page);
    }

    public async Task IsTableDataForTooltipDisplayed(List<List<string>> expectedData)
    {
        await TooltipIsDisplayed();
        var table = ChartTooltip.Locator("table");
        await table.ShouldHaveTableContent(expectedData, true);
    }

    public async Task IsSaveImagesButtonDisplayed()
    {
        await SaveImagesButton.ShouldBeVisible();
    }

    public async Task ClickSaveImagesButton()
    {
        await SaveImagesButton.ClickAsync();
    }

    public async Task IsSaveImagesModalDisplayed()
    {
        await SaveImagesModal.ShouldBeVisible();
    }

    public async Task ClickSchoolPerformanceCheckbox(string banding)
    {
        var checkbox = SchoolPerformanceCheckbox(banding);
        await checkbox.ShouldBeVisible();
        await checkbox.Click();
        await ClickApplyButton();
    }

    public async Task ApplyFilters()
    {
        await ApplyButton.Click();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
}
