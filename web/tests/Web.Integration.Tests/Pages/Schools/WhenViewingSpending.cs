using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using FluentValidation;
using Moq;
using Web.App;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingSpending(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly List<string> AllCostCategories =
    [
        Category.TeachingStaff,
        Category.NonEducationalSupportStaff,
        Category.EducationalSupplies,
        Category.EducationalIct,
        Category.PremisesStaffServices,
        Category.Utilities,
        Category.AdministrativeSupplies,
        Category.CateringStaffServices,
        Category.Other
    ];

    private static Dictionary<string, string> CategoryHeadingToIdMap => new()
    {
        { "Teaching and Teaching support staff", "teaching-and-teaching-support-staff" },
        { "Non-educational support staff", "non-educational-support-staff-and-services" },
        { "Educational supplies", "educational-supplies" },
        { "Educational ICT", "educational-ict" },
        { "Premises staff and services", "premises-staff-and-services" },
        { "Utilities", "utilities" },
        { "Administrative supplies", "administrative-supplies" },
        { "Catering staff and supplies", "catering-staff-and-supplies" }
    };

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(false, true, false)]
    [InlineData(false, true, true)]
    [InlineData(true, false, false)]
    [InlineData(false, false, false)]
    [InlineData(false, false, true)]
    public async Task CanDisplay(bool isPartOfTrust, bool ssrFeatureEnabled, bool cfrItSpendBreakdownEnabled)
    {
        var (page, school, ratings, expenditure) = await SetupNavigateInitPage(isPartOfTrust, ssrFeatureEnabled: ssrFeatureEnabled, cfrItSpendBreakdownEnabled: cfrItSpendBreakdownEnabled);

        AssertPageLayout(page, school, ratings, expenditure, isPartOfTrust, ssrFeatureEnabled, cfrItSpendBreakdown: cfrItSpendBreakdownEnabled);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplayChartWarningWhenChartApiFails(bool isPartOfTrust)
    {
        var (page, school, ratings, expenditure) = await SetupNavigateInitPage(isPartOfTrust, ssrFeatureEnabled: true, chartApiException: true);

        AssertPageLayout(page, school, ratings, expenditure, isPartOfTrust, true, true);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanNavigateUsingViewAllCategories(bool isPartOfTrust)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(isPartOfTrust);

        var categorySections = page.QuerySelectorAll("section");

        Assert.Equal(8, categorySections.Length);

        var expectedUrl = Paths.SchoolComparison(school.URN);

        foreach (var section in categorySections)
        {
            var sectionHeading = section.QuerySelector("h3")?.TextContent;
            Assert.NotNull(sectionHeading);

            var id = CategoryHeadingToIdMap[sectionHeading];

            var anchor = section.QuerySelector(".govuk-link");
            Assert.NotNull(anchor);

            var targetPage = await Client.Follow(anchor);

            DocumentAssert.AssertPageUrl(targetPage, $"{expectedUrl}#{id}".ToAbsolute());
            DocumentAssert.TitleAndH1(targetPage, "Benchmark spending - Financial Benchmarking and Insights Tool - GOV.UK",
                "Benchmark spending");
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanNavigateToCustomData(bool isPartOfTrust)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(isPartOfTrust);

        var anchor = page.QuerySelector("#custom-data-link");
        Assert.NotNull(anchor);

        var targetPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(targetPage, Paths.SchoolCustomData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolSpending(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpending(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolSpending(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpending(urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(true, true, true, false)]
    [InlineData(false, true, true, false)]
    [InlineData(true, true, true, true)]
    [InlineData(false, true, true, true)]
    [InlineData(false, true, false, false)]
    [InlineData(false, false, true, false)]
    [InlineData(false, false, false, false)]
    public async Task CanDisplayWithComparatorSet(bool isPartOfTrust, bool pupilSet, bool buildingSet, bool ssrFeatureEnabled)
    {
        var comparatorSet = new SchoolComparatorSet
        {
            Building = buildingSet ? Fixture.CreateMany<string>().ToArray() : [],
            Pupil = pupilSet ? Fixture.CreateMany<string>().ToArray() : []
        };
        var (page, school, ratings, expenditure) = await SetupNavigateInitPage(isPartOfTrust, comparatorSet, ssrFeatureEnabled);

        AssertPageLayout(page, school, ratings, expenditure, isPartOfTrust, ssrFeatureEnabled);
    }

    private async Task<(IHtmlDocument page, School school, RagRating[] ratings, SchoolExpenditure? expenditure)> SetupNavigateInitPage(
        bool isPartOfTrust,
        SchoolComparatorSet? comparatorSet = null,
        bool ssrFeatureEnabled = false,
        bool chartApiException = false,
        bool cfrItSpendBreakdownEnabled = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, isPartOfTrust ? EstablishmentTypes.Academies : EstablishmentTypes.Maintained)
            .With(x => x.TrustCompanyNumber, isPartOfTrust ? "1223545" : "")
            .Create();

        Assert.NotNull(school.URN);

        var rating = CreateRagRatings(school.URN);

        var expenditures = Fixture.Build<SchoolExpenditure>().CreateMany().ToArray();
        expenditures.ElementAt(0).URN = school.URN;

        var verticalBarChart = new ChartResponse
        {
            Html = "<svg />"
        };

        var disabledFeatureFlags = new List<string>();
        if (!ssrFeatureEnabled)
        {
            disabledFeatureFlags.Add(FeatureFlags.SchoolSpendingPrioritiesSsrCharts);
        }

        if (!cfrItSpendBreakdownEnabled)
        {
            disabledFeatureFlags.Add(FeatureFlags.CfrItSpendBreakdown);
        }

        var client = Client
            .SetupDisableFeatureFlags(disabledFeatureFlags.ToArray())
            .SetupEstablishment(school)
            .SetupInsights()
            .SetupUserData()
            .SetupMetricRagRating(rating)
            .SetupChartRendering<PriorityCostCategoryDatum>(verticalBarChart);
        client.ComparatorSetApi.Reset();

        if (chartApiException)
        {
            Client.SetupChartRenderingWithException<PriorityCostCategoryDatum>();
        }

        if (comparatorSet != null)
        {
            client.SetupComparatorSet(school, comparatorSet);
            var setup = client.ExpenditureApi.SetupSequence(api => api.QuerySchools(It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()));
            if ((comparatorSet.Pupil.Length > 0 && comparatorSet.Building.Length == 0) || (comparatorSet.Pupil.Length == 0 && comparatorSet.Building.Length > 0))
            {
                setup.ReturnsAsync(ApiResult.Ok(expenditures));
                setup.ReturnsAsync(ApiResult.BadRequest(new ValidationError(Severity.Error, "Urns", "Validation failed")));
            }
            else if (comparatorSet.Pupil.Length == 0 && comparatorSet.Building.Length == 0)
            {
                setup.ReturnsAsync(ApiResult.BadRequest(new ValidationError(Severity.Error, "Urns", "Validation failed")));
            }
            else
            {
                setup.ReturnsAsync(ApiResult.Ok(expenditures));
                setup.ReturnsAsync(ApiResult.Ok(expenditures));
            }
        }

        var page = await client.Navigate(Paths.SchoolSpending(school.URN));
        return (page, school, rating, comparatorSet == null ? null : expenditures.First());
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        RagRating[] ratings,
        SchoolExpenditure? expenditure,
        bool isPartOfTrust,
        bool ssrCharts = false,
        bool chartError = false,
        bool cfrItSpendBreakdown = false)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpending(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Spending priorities for this school - Financial Benchmarking and Insights Tool - GOV.UK",
            "Spending priorities for this school");

        var dataSourceElement = page.QuerySelectorAll("main > div > div:nth-child(3) > div > p");
        Assert.NotNull(dataSourceElement);

        if (school.IsPartOfTrust)
        {
            DocumentAssert.TextEqual(dataSourceElement.ElementAt(0), "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
            DocumentAssert.TextEqual(dataSourceElement.ElementAt(1), "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
        }
        else
        {
            DocumentAssert.TextEqual(dataSourceElement.ElementAt(0), "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
        }

        var categorySections = page.QuerySelectorAll("section");

        foreach (var section in categorySections)
        {
            var sectionHeading = section.QuerySelector("h3")?.TextContent;

            var costCodeList = section.QuerySelector(".app-cost-code-list");

            var expectedCostCodeList = new Dictionary<string, int>
            {
                { "Teaching and Teaching support staff", 5 },
                { "Non-educational support staff", isPartOfTrust ? 4 : 3 },
                { "Educational supplies", 2 },
                { "Educational ICT", cfrItSpendBreakdown ? 6 : 1 },
                { "Premises staff and services", 4 },
                { "Utilities", 2 },
                { "Administrative supplies", cfrItSpendBreakdown ? 2 : 1 },
                { "Catering staff and supplies", 2 }
            };

            Assert.NotNull(costCodeList);
            Assert.NotNull(sectionHeading);

            var expectedCount = expectedCostCodeList[sectionHeading];
            if (school.IsPartOfTrust)
            {
                expectedCount++;
                Assert.Equal("% of central services", costCodeList.LastElementChild?.TextContent);
            }

            Assert.True(expectedCount == costCodeList.ChildElementCount, $"Expected {expectedCount} child element(s) for {sectionHeading} but received {costCodeList.ChildElementCount}");

            Assert.NotEqual(Category.Other, sectionHeading);

            var chartSvg = section.QuerySelector(".ssr-chart");
            var chartWarning = section.QuerySelector(".ssr-chart-warning");
            var chartContainer = section.QuerySelector(".composed-container");

            if (ssrCharts)
            {
                if (chartError)
                {
                    Assert.NotNull(chartWarning);
                    Assert.Null(chartSvg);
                }
                else
                {
                    Assert.NotNull(chartSvg);
                    Assert.Null(chartWarning);
                }

                Assert.Null(chartContainer);
                AssertChartStats(section, ratings.SingleOrDefault(r => section.Id?.EndsWith(r.CostCategoryAnchorId) == true), expenditure);
            }
            else
            {
                Assert.NotNull(chartContainer);
                Assert.Null(chartWarning);
                Assert.Null(chartSvg);
            }
        }
    }

    private static void AssertChartStats(IElement section, RagRating? rating, SchoolExpenditure? expenditure)
    {
        var chartStats = section.QuerySelector(".chart-stat-summary");
        Assert.NotNull(chartStats);

        var wrappers = chartStats.QuerySelectorAll(".chart-stat-wrapper");
        Assert.Equal(expenditure == null ? 2 : 3, wrappers.Length);

        var stat1 = expenditure == null ? null : wrappers.ElementAt(0).TextContent.Trim();
        var stat2 = wrappers.ElementAt(expenditure == null ? 0 : 1).TextContent.Trim();
        var stat3 = wrappers.ElementAt(expenditure == null ? 1 : 2).TextContent.Trim();
        var unit = string.IsNullOrWhiteSpace(rating?.Category) ? string.Empty : Lookups.CategoryUnitMap[rating.Category];

        if (expenditure != null)
        {
            var sectionHeading = section.QuerySelector("h3")?.TextContent!;
            var expectedTotalsList = new Dictionary<string, decimal?>
            {
                { Category.TeachingStaff, expenditure.TotalTeachingSupportStaffCosts },
                { "Non-educational support staff", expenditure.TotalNonEducationalSupportStaffCosts },
                { Category.EducationalSupplies, expenditure.TotalEducationalSuppliesCosts },
                { Category.EducationalIct, expenditure.LearningResourcesIctCosts },
                { Category.PremisesStaffServices, expenditure.TotalPremisesStaffServiceCosts },
                { Category.Utilities, expenditure.TotalUtilitiesCosts },
                { Category.AdministrativeSupplies, expenditure.AdministrativeSuppliesNonEducationalCosts },
                { Category.CateringStaffServices, expenditure.TotalGrossCateringCosts }
            };

            var total = expectedTotalsList[sectionHeading];
            Assert.Equal($"This school spends\n    \n        {total?.ToCurrency()}\n    \n        \n            \n                {unit}", stat1);
        }

        Assert.Equal($"Similar schools spend\n    \n        {rating?.Median.ToCurrency()}\n    \n        \n            \n                {unit}, on average", stat2);

        var percentage = (rating?.DiffMedian ?? 0) / (rating?.Median ?? 1) * 100;
        Assert.Equal($"This school spends\n    \n        {rating?.DiffMedian.ToCurrency()}\n            \n                ({percentage:F1}%)\n            \n    \n        \n            \n                more {unit}", stat3);
    }

    private RagRating[] CreateRagRatings(string urn)
    {
        var random = new Random();

        var statusKeys = Lookups.StatusPriorityMap.Keys.ToList();

        var ratings = new List<RagRating>();

        foreach (var category in AllCostCategories)
        {
            var rating = Fixture.Build<RagRating>()
                .With(r => r.Category, category)
                .With(r => r.RAG, () => statusKeys[random.Next(statusKeys.Count)])
                .With(r => r.URN, urn)
                .Create();
            ratings.Add(rating);
        }

        return ratings.ToArray();
    }
}