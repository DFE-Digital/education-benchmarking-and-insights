using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
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
        var (page, school) = await SetupNavigateInitPage(withUserData, ssrFeatureEnabled);

        if (withUserData)
        {
            AssertPageLayout(page, school, ssrFeatureEnabled);
        }
        else
        {
            DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute());
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplayNonSsrChartsWhenChartApiFails(bool withUserData)
    {
        var (page, school) = await SetupNavigateInitPage(withUserData, true, true);

        if (withUserData)
        {
            AssertPageLayout(page, school);
        }
        else
        {
            DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute());
        }
    }

    [Fact]
    public async Task CanNavigateUsingViewAllCategories()
    {
        var (page, school) = await SetupNavigateInitPage(true);

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

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(bool withUserData, bool ssrFeatureEnabled = false, bool chartApiException = false)
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

        var expenditure = Fixture.Build<SchoolExpenditure>().Create();
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

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, bool ssrCharts = false)
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
            var chartContainer = section.QuerySelector(".composed-container");

            if (ssrCharts)
            {
                Assert.NotNull(chartSvg);
                Assert.Null(chartContainer);
            }
            else
            {
                Assert.NotNull(chartContainer);
                Assert.Null(chartSvg);
            }
        }
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