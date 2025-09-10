using System.Net;
using Web.App.Domain.Content;
using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingNewsArticle(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        const string slug = nameof(slug);
        const string title = nameof(title);
        const string heading = nameof(heading);
        var news = new News
        {
            Title = title,
            Slug = slug,
            Body = $"""
                    # {heading}

                    Content
                    """
        };

        var page = await Client
            .SetupNewsArticle(news)
            .Navigate(Paths.News(slug));

        DocumentAssert.AssertPageUrl(page, Paths.News(slug).ToAbsolute());
        DocumentAssert.TitleAndH1(page, $"{title} - Financial Benchmarking and Insights Tool - GOV.UK", heading);

        var content = page.QuerySelector(".markdown");
        Assert.NotNull(content);

        const string expected = $"""
                                 <h1 class="govuk-heading-l">{heading}</h1>
                                 <p class="govuk-body">Content</p>
                                 """;
        Assert.Equal(expected.Replace(Environment.NewLine, "\n"), content.InnerHtml.Trim());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string slug = nameof(slug);
        var page = await Client.SetupNewsArticle(null)
            .Navigate(Paths.News(slug));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.News(slug).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string slug = nameof(slug);
        var page = await Client.SetupNewsWithException()
            .Navigate(Paths.News(slug));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.News(slug).ToAbsolute(), HttpStatusCode.InternalServerError);
    }
}