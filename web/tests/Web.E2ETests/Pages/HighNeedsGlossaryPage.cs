using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages;

public class HighNeedsGlossaryPage(IPage page) : BasePage(page)
{
    private readonly IPage _page = page;

    public override async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task AssertHighNeedsGlossary(int count)
    {
        var glossaryRows = _page.Locator("[id^=high-needs-glossary-]");
        var actualCount = await glossaryRows.CountAsync();
        Assert.Equal(count, actualCount);
    }

    public async Task AssertGeneralGlossary(int count)
    {
        var glossaryRows = _page.Locator("[id^=general-glossary-]");
        var actualCount = await glossaryRows.CountAsync();
        Assert.Equal(count, actualCount);
    }
}