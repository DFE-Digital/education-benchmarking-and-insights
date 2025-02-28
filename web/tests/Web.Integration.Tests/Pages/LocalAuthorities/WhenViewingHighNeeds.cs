using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeeds(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(null)]
    [InlineData(5)]
    public async Task CanDisplay(int? nationalRankings = null)
    {
        var (page, authority, rankings) = await SetupNavigateInitPage(nationalRankings);

        AssertPageLayout(page, authority, rankings);
    }

    [Fact]
    public async Task CanNavigateToStartBenchmarking()
    {
        var (page, authority, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Start benchmarking");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToNationalRankings()
    {
        var (page, authority, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "View national rankings");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsNationalRankings(authority.Code).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToHistoricData()
    {
        var (page, authority, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "View historic data");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsHistoricData(authority.Code).ToAbsolute());
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

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    private async Task<(IHtmlDocument page, LocalAuthority authority, LocalAuthorityRank[] rankings)> SetupNavigateInitPage(int? nationalRankings = null)
    {
        var authority = Fixture.Build<LocalAuthority>()
            .Create();

        var rankings = Fixture.Build<LocalAuthorityRank>()
            .CreateMany(nationalRankings ?? 0)
            .OrderBy(r => r.Name)
            .ToArray();
        if (rankings.FirstOrDefault() != null)
        {
            rankings.First().Code = authority.Code;
        }

        var ranking = Fixture.Build<LocalAuthorityRanking>()
            .With(r => r.Ranking, rankings)
            .Create();

        Assert.NotNull(authority.Name);

        var page = await Client.SetupEstablishment(authority, ranking)
            .SetupInsights()
            .Navigate(Paths.LocalAuthorityHighNeeds(authority.Code));

        return (page, authority, rankings);
    }

    private static void AssertPageLayout(IHtmlDocument page, LocalAuthority authority, LocalAuthorityRank[] rankings)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeeds(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "High needs benchmarking - Financial Benchmarking and Insights Tool - GOV.UK", "High needs benchmarking");

        var cards = page.QuerySelectorAll(".govuk-summary-card");

        var nationalRankingCard = cards.FirstOrDefault(c => c.TextContent.Contains("National Ranking"));
        Assert.NotNull(nationalRankingCard);
        if (rankings.Length == 0)
        {
            // todo: part of #251215
        }
        else
        {
            var table = nationalRankingCard.QuerySelector("table");
            Assert.NotNull(table);

            var headerRow = table.QuerySelector("thead > tr");
            Assert.NotNull(headerRow);
            DocumentAssert.AssertNodeText(headerRow, "Local authority  Spend as percentage of budget");

            var bodyRows = table.QuerySelectorAll("tbody > tr");
            Assert.Equal(rankings.Length, bodyRows.Length);
            for (var i = 0; i < rankings.Length; i++)
            {
                var ranking = rankings.ElementAt(i);
                DocumentAssert.AssertNodeText(bodyRows.ElementAt(i), $"{ranking.Rank}.  {ranking.Name}  {ranking.Value?.ToString("#.#")}%");
            }
        }
    }
}