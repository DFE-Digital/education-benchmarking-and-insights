using System.Net;
using AutoFixture;
using Web.App.Domain.Content;
using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingNews(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var news = Fixture.Build<News>().CreateMany().ToArray();

        var page = await Client
            .SetupNews(news)
            .Navigate(Paths.News());

        DocumentAssert.AssertPageUrl(page, Paths.News().ToAbsolute());
        DocumentAssert.TitleAndH1(page, "News - Financial Benchmarking and Insights Tool - GOV.UK", "News");

        var items = page.QuerySelectorAll(".govuk-list.govuk-list--result > li");
        Assert.NotNull(items);
        Assert.Equal(news.Length, items.Length);

        for (var i = 0; i < items.Length; i++)
        {
            var item = items.ElementAt(i);
            var article = news.ElementAt(i);

            var link = item.QuerySelector("a");
            Assert.NotNull(link);

            Assert.Equal(Paths.News(article.Slug), link.GetAttribute("href"));
            Assert.Equal(article.Title, link.TextContent);
        }
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        var page = await Client.SetupNewsWithException()
            .Navigate(Paths.News());

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.News().ToAbsolute(), HttpStatusCode.InternalServerError);
    }
}