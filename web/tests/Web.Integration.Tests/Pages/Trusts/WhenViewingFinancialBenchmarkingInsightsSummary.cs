using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Extensions;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingFinancialBenchmarkingInsightsSummary(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly Dictionary<int, string> AllCostCategories = new()
    {
        { 1, Category.TeachingStaff },
        { 2, Category.NonEducationalSupportStaff },
        { 3, Category.EducationalSupplies },
        { 4, Category.EducationalIct },
        { 5, Category.PremisesStaffServices },
        { 6, Category.Utilities },
        { 7, Category.AdministrativeSupplies },
        { 8, Category.CateringStaffServices }
    };

    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust, _, _, _) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
    }

    [Fact]
    public async Task CanDisplayIntroductionSection()
    {
        var (page, trust, _, _, _) = await SetupNavigateInitPage();
        AssertIntroductionSection(page, trust);
    }

    [Fact]
    public async Task CanDisplayKeyInformationSection()
    {
        var (page, trust, balance, _, _) = await SetupNavigateInitPage();
        AssertKeyInformationSection(page, trust, balance);
    }

    [Fact]
    public async Task CanDisplaySpendingPrioritiesSection()
    {
        var (page, trust, _, ragRatings, schools) = await SetupNavigateInitPage();
        AssertSpendingPrioritiesSection(page, trust, ragRatings, schools);
    }
    
    public async Task CanDisplayNextStepsSection()
    {
        var (page, trust, _) = await SetupNavigateInitPage();
        AssertNextStepsSection(page, trust);
    }

    private async Task<(IHtmlDocument page, Trust trust, TrustBalance balance, RagRating[] ratings, TrustSchool[] schools)> SetupNavigateInitPage()
    {
        var random = new Random();
        const string companyNumber = "12345678";

        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, companyNumber)
            .Create();

        var schools = Fixture.Build<TrustSchool>()
                .With(x => x.OverallPhase, () => OverallPhaseTypes.All.ElementAt(random.Next(0, OverallPhaseTypes.All.Length - 1)))
                .CreateMany(20).ToArray();

        var values = AllCostCategories.Select(c => c.Value);
        var queue = new Queue<string>();
        var ratings = schools.SelectMany(s =>
            {
                return Fixture.Build<RagRating>()
                    .OmitAutoProperties()
                    .With(x => x.Category, () =>
                    {
                        if (queue.Count == 0)
                        {
                            foreach (var value in values)
                            {
                                queue.Enqueue(value);
                            }
                        }

                        return queue.Dequeue();
                    })
                    .With(x => x.RAG, () => Lookups.StatusPriorityMap.Keys.ElementAt(random.Next(0, Lookups.StatusPriorityMap.Keys.Count - 1)))
                    .With(x => x.URN, s.URN)
                    .With(x => x.Value, random.Next(1000, 2000))
                    .CreateMany(values.Count());
            }).ToArray();

        var balance = Fixture.Build<TrustBalance>()
            .With(x => x.CompanyNumber, trust.CompanyNumber)
            .Create();

        var page = await Client
            .SetupEstablishment(trust, schools)
            .SetupInsights()
            .SetupMetricRagRating(ratings)
            .SetupBalance(trust, balance)
            .Navigate(Paths.TrustFinancialBenchmarkingInsightsSummary(trust.CompanyNumber));

        return (page, trust, balance, ratings, schools);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        DocumentAssert.AssertPageUrl(page, Paths.TrustFinancialBenchmarkingInsightsSummary(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Summary - Financial Benchmarking and Insights Tool - GOV.UK", trust.TrustName!);
    }

    private static void AssertIntroductionSection(IHtmlDocument page, Trust trust)
    {
        var introductionSection = page.QuerySelector("section#introduction");
        Assert.NotNull(introductionSection);

        var link = introductionSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.TrustHome(trust.CompanyNumber), link.GetAttribute("href"));
    }

    private static void AssertKeyInformationSection(IHtmlDocument page, Trust trust, TrustBalance balance)
    {
        var keyInformationSection = page.QuerySelector("section#key-information-section");
        Assert.NotNull(keyInformationSection);
        DocumentAssert.Heading2(keyInformationSection, "Key information about this trust");

        var headlineFiguresTexts = keyInformationSection.ChildNodes
            .QuerySelectorAll(".app-headline-figures.app-headline-summary")
            .Select(e => string.Join(" ", e.ChildNodes.Select(n => n.TextContent.Trim())).Trim());
        Assert.Equal([
            $"In year balance  £{balance.InYearBalance}",
            $"Revenue reserve  £{balance.RevenueReserve}",
            $"Academies  {trust.Schools.Length}"
        ], headlineFiguresTexts);

        var link = keyInformationSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.TrustHome(trust.CompanyNumber), link.GetAttribute("href"));
    }

    private static void AssertSpendingPrioritiesSection(IHtmlDocument page, Trust trust, RagRating[] ragRatings, TrustSchool[] schools)
    {
        var spendingPrioritiesSection = page.QuerySelector("section#spending-priorities-section");
        Assert.NotNull(spendingPrioritiesSection);
        DocumentAssert.Heading2(spendingPrioritiesSection, "Spending priorities at this trust");

        var cards = spendingPrioritiesSection.QuerySelectorAll(".govuk-summary-card");
        Assert.NotNull(cards);
        Assert.Equal(2, cards.Length);

        var spendingPriorities = ragRatings
            .GroupBy(x => (x.RAG, x.Category))
            .Select(x => (x.Key.RAG, x.Key.Category, Count: x.Count()))
            .GroupBy(x => x.Category)
            .Select(x => new
            {
                Category = x.Key,
                Red = x.Where(w => w.RAG == "red").Select(r => r.Count).SingleOrDefault(),
                Amber = x.Where(w => w.RAG == "amber").Select(a => a.Count).SingleOrDefault(),
                Green = x.Where(w => w.RAG == "green").Select(g => g.Count).SingleOrDefault()
            })
            .OrderByDescending(o => o.Red)
            .ThenByDescending(o => o.Amber)
            .ThenBy(o => o.Category)
            .Where(o => o.Red > 0 || o.Amber > 0)
            .Take(2)
            .ToArray();

        AssertSpendingPrioritiesCard(spendingPriorities.ElementAt(0).Category, spendingPriorities.ElementAt(0).Red, spendingPriorities.ElementAt(0).Amber, cards.ElementAt(0), ragRatings, schools.Length);
        AssertSpendingPrioritiesCard(spendingPriorities.ElementAt(1).Category, spendingPriorities.ElementAt(1).Red, spendingPriorities.ElementAt(1).Amber, cards.ElementAt(1), ragRatings, schools.Length);

        var link = spendingPrioritiesSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.TrustSpending(trust.CompanyNumber), link.GetAttribute("href"));
    }

    private static void AssertSpendingPrioritiesCard(string? category, int? red, int? amber, IElement card, RagRating[] ragRatings, int schools)
    {
        Assert.Equal(category, card.QuerySelector(".govuk-summary-card__title")?.TextContent.Trim());

        Assert.Equal(
            $"{(red > 0 ? red : amber)} out of {schools} schools in the {(red > 0 ? "high" : "medium")} priority range.",
            card.QuerySelector(".priority-wrapper")?.TextContent.Trim().Replace(StringExtensions.WhitespaceRegex(), " "));

        var values = ragRatings.Where(r => r.Category == category).OrderBy(r => r.Value).Select(r => r.Value).ToArray();
        var value = values.LastOrDefault();
        var lowestHighest = "Highest";
        if (
            new[]
            {
                Category.TeachingStaff,
                Category.EducationalSupplies,
                Category.EducationalIct
            }.Contains(category))
        {
            lowestHighest = "Lowest";
            value = values.FirstOrDefault();
        }

        var unit = new[]
        {
            Category.PremisesStaffServices,
            Category.Utilities
        }.Contains(category)
            ? "square metre"
            : "pupil";
        Assert.Equal(
            $"{lowestHighest} spend in this cost category {value.ToCurrency()} per {unit}",
            card.QuerySelector(".panel")?.TextContent.Trim().Replace(StringExtensions.WhitespaceRegex(), " "));
    }
    
    private static void AssertNextStepsSection(IHtmlDocument page, Trust trust)
    {
        var nextStepsSection = page.QuerySelector("section#next-steps-section");
        Assert.NotNull(nextStepsSection);
        DocumentAssert.Heading2(nextStepsSection, "Next steps");

        var link = nextStepsSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.TrustHome(trust.CompanyNumber), link.GetAttribute("href"));
    }
}