using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages;

public class NewsPage(IPage page) : BasePage(page)
{
    private ILocator NewsArticleList => Page.Locator(Selectors.GovListResult);

    public override async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await NewsArticleList.ShouldBeVisible();
    }

    public async Task HasArticleCount(int expected)
    {
        var actual = await NewsArticleList.Locator("> li").CountAsync();
        Assert.Equal(expected, actual);
    }

    public async Task<NewsArticlePage> ClickNewsArticleLink(string title)
    {
        var link = NewsArticleList.Locator("> li").Locator("a", new LocatorLocatorOptions
        {
            HasText = title
        });
        await link.ShouldBeVisible();
        await link.ClickAsync();
        return new NewsArticlePage(Page);
    }
}