using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeeds(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority);
    }

    [Fact]
    public async Task CanNavigateToStartBenchmarking()
    {
        var (page, authority) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Start benchmarking");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToNationalRankings()
    {
        var (page, authority) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "View national rankings");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsNationalRankings(authority.Code).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToHistoricData()
    {
        var (page, authority) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "View historic data");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsHistoricData(authority.Code).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, LocalAuthority authority)> SetupNavigateInitPage()
    {
        var authority = Fixture.Build<LocalAuthority>()
            .Create();

        Assert.NotNull(authority.Name);

        var page = await Client.SetupEstablishment(authority, [])
            .SetupInsights()
            .Navigate(Paths.LocalAuthorityHighNeeds(authority.Code));

        return (page, authority);
    }

    private static void AssertPageLayout(IHtmlDocument page, LocalAuthority authority)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeeds(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "High needs benchmarking - Financial Benchmarking and Insights Tool - GOV.UK", "High needs benchmarking");
    }
}