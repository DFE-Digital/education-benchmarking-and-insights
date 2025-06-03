using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.ViewComponents;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.CustomData;

public class WhenViewingCustomDataSpending(SchoolBenchmarkingWebAppClient client)
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
    [InlineData(true, false)]
    [InlineData(false, false)]
    [InlineData(true, true)]
    [InlineData(false, true)]
    public async Task CanDisplay(bool withUserData, bool ssrFeatureEnabled)
    {
        var (page, school, ratings, expenditure) = await SetupNavigateInitPage(withUserData, ssrFeatureEnabled);

        if (withUserData)
        {
            AssertPageLayout(page, school, ratings, expenditure, ssrFeatureEnabled);
        }
        else
        {
            DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute());
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplayChartWarningWhenChartApiFails(bool withUserData)
    {
        var (page, school, ratings, expenditure) = await SetupNavigateInitPage(withUserData, true, true);

        if (withUserData)
        {
            AssertPageLayout(page, school, ratings, expenditure, true, true);
        }
        else
        {
            DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute());
        }
    }

    [Fact]
    public async Task CanNavigateUsingViewAllCategories()
    {
        var (page, school, _, _) = await SetupNavigateInitPage(true);

        var categorySections = page.QuerySelectorAll("section");

        Assert.Equal(8, categorySections.Length);

        var expectedUrl = Paths.SchoolComparisonCustomData(school.URN);

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

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";

        var userData = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = "123",
                Status = "complete"
            }
        };

        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupUserData(userData)
            .Navigate(Paths.SchoolSpendingCustomData(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpendingCustomData(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";

        var userData = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = "123",
                Status = "complete"
            }
        };

        var page = await Client.SetupEstablishmentWithException()
            .SetupUserData(userData)
            .Navigate(Paths.SchoolSpendingCustomData(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpendingCustomData(urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school, RagRating[] ratings, SchoolExpenditure? expenditure)> SetupNavigateInitPage(bool withUserData, bool ssrFeatureEnabled = false, bool chartApiException = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Without(x => x.FederationLeadURN)
            .Create();

        var customDataId = "123";

        var userData = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = customDataId,
                Status = "complete"
            }
        };

        Assert.NotNull(school.URN);

        var rating = CreateRagRatings(school.URN);

        var expenditure = Fixture.Build<SchoolExpenditure>()
            .With(e => e.URN, school.URN)
            .Create();

        var verticalBarChart = new ChartResponse { Html = "<svg />" };

        IHtmlDocument page;

        Client
            .SetupDisableFeatureFlags(ssrFeatureEnabled ? [] : [FeatureFlags.SchoolSpendingPrioritiesSsrCharts])
            .SetupEstablishment(school)
            .SetupInsights()
            .SetupChartRendering<PriorityCostCategoryDatum>(verticalBarChart);

        if (chartApiException)
        {
            Client.SetupChartRenderingWithException<PriorityCostCategoryDatum>();
        }

        if (withUserData)
        {
            page = await Client
                .SetupUserData(userData)
                .SetupMetricRagRatingIncCustom(customDataId, rating)
                .SetupExpenditureForCustomData(school, customDataId, expenditure)
                .Navigate(Paths.SchoolSpendingCustomData(school.URN));
        }
        else
        {
            page = await Client
                .SetupUserData()
                .SetupMetricRagRating(rating)
                .SetupExpenditure(school, expenditure)
                .Navigate(Paths.SchoolSpendingCustomData(school.URN));
        }

        return (page, school, rating, expenditure);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, RagRating[] ratings, SchoolExpenditure? expenditure, bool ssrCharts = false, bool chartError = false)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpendingCustomData(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Spending priorities for this school - Financial Benchmarking and Insights Tool - GOV.UK",
            "Spending priorities for this school");

        var categorySections = page.QuerySelectorAll("section");

        foreach (var section in categorySections)
        {
            var sectionHeading = section.QuerySelector("h3")?.TextContent;

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