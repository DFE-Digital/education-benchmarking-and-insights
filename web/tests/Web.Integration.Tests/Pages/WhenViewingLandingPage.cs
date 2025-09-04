using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain.Content;
using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingLandingPage(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplay(bool showBanner)
    {
        var (page, banner) = await SetupNavigateInitPage(showBanner);

        DocumentAssert.AssertPageUrl(page, Paths.ServiceHome.ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Home - Financial Benchmarking and Insights Tool - GOV.UK", "Financial Benchmarking and Insights Tool");

        var startButton = page.GetElementsByClassName("govuk-button--start").FirstOrDefault();
        Assert.NotNull(startButton);
        DocumentAssert.PrimaryCta(startButton, "Start now", Paths.FindOrganisation);

        DocumentAssert.Banner(page, banner);
    }

    [Fact]
    public async Task CanNavigateToFindOrganisation()
    {
        var (page, _) = await SetupNavigateInitPage();
        var startButton = page.GetElementsByClassName("govuk-button--start").FirstOrDefault();
        Assert.NotNull(startButton);

        page = await Client.Follow(startButton);

        DocumentAssert.AssertPageUrl(page, Paths.FindOrganisation.ToAbsolute());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanNavigateToNews(bool showNews)
    {
        var (page, _) = await SetupNavigateInitPage(showNews: showNews);
        var footerLinks = page.QuerySelectorAll("footer ul > li > a");
        Assert.NotNull(footerLinks);

        if (showNews)
        {
            Assert.Equal(6, footerLinks.Length);
            page = await Client.Follow(footerLinks.Last());
            DocumentAssert.AssertPageUrl(page, Paths.News().ToAbsolute());
        }
        else
        {
            Assert.Equal(5, footerLinks.Length);
        }
    }

    private async Task<(IHtmlDocument page, Banner? banner)> SetupNavigateInitPage(bool showBanner = false, bool showNews = false)
    {
        var banner = showBanner
            ? Fixture.Create<Banner>()
            : null;

        var page = await Client
            .SetupDisableFeatureFlags(showNews ? [] : [FeatureFlags.News])
            .SetupBanner(banner)
            .Navigate(Paths.ServiceHome);

        return (page, banner);
    }
}