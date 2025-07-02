using AngleSharp.Html.Dom;
using AutoFixture;
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

    private async Task<(IHtmlDocument page, Banner? banner)> SetupNavigateInitPage(bool showBanner = false)
    {
        var banner = showBanner
            ? Fixture.Create<Banner>()
            : null;

        var page = await Client
            .SetupBanner(banner)
            .Navigate(Paths.ServiceHome);

        return (page, banner);
    }
}