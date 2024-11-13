using Microsoft.Playwright;
using Xunit;
namespace Web.E2ETests.Pages.Trust;

public class SpendingCostsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator HighPriorityHeading => PriorityHeading("High priority");
    private ILocator MediumPriorityHeading => PriorityHeading("Medium priority");
    private ILocator LowPriorityHeading => PriorityHeading("Low priority");
    private ILocator Filters => page.Locator(Selectors.Aside);
    private ILocator PriorityHeading(string priority) => page.Locator(Selectors.H2, new PageLocatorOptions
    {
        HasText = priority
    });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await HighPriorityHeading.ShouldBeVisible();
        await MediumPriorityHeading.ShouldBeVisible();
        await LowPriorityHeading.ShouldBeVisible();
        await Filters.ShouldBeVisible();
    }

    public async Task AssertCategoryPriority(string priority, string category, string commentary)
    {
        var section = page.Locator($"#{priority.ToLower().Replace(" ", "-")}-section");
        await section.ShouldBeVisible();
        var heading = section.Locator("> div > h3", new LocatorLocatorOptions
        {
            HasText = category
        });
        await heading.ShouldBeVisible();
        var text = await heading.Locator("~ .top-categories").InnerTextAsync();
        Assert.Equal($"{priority} {commentary}", text);
    }
}