using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeedsNationalRanking(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityHighNeedsNationalRankings(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsNationalRankings(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHighNeedsNationalRankings(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsNationalRankings(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    private async Task<(
        IHtmlDocument page,
        LocalAuthority authority)> SetupNavigateInitPage()
    {
        var authority = Fixture.Build<LocalAuthority>()
            .With(a => a.Code, "123")
            .Create();

        var ranking = Fixture.Build<LocalAuthorityRanking>()
            .Create();

        var page = await Client.SetupEstablishment(authority)
            .SetupInsights()
            .Navigate(Paths.LocalAuthorityHighNeedsNationalRankings(authority.Code));

        return (page, authority);
    }

    private static void AssertPageLayout(IHtmlDocument page, LocalAuthority authority)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsNationalRankings(authority.Code).ToAbsolute());

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "High needs national view - Financial Benchmarking and Insights Tool - GOV.UK", "High needs national view");

        var nationalRankingComponent = page.GetElementById("la-national-rank");
        Assert.NotNull(nationalRankingComponent);
    }
}