using Microsoft.Playwright;
namespace Web.E2ETests.Pages;

public class HomePage(IPage page) : BasePage(page)
{
    public override async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }
}