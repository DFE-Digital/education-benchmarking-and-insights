using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using FluentValidation;
using Moq;
using Web.App;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.ViewComponents;
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
        {
            "Teaching and Teaching support staff", "teaching-and-teaching-support-staff"
        },
        {
            "Non-educational support staff", "non-educational-support-staff-and-services"
        },
        {
            "Educational supplies", "educational-supplies"
        },
        {
            "Educational ICT", "educational-ict"
        },
        {
            "Premises staff and services", "premises-staff-and-services"
        },
        {
            "Utilities", "utilities"
        },
        {
            "Administrative supplies", "administrative-supplies"
        },
        {
            "Catering staff and supplies", "catering-staff-and-supplies"
        }
    };

    [Theory]
    [InlineData(EstablishmentTypes.Academies, true)]
    [InlineData(EstablishmentTypes.Maintained, true)]
    [InlineData(EstablishmentTypes.Academies, false)]
    [InlineData(EstablishmentTypes.Maintained, false)]
    public async Task CanDisplay(string financeType, bool ssrFeatureEnabled)
    {
        var (page, school, ratings, expenditure) = await SetupNavigateInitPage(financeType, ssrFeatureEnabled: ssrFeatureEnabled);

        AssertPageLayout(page, school, ratings, expenditure, financeType, ssrFeatureEnabled);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayChartWarningWhenChartApiFails(string financeType)
    {
        var (page, school, ratings, expenditure) = await SetupNavigateInitPage(financeType, ssrFeatureEnabled: true, chartApiException: true);

        AssertPageLayout(page, school, ratings, expenditure, financeType, true, true);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateUsingViewAllCategories(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);

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
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCustomData(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);

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
    [InlineData(EstablishmentTypes.Academies, true, true, false)]
    [InlineData(EstablishmentTypes.Maintained, true, true, false)]
    [InlineData(EstablishmentTypes.Academies, true, true, true)]
    [InlineData(EstablishmentTypes.Maintained, true, true, true)]
    [InlineData(EstablishmentTypes.Maintained, true, false, false)]
    [InlineData(EstablishmentTypes.Maintained, false, true, false)]
    [InlineData(EstablishmentTypes.Maintained, false, false, false)]
    public async Task CanDisplayWithComparatorSet(string financeType, bool pupilSet, bool buildingSet, bool ssrFeatureEnabled)
    {
        var comparatorSet = new SchoolComparatorSet
        {
            Building = buildingSet ? Fixture.CreateMany<string>().ToArray() : [],
            Pupil = pupilSet ? Fixture.CreateMany<string>().ToArray() : []
        };
        var (page, school, ratings, expenditure) = await SetupNavigateInitPage(financeType, comparatorSet, ssrFeatureEnabled);

        AssertPageLayout(page, school, ratings, expenditure, financeType, ssrFeatureEnabled);
    }

    private async Task<(IHtmlDocument page, School school, RagRating[] ratings, SchoolExpenditure? expenditure)> SetupNavigateInitPage(
        string financeType,
        SchoolComparatorSet? comparatorSet = null,
        bool ssrFeatureEnabled = false,
        bool chartApiException = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .With(x => x.TrustCompanyNumber, financeType == EstablishmentTypes.Academies ? "1223545" : "")
            .Create();

        Assert.NotNull(school.URN);

        var rating = CreateRagRatings(school.URN);

        var expenditures = Fixture.Build<SchoolExpenditure>().CreateMany().ToArray();
        expenditures.ElementAt(0).URN = school.URN;

        var verticalBarChart = new ChartResponse { Html = "<svg />" };

        var client = Client
            .SetupDisableFeatureFlags(ssrFeatureEnabled ? [] : [FeatureFlags.SchoolSpendingPrioritiesSsrCharts])
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
            if (comparatorSet.Pupil.Length > 0 && comparatorSet.Building.Length == 0 || comparatorSet.Pupil.Length == 0 && comparatorSet.Building.Length > 0)
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

    private static void AssertPageLayout(IHtmlDocument page, School school, RagRating[] ratings, SchoolExpenditure? expenditure, string financeType, bool ssrCharts = false, bool chartError = false)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpending(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Spending priorities for this school - Financial Benchmarking and Insights Tool - GOV.UK",
            "Spending priorities for this school");

        var categorySections = page.QuerySelectorAll("section");

        foreach (var section in categorySections)
        {
            var sectionHeading = section.QuerySelector("h3")?.TextContent;

            var costCodeList = section.QuerySelector(".app-cost-code-list");

            var expectedCostCodeList = new Dictionary<string, int>
            {
                { "Teaching and Teaching support staff", 5 },
                { "Non-educational support staff", financeType == EstablishmentTypes.Maintained ? 3 : 4 },
                { "Educational supplies", 2 },
                { "Educational ICT", 1 },
                { "Premises staff and services", 4 },
                { "Utilities", 2 },
                { "Administrative supplies", 1 },
                { "Catering staff and supplies", 2 }
            };

            Assert.NotNull(costCodeList);
            Assert.NotNull(sectionHeading);
            Assert.Equal(expectedCostCodeList[sectionHeading], costCodeList.ChildElementCount);

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
            Assert.Equal($"This school spends\n    \n        {total?.ToCurrency(0)}\n    \n        \n            \n                {unit}", stat1);
        }

        Assert.Equal($"Similar schools spend\n    \n        {rating?.Median.ToCurrency(0)}\n    \n        \n            \n                {unit}, on average", stat2);

        var percentage = (rating?.DiffMedian ?? 0) / (rating?.Median ?? 1) * 100;
        Assert.Equal($"This school spends\n    \n        {rating?.DiffMedian.ToCurrency(0)}\n            \n                ({percentage:F1}%)\n            \n    \n        \n            \n                more {unit}", stat3);
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