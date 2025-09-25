using Microsoft.Playwright;
using Web.E2ETests.Pages.Trust.Comparators;

namespace Web.E2ETests.Pages.Trust.Benchmarking;

public class BenchmarkItSpendingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Benchmark your IT spending"
        });
    private ILocator ViewComparatorSetLink => page.Locator("a[data-test-id='comparators-link']");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await ViewComparatorSetLink.ShouldBeVisible();
    }

    public async Task<ViewComparatorsPage> ClickViewComparatorSetLink()
    {
        await ViewComparatorSetLink.ClickAsync();
        return new ViewComparatorsPage(page);
    }
}