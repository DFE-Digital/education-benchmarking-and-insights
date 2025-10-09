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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplayPrioritySchoolsSection(bool singleSchoolInTrust)
    {
        var (page, trust, _, ragRatings, schools) = await SetupNavigateInitPage(singleSchoolInTrust);
        AssertPrioritySchoolsSection(page, trust, ragRatings, schools, singleSchoolInTrust);
    }

    [Fact]
    public async Task CanDisplayNextStepsSection()
    {
        var (page, trust, _, _, _) = await SetupNavigateInitPage();
        AssertNextStepsSection(page, trust);
    }

    private async Task<(IHtmlDocument page, Trust trust, TrustBalance balance, RagRating[] ratings, TrustSchool[] schools)> SetupNavigateInitPage(bool singleSchoolInTrust = false)
    {
        var random = new Random();
        const string companyNumber = "12345678";

        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, companyNumber)
            .Create();

        var schools = Fixture.Build<TrustSchool>()
                .With(x => x.OverallPhase, () => OverallPhaseTypes.All.ElementAt(random.Next(0, OverallPhaseTypes.All.Length - 1)))
                .CreateMany(singleSchoolInTrust ? 1 : 20).ToArray();

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

        var lowestHighest = "Highest";
        decimal? value;
        if (
            new[]
            {
                Category.TeachingStaff,
                Category.EducationalSupplies,
                Category.EducationalIct
            }.Contains(category))
        {
            value = ragRatings
                .Where(r => r.Category == category)
                .OrderByDescending(r => r.Value)
                .Select(r => r.Value)
                .FirstOrDefault();
        }
        else
        {
            var highlightRag = ragRatings
                .Where(r => r.Category == category)
                .Where(r => r.Value >= 0)
                .OrderByDescending(r => Math.Abs(r.DiffMedian ?? 0))
                .FirstOrDefault();
            value = highlightRag?.Value;
            if (!(highlightRag?.Value > highlightRag?.Median))
            {
                lowestHighest = "Lowest";
            }
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

    private static void AssertPrioritySchoolsSection(IHtmlDocument page, Trust trust, RagRating[] ragRatings, TrustSchool[] schools, bool singleSchoolInTrust)
    {
        var prioritySchools = ragRatings
            .Where(r => r.Category != Category.Other)
            .GroupBy(r => r.URN)
            .Select(g => new
            {
                Urn = g.Key,
                Name = schools.FirstOrDefault(s => s.URN == g.Key)?.SchoolName,
                Red = g.Count(r => r.RAG == "red"),
                Amber = g.Count(r => r.RAG == "amber"),
                Green = g.Count(r => r.RAG == "green"),
                TopCategories = ragRatings
                    .Where(r => r.Category != Category.Other)
                    .Where(r => r.URN == g.Key)
                    .Select(r => new
                    {
                        Category = r.Category!,
                        Value = r.Value,
                        Unit = Lookups.CategoryUnitMap[r.Category!]
                    })
                    .OrderByDescending(c => c.Value)
                    .Take(2)
                    .ToList()
            })
            .OrderByDescending(s => s.Red)
            .ThenByDescending(s => s.Amber)
            .ThenBy(s => s.Name)
            .Take(2)
            .ToArray();

        var prioritySchoolsSection = page.QuerySelector("section#priority-schools-section");
        Assert.NotNull(prioritySchoolsSection);
        DocumentAssert.Heading2(prioritySchoolsSection, "Spending at schools in this trust");

        var cards = prioritySchoolsSection.QuerySelectorAll(".govuk-summary-card");
        Assert.NotNull(cards);
        Assert.Equal(singleSchoolInTrust ? 1 : 2, cards.Length);

        var highlightText = prioritySchoolsSection.QuerySelector("p.govuk-body")?.TextContent.Trim();
        Assert.NotNull(highlightText);

        Assert.Contains(schools.Length > 1 ? "Key highlights from two schools" : "Key highlights from the school", highlightText);

        for (var i = 0; i < prioritySchools.Length; i++)
        {
            var expected = prioritySchools[i];
            var card = cards[i];

            Assert.NotNull(expected.Name);
            Assert.Contains(expected.Name, card.QuerySelector(".govuk-summary-card__title")?.TextContent.Trim());

            var redCount = card.QuerySelector("[data-testid='priority-red']");
            Assert.NotNull(redCount);
            Assert.Equal($"{expected.Red}", redCount.TextContent.Trim());
            var redLink = redCount.QuerySelector("a.govuk-link");

            var amberCount = card.QuerySelector("[data-testid='priority-amber']");
            Assert.NotNull(amberCount);
            Assert.Equal($"{expected.Amber}", amberCount.TextContent.Trim());
            var amberLink = amberCount.QuerySelector("a.govuk-link");

            var greenCount = card.QuerySelector("[data-testid='priority-green']");
            Assert.NotNull(greenCount);
            Assert.Equal($"{expected.Green}", greenCount.TextContent.Trim());
            var greenLink = greenCount.QuerySelector("a.govuk-link");

            if (expected.Red > 0)
            {
                Assert.NotNull(redLink);
                Assert.Equal($"{Paths.SchoolSpending(expected.Urn)}#high-priority", redLink.GetAttribute("href"));
            }
            else
            {
                Assert.Null(redLink);
            }

            if (expected.Amber > 0)
            {
                Assert.NotNull(amberLink);
                Assert.Equal($"{Paths.SchoolSpending(expected.Urn)}#medium-priority", amberLink.GetAttribute("href"));
            }
            else
            {
                Assert.Null(amberLink);
            }

            if (expected.Green > 0)
            {
                Assert.NotNull(greenLink);
                Assert.Equal($"{Paths.SchoolSpending(expected.Urn)}#low-priority", greenLink.GetAttribute("href"));
            }
            else
            {
                Assert.Null(greenLink);
            }

            var panel = card.QuerySelector(".panel--grey");
            var gridRow = panel?.QuerySelector(".govuk-grid-row");
            Assert.NotNull(gridRow);
            var categoryColumns = gridRow.QuerySelectorAll(".govuk-grid-column-one-half");

            Assert.NotNull(categoryColumns);
            Assert.Equal(expected.TopCategories.Count, categoryColumns.Length);

            for (var j = 0; j < expected.TopCategories.Count; j++)
            {
                var expectedCategory = expected.TopCategories[j];
                var column = categoryColumns[j];

                Assert.Contains(expectedCategory.Category, column.QuerySelector(".key-spend-category")?.TextContent.Trim());
                Assert.Contains(expectedCategory.Value.ToCurrency(), column.QuerySelector(".govuk-body-l strong")?.TextContent.Trim());
                Assert.Contains(expectedCategory.Unit, column.QuerySelectorAll(".govuk-body")[1].TextContent.Trim());
            }
        }

        var linkParagraph = prioritySchoolsSection.QuerySelector("[data-testid='view-more-info-trust-home']");
        Assert.NotNull(linkParagraph);

        var link = linkParagraph.QuerySelector("a.govuk-link");
        Assert.NotNull(link);
        Assert.Equal(Paths.TrustHome(trust.CompanyNumber), link.GetAttribute("href"));

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