using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class ServiceLandingPage
{
    private readonly IPage _page;

    public ServiceLandingPage(IPage page)
    {
        _page = page;
    }

    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator StartNowCta => _page.Locator(":text('Start now')");

    public async Task ClickStartNow()
    {
        await StartNowCta.ClickAsync();
    }
}