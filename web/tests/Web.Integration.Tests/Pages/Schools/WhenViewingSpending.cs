using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using FluentValidation;
using Moq;
using Web.App.Domain;
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
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        AssertPageLayout(page, school, financeType);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateUsingViewAllCategories(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

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
        var (page, school) = await SetupNavigateInitPage(financeType);

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
    [InlineData(EstablishmentTypes.Academies, true, true)]
    [InlineData(EstablishmentTypes.Maintained, true, true)]
    [InlineData(EstablishmentTypes.Maintained, true, false)]
    [InlineData(EstablishmentTypes.Maintained, false, true)]
    [InlineData(EstablishmentTypes.Maintained, false, false)]
    public async Task CanDisplayWithComparatorSet(string financeType, bool pupilSet, bool buildingSet)
    {
        var comparatorSet = new SchoolComparatorSet
        {
            Building = buildingSet ? Fixture.CreateMany<string>().ToArray() : [],
            Pupil = pupilSet ? Fixture.CreateMany<string>().ToArray() : []
        };
        var (page, school) = await SetupNavigateInitPage(financeType, comparatorSet);

        AssertPageLayout(page, school, financeType);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, SchoolComparatorSet? comparatorSet = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .With(x => x.TrustCompanyNumber, financeType == EstablishmentTypes.Academies ? "1223545" : "")
            .Create();

        Assert.NotNull(school.URN);

        var rating = CreateRagRatings(school.URN);

        var expenditures = Fixture.Build<SchoolExpenditure>().CreateMany().ToArray();

        var client = Client.SetupEstablishment(school)
            .SetupInsights()
            .SetupUserData()
            .SetupMetricRagRating(rating);

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
        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, string financeType)
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