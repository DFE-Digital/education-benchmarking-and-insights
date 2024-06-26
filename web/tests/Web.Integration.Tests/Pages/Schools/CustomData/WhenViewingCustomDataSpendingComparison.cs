using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using System.Net;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.CustomData;
public class WhenViewingCustomDataSpendingComparison(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true, true, true)]
    [InlineData(true, false, false)]
    [InlineData(true, true, false)]
    [InlineData(false, false, false)]
    [InlineData(false, true, true)]
    [InlineData(false, false, true)]
    public async Task CanDisplay(bool withUserData, bool noCategoryChange, bool allCategoryChange)
    {
        var (page, school, originalRatings, customRatings) = await SetupNavigateInitPage(withUserData, noCategoryChange, allCategoryChange);

        if (withUserData)
        {
            AssertPageLayout(page, school, originalRatings, customRatings);
        }
        else
        {
            DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute());
        }
    }

    [Fact]
    public async Task CanNavigateToCompareYourCostsCustomData()
    {
        var (page, school, _, _) = await SetupNavigateInitPage(true, false, false);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparisonCustomData(school.URN).ToAbsolute());
    }

    // TODO: enable test once this page is complete
    //[Fact]
    //public async Task CanNavigateToSpendingCustomData()
    //{
    //    var (page, school, _, _) = await SetupNavigateInitPage(true);

    //    var liElements = page.QuerySelectorAll("ul.app-links > li");
    //    var anchor = liElements[1].QuerySelector("h3 > a");
    //    Assert.NotNull(anchor);

    //    var newPage = await Client.Follow(anchor);

    //    DocumentAssert.AssertPageUrl(newPage, Paths.SchoolSpendingCustomData(school.URN).ToAbsolute());
    //}

    [Fact]
    public async Task CanNavigateToCensusCustomData()
    {
        var (page, school, _, _) = await SetupNavigateInitPage(true);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[2].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCensusCustomData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        var userData = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = "123"
            }
        };

        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupUserData(userData)
            .Navigate(Paths.SchoolSpendingComparison(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpendingComparison(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolSpendingComparison(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpendingComparison(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school, RagRating[] originalRatings, RagRating[] customRatings)> SetupNavigateInitPage(bool withUserData, bool noCategoryChange = false, bool allCategoryChange = false)
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
                Id = customDataId
            }
        };

        Assert.NotNull(school.URN);

        var originalRatings = CreateRagRatings(school.URN);
        var customRatings = CreateRagRatings(school.URN);

        if (noCategoryChange)
        {
            Array.ForEach(customRatings, r =>
            {
                var original = originalRatings.FirstOrDefault(orig => orig.Category == r.Category);
                r.RAG = original?.RAG;
            });
        }

        if (allCategoryChange)
        {
            var statusKeys = Lookups.StatusPriorityMap.Keys.ToList();
            var random = new Random();

            Array.ForEach(customRatings, r =>
            {
                var original = originalRatings.FirstOrDefault(orig => orig.Category == r.Category);
                if (r.RAG == original?.RAG)
                {
                    string newValue;
                    do
                    {
                        newValue = statusKeys[random.Next(statusKeys.Count)];
                    } while (newValue == original?.RAG);

                    r.RAG = newValue;
                }
            });
        }

        IHtmlDocument page;

        if (withUserData)
        {
            page = await Client.SetupEstablishment(school)
                .SetupUserData(userData)
                .SetupMetricRagRatingIncCustom(customDataId, customRatings, originalRatings)
                .Navigate(Paths.SchoolSpendingComparison(school.URN));
        }
        else
        {
            page = await Client.SetupEstablishment(school)
                .SetupMetricRagRating()
                .SetupInsights()
                .SetupUserData()
                .Navigate(Paths.SchoolSpendingComparison(school.URN));
        }

        return (page, school, originalRatings, customRatings);

    }

    private static void AssertPageLayout(IHtmlDocument page, School school, RagRating[] originalRatings, RagRating[] customRatings)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSpendingComparison(school.URN).ToAbsolute());

        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute()),
            ("Customised data", Paths.SchoolCustomisedData(school.URN).ToAbsolute()),
            ("Side-by-side comparison", Paths.SchoolSpendingComparison(school.URN).ToAbsolute()),
        };

        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        DocumentAssert.TitleAndH1(page, "Side-by-side comparison - Financial Benchmarking and Insights Tool - GOV.UK",
            "Side-by-side comparison");

        var headlinesSection = page.Body.SelectSingleNode("//main/div/div[2]");
        Assert.NotNull(headlinesSection);

        var highHeadline = headlinesSection.ChildNodes.QuerySelectorAll(".app-headline-high > p").ToList();
        Assert.NotNull(highHeadline);
        AssertHeadlines(highHeadline, "red", customRatings, originalRatings);

        var mediumHeadline = headlinesSection.ChildNodes.QuerySelectorAll(".app-headline-medium > p").ToList();
        Assert.NotNull(mediumHeadline);
        AssertHeadlines(mediumHeadline, "amber", customRatings, originalRatings);

        var lowHeadline = headlinesSection.ChildNodes.QuerySelectorAll(".app-headline-low > p").ToList();
        Assert.NotNull(lowHeadline);
        AssertHeadlines(lowHeadline, "green", customRatings, originalRatings);

        var categorySections = page.QuerySelectorAll(".top-categories").ToList();
        Assert.NotNull(categorySections);
        Assert.Equal(2, categorySections.Count);

        var changeCount = customRatings
            .Join(originalRatings,
                  custom => custom.Category,
                  original => original.Category,
                  (custom, original) => new { custom, original })
            .Where(joined => joined.custom.RAG != joined.original.RAG)
            .Count();

        if (changeCount > 0)
        {
            var changeSection = categorySections[0].ChildNodes.QuerySelectorAll("div").ToList();
            Assert.NotNull(changeSection);
            Assert.Equal(changeCount, changeSection.Count);

            foreach (var table in changeSection)
            {
                AssertTableContents(table, originalRatings, customRatings);
            }
        }
        else
        {
            var changeSection = categorySections[0]?.ChildNodes.QuerySelector("p")?.TextContent;
            Assert.NotNull(changeSection);
            Assert.Contains("There are no categories to display of this type for this comparison", changeSection);
        }

        var noChangeCount = customRatings
            .Join(originalRatings,
                  custom => custom.Category,
                  original => original.Category,
                  (custom, original) => new { custom, original })
            .Where(joined => joined.custom.RAG == joined.original.RAG)
            .Count();

        if (noChangeCount > 0)
        {
            var noChangeSection = categorySections[1].ChildNodes.QuerySelectorAll("div").ToList();
            Assert.NotNull(noChangeSection);
            Assert.Equal(noChangeCount, noChangeSection.Count);

            foreach (var table in noChangeSection)
            {
                AssertTableContents(table, originalRatings, customRatings);
            }
        }
        else
        {
            var noChangeSection = categorySections[1]?.ChildNodes.QuerySelector("p")?.TextContent;
            Assert.NotNull(noChangeSection);
            Assert.Contains("There are no categories to display of this type for this comparison", noChangeSection);
        }

        var toolsListSection = page.Body.SelectSingleNode("//main/div/div[4]");
        DocumentAssert.Heading2(toolsListSection, "Custom data benchmarking tools");

        var toolsLinks = toolsListSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(3, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "Compare your costs",
            Paths.SchoolComparisonCustomData(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Spending priorities for this school",
            Paths.SchoolSpendingCustomData(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks[2], "Benchmark pupil and workforce data",
            Paths.SchoolCensusCustomData(school.URN).ToAbsolute());
    }

    private RagRating[] CreateRagRatings(string urn)
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

    private static void AssertHeadlines(List<IElement> elements, string rag, RagRating[] customRatings, RagRating[] originalRatings)
    {
        Assert.Equal(3, elements.Count);

        var expectedCustomCount = customRatings.Count(x => x.RAG == rag);
        var expectedOriginalCount = originalRatings.Count(x => x.RAG == rag);

        var customCountSection = elements[0].QuerySelectorAll("span").ToList();

        var numericPart = customCountSection[0].TextContent.Trim();

        var changeSymbol = customCountSection[1].TextContent.Trim();

        Assert.Equal(expectedCustomCount.ToString(), numericPart);

        switch (expectedCustomCount)
        {
            case var _ when expectedCustomCount > expectedOriginalCount:
                Assert.Contains(ChangeSymbols.Increase, changeSymbol);
                Assert.Contains(ChangeSymbols.IncreaseText, changeSymbol);
                break;
            case var _ when expectedCustomCount < expectedOriginalCount:
                Assert.Contains(ChangeSymbols.Decrease, changeSymbol);
                Assert.Contains(ChangeSymbols.DecreaseText, changeSymbol);
                break;
            case var _ when expectedCustomCount == expectedOriginalCount:
                Assert.Contains(ChangeSymbols.NoChangeText, changeSymbol);
                break;
            default:
                Assert.Fail("Error with comparison for changeSymbol assertion");
                break;
        }

        var originalCountText = elements[1].TextContent.Replace("(was ", "").TrimEnd(')');
        Assert.Equal(expectedOriginalCount.ToString(), originalCountText);
    }

    private static void AssertTableContents(IElement table, RagRating[] originalRatings, RagRating[] customRatings)
    {
        var heading = table.QuerySelector("caption")?.TextContent.Trim();
        Assert.NotNull(heading);

        var originalExpected = originalRatings.FirstOrDefault(x => x.Category == heading);
        Assert.NotNull(originalExpected);

        var customExpected = customRatings.FirstOrDefault(x => x.Category == heading);
        Assert.NotNull(customExpected);

        var rows = table.QuerySelectorAll("table.govuk-table tbody.govuk-table__body tr.govuk-table__row").ToList();
        Assert.Equal(2, rows.Count);

        var originalRow = rows[0];
        var customRow = rows[1];

        var originalPriorityCell = originalRow.QuerySelector("td:nth-child(2)")?.TextContent.Trim();
        Assert.Equal(originalExpected.PriorityTag?.DisplayText, originalPriorityCell);

        var originalSpendCell = originalRow.QuerySelector("td:nth-child(3)")?.TextContent.Replace("£", "").Trim();
        Assert.Equal(originalExpected.Value.ToString(), originalSpendCell);

        var originalPercentageCell = originalRow.QuerySelector("td:nth-child(4)")?.TextContent.Trim();
        Assert.NotNull(originalPercentageCell);
        var originalExpectedPercentage = originalExpected.Percentile.ToString();
        Assert.NotNull(originalExpectedPercentage);
        Assert.Contains(originalExpectedPercentage, originalPercentageCell);

        var customPriorityCell = customRow.QuerySelector("td:nth-child(2)")?.TextContent.Trim();
        Assert.Equal(customExpected.PriorityTag?.DisplayText, customPriorityCell);

        var customSpendCell = customRow.QuerySelector("td:nth-child(3)")?.TextContent.Replace("£", "").Trim();
        Assert.Equal(customExpected.Value.ToString(), customSpendCell);

        var customPercentageCell = customRow.QuerySelector("td:nth-child(4)")?.TextContent.Trim();
        Assert.NotNull(customPercentageCell);
        var customExpectedPercentage = customExpected.Percentile.ToString();
        Assert.NotNull(customExpectedPercentage);
        Assert.Contains(customExpectedPercentage, customPercentageCell);
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

    private static class ChangeSymbols
    {
        public const string Decrease = "▼";
        public const string Increase = "▲";
        public const string NoChange = "";
        public const string DecreaseText = "decreased";
        public const string IncreaseText = "increased";
        public const string NoChangeText = "no change";
    }
}

