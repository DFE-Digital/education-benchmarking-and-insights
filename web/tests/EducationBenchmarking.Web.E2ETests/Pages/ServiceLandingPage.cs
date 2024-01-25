using EducationBenchmarking.Web.E2ETests.Hooks;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class ServiceLandingPage(PageHook page)
{
    private readonly IPage _page = page.Current;

    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator StartNowCta => _page.Locator(":text('Start now')");

    public async Task ClickStartNow()
    {
        await StartNowCta.ClickAsync();
    }

    public async Task GoToPage()
    {
        await _page.GotoAsync($"{Config.BaseUrl}");
    }
}