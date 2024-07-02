using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.CustomData;
public class WhenViewingCustomDataCensus(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplay(bool withUserData)
    {
        var (page, school) = await SetupNavigateInitPage(withUserData);

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
    public async Task CanDisplayNotFound()
    {
        var userData = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = "123",
                Status = "complete"
            }
        };

        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupUserData(userData)
            .Navigate(Paths.SchoolCensusCustomData(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCensusCustomData(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        var userData = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = "123",
                Status = "complete"
            }
        };

        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .SetupUserData(userData)
            .Navigate(Paths.SchoolCensusCustomData(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCensusCustomData(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(bool withUserData)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
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

        var customRatings = CreateRagRatings(school.URN);

        IHtmlDocument page;

        if (withUserData)
        {
            page = await Client.SetupEstablishment(school)
                .SetupUserData(userData)
                .SetupMetricRagRatingIncCustom(customDataId, customRatings)
                .Navigate(Paths.SchoolCensusCustomData(school.URN));
        }
        else
        {
            page = await Client.SetupEstablishment(school)
                .SetupMetricRagRating()
                .SetupInsights()
                .SetupUserData()
                .Navigate(Paths.SchoolCensusCustomData(school.URN));
        }
        return (page, school);

    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCensusCustomData(school.URN).ToAbsolute());

        var expectedBreadcrumbs = new[]
       {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute()),
            ("Customised data", Paths.SchoolCustomisedData(school.URN).ToAbsolute()),
        };

        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        DocumentAssert.TitleAndH1(page, "Benchmark pupil and workforce data - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark pupil and workforce data");

        var censusComponent = page.GetElementById("compare-your-census");
        Assert.NotNull(censusComponent);

        var toolsListSection = page.Body.SelectSingleNode("//main/div/div[4]");
        DocumentAssert.Heading2(toolsListSection, "Custom data benchmarking tools");

        var toolsLinks = toolsListSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(3, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "Side-by-side comparison",
            Paths.SchoolSpendingComparison(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Compare your costs",
            Paths.SchoolComparisonCustomData(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks[2], "Spending priorities for this school",
            Paths.SchoolSpendingCustomData(school.URN).ToAbsolute());
    }

    private RagRating[] CreateRagRatings(string urn)
    {
        {
            var random = new Random();

            var statusKeys = Lookups.StatusPriorityMap.Keys.ToList();

            var ratings = new List<RagRating>();

            foreach (var category in AllCostCategories)
            {
                var rating = Fixture.Build<RagRating>()
                                    .With(x => x.Category, category)
                                    .With(r => r.RAG, () => statusKeys[random.Next(statusKeys.Count)])
                                    .With(r => r.URN, urn)
                                    .Create();
                ratings.Add(rating);
            }

            return ratings.ToArray();
        }
    }

    private static readonly List<string> AllCostCategories = new()
    {
        {
            Category.TeachingStaff
        },
        {
            Category.NonEducationalSupportStaff
        },
        {
            Category.EducationalSupplies
        },
        {
            Category.EducationalIct
        },
        {
            Category.PremisesStaffServices
        },
        {
            Category.Utilities
        },
        {
            Category.AdministrativeSupplies
        },
        {
            Category.CateringStaffServices
        },
        {
            Category.Other
        }
    };
}


