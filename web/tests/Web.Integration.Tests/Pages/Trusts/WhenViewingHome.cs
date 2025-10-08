﻿using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Domain.Content;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingHome(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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
        { 8, Category.CateringStaffServices },
        { 9, Category.Other }
    };

    [Theory]
    [InlineData(true, true, true)]
    [InlineData(false, true, true)]
    [InlineData(true, false, true)]
    [InlineData(false, false, true)]
    [InlineData(false, false, false)]
    public async Task CanDisplay(bool showBanner, bool bfrItSpendFeatureEnabled, bool fbisFeatureEnabled)
    {
        var (page, trust, balance, ratings, schools, banner) = await SetupNavigateInitPage(showBanner: showBanner, bfrItSpendFeatureEnabled: bfrItSpendFeatureEnabled, fbisFeatureEnabled: fbisFeatureEnabled);

        AssertPageLayout(page, trust, balance, ratings, schools, banner, bfrItSpendFeatureEnabled, fbisFeatureEnabled);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustHome(companyName));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(companyName).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustHome(companyName));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(companyName).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayMissingSubmission()
    {
        var (page, trust, balance, ratings, schools, banner) = await SetupNavigateInitPage(false, false, false);

        AssertPageLayout(page, trust, balance, ratings, schools, banner);
    }

    [Fact]
    public async Task CanNavigateToChangeTrust()
    {
        var (page, _, _, _, _, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change trust");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.TrustSearch.ToAbsolute());
    }

    private async Task<(IHtmlDocument page, Trust trust, TrustBalance? balance, RagRating[] ratings, TrustSchool[] schools, Banner? banner)> SetupNavigateInitPage(
        bool includeRatings = true,
        bool includeSchools = true,
        bool includeBalance = true,
        bool showBanner = false,
        bool bfrItSpendFeatureEnabled = true,
        bool fbisFeatureEnabled = true)
    {
        var random = new Random();

        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, random.Next(10000000, 99999999).ToString())
            .Create();

        var schools = includeSchools
            ? Fixture.Build<TrustSchool>()
                .With(x => x.OverallPhase, () => OverallPhaseTypes.All.ElementAt(random.Next(0, OverallPhaseTypes.All.Length - 1)))
                .CreateMany(20).ToArray()
            : [];

        var values = AllCostCategories.Select(c => c.Value);
        var queue = new Queue<string>();
        var ratings = includeRatings
            ? schools.SelectMany(s =>
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
                    .CreateMany(values.Count());
            }).ToArray()
            : [];

        var balance = includeBalance ? Fixture.Create<TrustBalance>() : null;

        var banner = showBanner
            ? Fixture.Create<Banner>()
            : null;

        var disabledFeatures = new List<string>();
        if (!bfrItSpendFeatureEnabled)
        {
            disabledFeatures.Add(FeatureFlags.TrustItSpendBreakdown);
        }
        if (!fbisFeatureEnabled)
        {
            disabledFeatures.Add(FeatureFlags.FbisForTrust);
        }

        var client = Client
            .SetupDisableFeatureFlags(disabledFeatures.ToArray())
            .SetupEstablishment(trust, schools)
            .SetupInsights()
            .SetupMetricRagRating(ratings)
            .SetupBanner(banner);
        if (balance != null)
        {
            client.SetupBalance(trust, balance);
        }

        var page = await client.Navigate(Paths.TrustHome(trust.CompanyNumber));
        return (page, trust, balance, ratings, schools, banner);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        Trust trust,
        TrustBalance? balance,
        RagRating[] ratings,
        TrustSchool[] schools,
        Banner? banner,
        bool bfrItSpendFeatureEnabled = true,
        bool fbisFeatureEnabled = true)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        if (balance != null)
        {
            var dataSourceElement = page.QuerySelector($"main > div > div:nth-child({(banner == null ? "2" : "3")}) > div > p");
            Assert.NotNull(dataSourceElement);

            DocumentAssert.TextEqual(dataSourceElement, "This trust's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
        }
        else
        {
            var commentaryElement = page.QuerySelector("main > div > p");
            Assert.NotNull(commentaryElement);
            DocumentAssert.TextEqual(commentaryElement, "This trust does not have a submission for the current period.");
        }

        Assert.NotNull(trust.TrustName);
        DocumentAssert.TitleAndH1(page, "Your trust - Financial Benchmarking and Insights Tool - GOV.UK", trust.TrustName);

        if (ratings.Length != 0)
        {
            // headlines
            var highPriorityHeadline = page.QuerySelector("li.app-headline-high") as IHtmlListItemElement;
            Assert.Equal($"{ratings.Count(r => r.Category != Category.Other && r.RAG == "red")}\n\nHigh priority costs", highPriorityHeadline.GetInnerText());

            var mediumPriorityHeadline = page.QuerySelector("li.app-headline-medium") as IHtmlListItemElement;
            Assert.Equal($"{ratings.Count(r => r.Category != Category.Other && r.RAG == "amber")}\n\nMedium priority costs", mediumPriorityHeadline.GetInnerText());

            var lowPriorityHeadline = page.QuerySelector("li.app-headline-low") as IHtmlListItemElement;
            Assert.Equal($"{ratings.Count(r => r.Category != Category.Other && r.RAG == "green")}\n\nLow priority costs", lowPriorityHeadline.GetInnerText());

            // cost categories
            var costCategoryRag = page.QuerySelector("table.table-cost-category-rag") as IHtmlTableElement;
            var costCategoryRagRows = costCategoryRag?.Bodies.First().Rows;
            Assert.Equal(AllCostCategories.Count - 1, costCategoryRagRows?.Length);
        }

        // school phases
        if (schools.Length != 0)
        {
            var primarySchoolRag = page.QuerySelector("#school-rag-primary-schools") as IHtmlTableElement;
            var primarySchoolRagRows = primarySchoolRag?.Bodies.First().Rows;
            Assert.Equal(schools.Count(s => s.OverallPhase == OverallPhaseTypes.Primary), primarySchoolRagRows?.Length ?? 0);

            var secondarySchoolRag = page.QuerySelector("#school-rag-secondary-schools") as IHtmlTableElement;
            var secondarySchoolRagRows = secondarySchoolRag?.Bodies.First().Rows;
            Assert.Equal(schools.Count(s => s.OverallPhase == OverallPhaseTypes.Secondary), secondarySchoolRagRows?.Length ?? 0);

            var specialSchoolRag = page.QuerySelector("#school-rag-special") as IHtmlTableElement;
            var specialSchoolRagRows = specialSchoolRag?.Bodies.First().Rows;
            Assert.Equal(schools.Count(s => s.OverallPhase is OverallPhaseTypes.Special), specialSchoolRagRows?.Length ?? 0);

            var alternativeProvisionSchoolRag = page.QuerySelector("#school-rag-alternative-provision") as IHtmlTableElement;
            var alternativeProvisionSchoolRagRows = alternativeProvisionSchoolRag?.Bodies.First().Rows;
            Assert.Equal(schools.Count(s => s.OverallPhase is OverallPhaseTypes.AlternativeProvision), alternativeProvisionSchoolRagRows?.Length ?? 0);

            var postSixteenSchoolRag = page.QuerySelector("#school-rag-post-16") as IHtmlTableElement;
            var postSixteenSchoolRagRows = postSixteenSchoolRag?.Bodies.First().Rows;
            Assert.Equal(schools.Count(s => s.OverallPhase == OverallPhaseTypes.PostSixteen), postSixteenSchoolRagRows?.Length ?? 0);

            var universityTechnicalCollegeRag = page.QuerySelector("#school-rag-university-technical-colleges") as IHtmlTableElement;
            var universityTechnicalCollegeRagRows = universityTechnicalCollegeRag?.Bodies.First().Rows;
            Assert.Equal(schools.Count(s => s.OverallPhase == OverallPhaseTypes.UniversityTechnicalCollege), universityTechnicalCollegeRagRows?.Length ?? 0);
        }

        DocumentAssert.Banner(page, banner);

        AssertBenchmarkingTools(page, trust, balance, bfrItSpendFeatureEnabled);
        AssertResources(page, trust, fbisFeatureEnabled);
    }

    private static void AssertBenchmarkingTools(IHtmlDocument page, Trust trust, TrustBalance? balance, bool bfrItSpendFeatureEnabled)
    {
        var toolsSection = page.GetElementById("benchmarking-and-planning-tools");

        //NB: No RAG therefore section not shown
        if (balance == null)
        {
            Assert.Null(toolsSection);
            return;
        }

        DocumentAssert.Heading2(toolsSection, "Benchmarking and planning tools");

        var toolsLinks = toolsSection?.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(bfrItSpendFeatureEnabled ? 6 : 5, toolsLinks?.Count);

        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(0), "View school spending", Paths.TrustComparison(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(1), "View pupil and workforce data", Paths.TrustCensus(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(2), "Curriculum and financial planning", Paths.TrustFinancialPlanning(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(3), "Trust to trust benchmarking", Paths.TrustComparators(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(bfrItSpendFeatureEnabled ? 5 : 4), "Forecast and risk", Paths.TrustForecast(trust.CompanyNumber).ToAbsolute());
        if (bfrItSpendFeatureEnabled)
        {
            DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(4), "Benchmark IT spending", Paths.TrustComparisonItSpend(trust.CompanyNumber).ToAbsolute());
        }
    }

    private static void AssertResources(IHtmlDocument page, Trust trust, bool fbisFeatureEnabled)
    {
        var resourcesSection = page.GetElementById("establishment-resources");
        DocumentAssert.Heading2(resourcesSection, "Resources");

        var resourcesLinks = resourcesSection?.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(fbisFeatureEnabled ? 5 : 4, resourcesLinks?.Count);

        DocumentAssert.Link(resourcesLinks?.ElementAtOrDefault(0), "Find ways to spend less", Paths.TrustResources(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(resourcesLinks?.ElementAtOrDefault(1), "View historic data", Paths.TrustHistory(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(resourcesLinks?.ElementAtOrDefault(2), "Trust contact details", Paths.TrustDetails(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(resourcesLinks?.ElementAtOrDefault(3), "Data sources and interpretation", Paths.DataSources.ToAbsolute());
        if (fbisFeatureEnabled)
        {
            DocumentAssert.Link(resourcesLinks?.ElementAtOrDefault(4), "Financial Benchmarking and Insights Summary", Paths.TrustFinancialBenchmarkingInsightsSummary(trust.CompanyNumber).ToAbsolute());
        }
    }
}