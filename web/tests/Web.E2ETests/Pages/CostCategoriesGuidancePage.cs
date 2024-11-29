using Microsoft.Playwright;
using Xunit;
namespace Web.E2ETests.Pages;

public class CostCategoriesGuidancePage(IPage page) : BasePage(page)
{
    private readonly IPage _page = page;

    public override async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task AssertSubCategories(string category, string[] subCategories)
    {
        var categoryRow = _page.Locator($"[id^=sub-categories-{category.ToSlug()}]");
        await categoryRow.IsVisibleAsync();

        var bullets = await categoryRow.Locator("ul li").AllTextContentsAsync();
        Assert.Equal(subCategories, bullets);
    }
}