using Microsoft.Playwright;
using Xunit;
namespace Web.E2ETests.Pages.Trust;

public class TrustForecastPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible().ShouldHaveText("Forecast and risks"); ;
    }

    public async Task IsForbidden()
    {
        await PageH1Heading.ShouldBeVisible().ShouldHaveText("Access denied");
    }
}