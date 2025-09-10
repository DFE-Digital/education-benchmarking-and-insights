using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Domain.NonFinancial;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeeds(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(null, null, false, false)]
    [InlineData(5, 5, true, false)]
    [InlineData(5, 5, true, true)]
    public async Task CanDisplay(int? nationalRankings = null, int? historyYears = null, bool headlines = true, bool notInRanking = false)
    {
        var (page, authority, rankings, history, highNeeds, plans) =
            await SetupNavigateInitPage(nationalRankings, historyYears, headlines, notInRanking);

        AssertPageLayout(page, authority, rankings, history, highNeeds, plans);
    }

    [Fact]
    public async Task CanNavigateToStartBenchmarking()
    {
        var (page, authority, _, _, _, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Start benchmarking");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData(5, true)]
    public async Task CanNavigateToNationalRankingsIfAvailable(int? nationalRankings = null, bool expectedButtonVisible = false)
    {
        var (page, authority, _, _, _, _) = await SetupNavigateInitPage(nationalRankings);

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "View full national view");
        if (expectedButtonVisible)
        {
            Assert.NotNull(anchor);
            page = await Client.Follow(anchor);
            DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsNationalRankings(authority.Code).ToAbsolute());
            return;
        }

        Assert.Null(anchor);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData(5, true)]
    public async Task CanNavigateToHistoricDataIfAvailable(int? historyYears = null, bool expectedButtonVisible = false)
    {
        var (page, authority, _, _, _, _) = await SetupNavigateInitPage(historyYears: historyYears);

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "View full historic data");
        if (expectedButtonVisible)
        {
            Assert.NotNull(anchor);
            page = await Client.Follow(anchor);
            DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsHistoricData(authority.Code).ToAbsolute());
            return;
        }

        Assert.Null(anchor);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityHighNeedsHistoricData(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsHistoricData(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHighNeedsHistoricData(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsHistoricData(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    private async Task<(
        IHtmlDocument page,
        LocalAuthority authority,
        LocalAuthorityRank[] rankings,
        HighNeedsHistory<HighNeedsYear> history,
        LocalAuthority<HighNeeds>? highNeeds,
        LocalAuthorityNumberOfPlans? plans)> SetupNavigateInitPage(
        int? nationalRankings = null,
        int? historyYears = null,
        bool headlines = true,
        bool notInRanking = false)
    {
        var authority = Fixture.Build<LocalAuthority>()
            .With(a => a.Code, "123")
            .Create();

        var rankings = Fixture.Build<LocalAuthorityRank>()
            .CreateMany(nationalRankings ?? 0)
            .OrderBy(r => r.Name)
            .ToArray();
        if (rankings.FirstOrDefault() != null && !notInRanking)
        {
            rankings.First().Code = authority.Code;
        }

        var ranking = Fixture.Build<LocalAuthorityRanking>()
            .With(r => r.Ranking, rankings)
            .Create();

        const int startYear = 2021;
        var endYear = startYear + historyYears.GetValueOrDefault() - 1;
        var history = new HighNeedsHistory<HighNeedsYear>();
        if (historyYears != null)
        {
            var outturn = new List<HighNeedsYear>();
            var budget = new List<HighNeedsYear>();
            for (var year = startYear; year <= endYear; year++)
            {
                outturn.Add(Fixture
                    .Build<HighNeedsYear>()
                    .With(h => h.Year, year)
                    .With(h => h.Code, authority.Code)
                    .Create());
                budget.Add(Fixture
                    .Build<HighNeedsYear>()
                    .With(h => h.Year, year)
                    .With(h => h.Code, authority.Code)
                    .Create());
            }

            history.StartYear = startYear;
            history.EndYear = endYear;
            history.Outturn = outturn.ToArray();
            history.Budget = budget.ToArray();
        }

        Assert.NotNull(authority.Name);

        var statisticalNeighbours = Fixture.Build<LocalAuthorityStatisticalNeighbours>().Create();
        var authorities = Fixture.Build<LocalAuthority>().CreateMany().ToArray();

        var highNeeds = headlines ? Fixture.Create<LocalAuthority<HighNeeds>>() : null;
        var plans = headlines ? Fixture.Create<LocalAuthorityNumberOfPlans>() : null;

        var page = await Client.SetupEstablishment(authority, ranking, statisticalNeighbours, authorities)
            .SetupHighNeeds(highNeeds == null ? null : [highNeeds], history)
            .SetupEducationHealthCarePlans(plans == null ? null : [plans], null)
            .SetupInsights()
            .SetupLocalAuthoritiesComparators(authority.Code!, [])
            .Navigate(Paths.LocalAuthorityHighNeedsDashboard(authority.Code));

        return (page, authority, rankings, history, highNeeds, plans);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        LocalAuthority authority,
        LocalAuthorityRank[] rankings,
        HighNeedsHistory<HighNeedsYear> history,
        LocalAuthority<HighNeeds>? highNeeds,
        LocalAuthorityNumberOfPlans? plans)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsDashboard(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "High needs benchmarking overview - Financial Benchmarking and Insights Tool - GOV.UK", "High needs benchmarking overview");

        var cards = page.QuerySelectorAll(".govuk-summary-card");

        var headlinesCard = cards.FirstOrDefault(c => c.TextContent.Contains("Total number of EHC plans"));
        AssertHeadlinesCard(headlinesCard, highNeeds, plans);

        var nationalRankingCard = cards.FirstOrDefault(c => c.TextContent.Contains("National view"));
        AssertNationalRankingCard(nationalRankingCard, rankings, authority);

        var budgetSpendHistoryCard = cards.FirstOrDefault(c => c.TextContent.Contains("Historical spending"));
        AssertBudgetSpendHistoryCard(budgetSpendHistoryCard, history);
    }

    private static void AssertHeadlinesCard(IElement? headlinesCard, LocalAuthority<HighNeeds>? highNeeds, LocalAuthorityNumberOfPlans? plans)
    {
        Assert.NotNull(headlinesCard);

        if (highNeeds == null || plans == null)
        {
            var content = headlinesCard.QuerySelector(".govuk-summary-card__content");
            DocumentAssert.AssertNodeText(content, "!\n    \n        Warning\n        Headlines could not be displayed.");
        }
        else
        {
            var table = headlinesCard.QuerySelector("table");
            Assert.NotNull(table);

            var bodyRows = table.QuerySelectorAll("tbody > tr");
            Assert.Equal(1, bodyRows.Length);
            DocumentAssert.AssertNodeText(bodyRows.ElementAt(0), $"Total number of EHC plans  {plans.Total ?? 0:N0}");
        }
    }

    private static void AssertNationalRankingCard(IElement? nationalRankingCard, LocalAuthorityRank[] rankings, LocalAuthority authority)
    {
        Assert.NotNull(nationalRankingCard);

        if (rankings.Length == 0)
        {
            var content = nationalRankingCard.QuerySelector(".govuk-summary-card__content");
            DocumentAssert.AssertNodeText(content, "!\n    \n        Warning\n        National view could not be displayed.");
        }
        else
        {
            if (rankings.All(r => r.Code != authority.Code))
            {
                var warning = nationalRankingCard.QuerySelector(".govuk-warning-text");
                DocumentAssert.AssertNodeText(warning, "!  Warning\n            There isn't enough information available to rank the current local authority.");
            }

            var table = nationalRankingCard.QuerySelector("table");
            Assert.NotNull(table);

            var headerRow = table.QuerySelector("thead > tr");
            Assert.NotNull(headerRow);
            DocumentAssert.AssertNodeText(headerRow, "Local authority  Outturn as percentage of budget");

            var bodyRows = table.QuerySelectorAll("tbody > tr");
            Assert.Equal(rankings.Length, bodyRows.Length);
            for (var i = 0; i < bodyRows.Length; i++)
            {
                var ranking = rankings.ElementAt(i);
                DocumentAssert.AssertNodeText(bodyRows.ElementAt(i), $"{ranking.Rank}.  {ranking.Name}  {ranking.Value?.ToString("#.#")}%");
            }
        }
    }

    private static void AssertBudgetSpendHistoryCard(IElement? budgetSpendHistoryCard, HighNeedsHistory<HighNeedsYear> history)
    {
        Assert.NotNull(budgetSpendHistoryCard);

        if (history.Budget == null || history.Outturn == null)
        {
            var content = budgetSpendHistoryCard.QuerySelector(".govuk-summary-card__content");
            DocumentAssert.AssertNodeText(content, "!\n    \n        Warning\n        Budget vs outturn (historical view) could not be displayed.");
        }
        else
        {
            var table = budgetSpendHistoryCard.QuerySelector("table");
            Assert.NotNull(table);

            var headerRow = table.QuerySelector("thead > tr");
            Assert.NotNull(headerRow);
            DocumentAssert.AssertNodeText(headerRow, "Year  Finances  Net position");

            var bodyRows = table.QuerySelectorAll("tbody > tr");
            Assert.Equal(history.EndYear - history.StartYear + 1 ?? 0, bodyRows.Length);
            var year = history.StartYear;
            for (var i = bodyRows.Length - 1; i <= 0; i--)
            {
                var outturn = history.Outturn.Single(o => o.Year == year);
                var budget = history.Budget.Single(o => o.Year == year);
                var outturnValue = outturn.Total;
                var budgetValue = budget.Total;
                var balanceValue = budgetValue - outturnValue;
                DocumentAssert.AssertNodeText(bodyRows.ElementAt(i),
                    $"{year}  Outturn:\n                            {outturnValue?.ToString("C0")}\n                        \n                        \n                            Budget:\n                            {budgetValue?.ToString("C0")}  {balanceValue?.ToString("C0")}");
                year++;
            }
        }
    }
}