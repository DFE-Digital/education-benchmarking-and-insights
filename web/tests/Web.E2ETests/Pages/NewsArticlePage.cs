using Microsoft.Playwright;

namespace Web.E2ETests.Pages;

public class NewsArticlePage(IPage page) : BasePage(page)
{
    public override async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task HasHeading(string heading)
    {
        await PageH1Heading.ShouldHaveText(heading);
    }
}