using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Comparison;

public class WhenViewingComparisonItSpend(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly ExpectedSubCategory[] AllSubCategories =
    [
        new("ICT costs: Administration software and systems", "ICT costs: Administration software and systems", 0),
        new("ICT costs: Connectivity", "ICT costs: Connectivity", 1),
        new("ICT costs: IT learning resources", "ICT costs: IT learning resources", 2),
        new("ICT costs: IT support", "ICT costs: IT support", 3),
        new("ICT costs: Laptops, desktops and tablets", "ICT costs: Laptops, desktops and tablets", 4),
        new("ICT costs: Onsite servers", "ICT costs: Onsite servers", 5),
        new("ICT costs: Other hardware", "ICT costs: Other hardware", 6)
    ];

    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust, userDefinedSet, spend) = await SetupNavigateInitPage();

        AssertPageLayout(page, trust, userDefinedSet, spend);
    }

    [Fact]
    public async Task CanDisplayChartWarningWhenChartApiFails()
    {
        var (page, trust, userDefinedSet, spend) = await SetupNavigateInitPage(chartApiException: true);

        AssertPageLayout(page, trust, userDefinedSet, spend, chartError: true);
    }

    [Fact]
    public async Task CanStartCreateComparatorsJourneyWhenComparatorsMissing()
    {
        var (page, trust, _, _) = await SetupNavigateInitPage(false);

        var redirectUri = WebUtility.UrlEncode(Paths.TrustComparisonItSpend(trust.CompanyNumber));
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreateBy(trust.CompanyNumber, redirectUri).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayTrustComparatorsWhenComparatorSetEmpty()
    {
        var (page, trust, _, _) = await SetupNavigateInitPage(true, true);

        var redirectUri = WebUtility.UrlEncode(Paths.TrustComparisonItSpend(trust.CompanyNumber));
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsUserDefined(trust.CompanyNumber, null, redirectUri).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToTrustComparators()
    {
        var (page, trust, _, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelector("a[data-test-id='comparators-link']");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);
        var redirectUri = WebUtility.UrlEncode(Paths.TrustComparisonItSpend(trust.CompanyNumber));
        DocumentAssert.AssertPageUrl(newPage, Paths.TrustComparatorsUserDefined(trust.CompanyNumber, null, redirectUri).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyNumber = "12345678";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustComparison(companyNumber));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparison(companyNumber).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyNumber = "12345678";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustComparison(companyNumber));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparison(companyNumber).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(0, "?viewAs=0&resultAs=1")]
    [InlineData(1, "?viewAs=1&resultAs=1")]
    public async Task CanSubmitFilterOptionsForViewAs(int viewAs, string expectedQueryParams)
    {
        var (page, trust, userDefinedSet, spend) = await SetupNavigateInitPage();

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "ViewAs", viewAs.ToString() }
            });
        });

        AssertPageLayout(
            page,
            trust,
            userDefinedSet,
            spend,
            viewAs,
            expectedQueryParams: expectedQueryParams);
    }

    [Theory]
    [InlineData(1, "?viewAs=0&resultAs=1")]
    public async Task CanSubmitFilterOptionsForResultsAs(int resultAs, string expectedQueryParams)
    {
        var (page, trust, userDefinedSet, spend) = await SetupNavigateInitPage();

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "ResultAs", resultAs.ToString() }
            });
        });

        AssertPageLayout(
            page,
            trust,
            userDefinedSet,
            spend,
            resultAs: resultAs,
            expectedQueryParams: expectedQueryParams);
    }

    [Theory]
    [InlineData(0, "?viewAs=0&resultAs=1&selectedSubCategories=0")]
    [InlineData(1, "?viewAs=0&resultAs=1&selectedSubCategories=1")]
    [InlineData(2, "?viewAs=0&resultAs=1&selectedSubCategories=2")]
    [InlineData(3, "?viewAs=0&resultAs=1&selectedSubCategories=3")]
    [InlineData(4, "?viewAs=0&resultAs=1&selectedSubCategories=4")]
    [InlineData(5, "?viewAs=0&resultAs=1&selectedSubCategories=5")]
    [InlineData(6, "?viewAs=0&resultAs=1&selectedSubCategories=6")]
    public async Task CanSubmitFilterOptionsForSubCategories(int expectedSubCategoryId, string expectedQueryParams)
    {
        var (page, trust, userDefinedSet, spend) = await SetupNavigateInitPage();

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "SelectedSubCategories", expectedSubCategoryId.ToString() }
            });
        });

        AssertPageLayout(
            page,
            trust,
            userDefinedSet,
            spend,
            expectedSubCategories: BuildExpectedSubCategories(expectedSubCategoryId),
            expectedQueryParams: expectedQueryParams);
    }

    [Fact]
    public async Task CanFollowChipsCorrectlyUpdatesPage()
    {
        var (page, trust, userDefinedSet, spend) = await SetupNavigateInitPage(
            queryParams: "?viewAs=0&resultAs=1&selectedSubCategories=0&selectedSubCategories=1");

        var target = page.QuerySelectorAll("a.app-filter__tag")
            .FirstOrDefault(el => el.TextContent.Contains("ICT costs: Connectivity"));
        Assert.NotNull(target);

        page = await Client.Follow(target);

        AssertPageLayout(
            page,
            trust,
            userDefinedSet,
            spend,
            expectedQueryParams: "?viewAs=0&resultAs=1&selectedSubCategories=0",
            expectedSubCategories: BuildExpectedSubCategories(0));
    }

    [Fact]
    public async Task CanFollowClearCorrectlyUpdatesPage()
    {
        var (page, trust, userDefinedSet, spend) = await SetupNavigateInitPage(
            queryParams: "?viewAs=0&resultAs=1&selectedSubCategories=0&selectedSubCategories=1");

        var target = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Clear");
        Assert.NotNull(target);

        page = await Client.Follow(target);

        AssertPageLayout(
            page,
            trust,
            userDefinedSet,
            spend,
            expectedQueryParams: "?viewAs=0&resultAs=1");
    }

    private async Task<(IHtmlDocument page, Trust trust, UserDefinedSchoolComparatorSet userDefinedSet, TrustItSpend[] spend)> SetupNavigateInitPage(
        bool withComparatorSet = true,
        bool withEmptyComparatorSet = false,
        bool chartApiException = false,
        string queryParams = "")
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345678")
            .Create();

        var comparatorSet = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = "123"
            },
            new UserData
            {
                Type = "comparator-set",
                Id = "456"
            }
        };

        var userDefinedSet = new UserDefinedSchoolComparatorSet
        {
            Set = withEmptyComparatorSet ? [] : ["00000001", "00000002", "00000003"]
        };

        var spend = Fixture.Build<TrustItSpend>().CreateMany().ToArray();
        spend.ElementAt(0).CompanyNumber = trust.CompanyNumber;

        var horizontalBarChart = new ChartResponse
        {
            Html = "<svg />"
        };

        var client = Client
            .SetupEstablishment(trust)
            .SetupInsights()
            .SetupUserData(withComparatorSet ? comparatorSet : null)
            .SetupComparatorSet(trust, userDefinedSet)
            .SetupItSpend(trustSpend: spend)
            .SetupChartRendering<TrustComparisonDatum>(horizontalBarChart);

        if (chartApiException)
        {
            Client.SetupChartRenderingWithException<TrustComparisonDatum>();
        }

        var page = await client.Navigate($"{Paths.TrustComparisonItSpend(trust.CompanyNumber)}{queryParams}");
        return (page, trust, userDefinedSet, spend);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        Trust trust,
        UserDefinedSchoolComparatorSet userDefinedSet,
        TrustItSpend[] spend,
        int viewAs = 0,
        int resultAs = 0,
        ExpectedSubCategory[]? expectedSubCategories = null,
        bool chartError = false,
        string expectedQueryParams = "")
    {
        expectedSubCategories ??= AllSubCategories;

        DocumentAssert.AssertPageUrl(page, $"{Paths.TrustComparisonItSpend(trust.CompanyNumber)}{expectedQueryParams}".ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Benchmark your IT spending - Financial Benchmarking and Insights Tool - GOV.UK", "Benchmark your IT spending");

        var comparatorsLink = page.QuerySelector("a[data-test-id='comparators-link']");
        Assert.NotNull(comparatorsLink);
        DocumentAssert.TextEqual(comparatorsLink, $"You have chosen {userDefinedSet.Set.Length - 1} similar trusts");

        var filterSection = page.QuerySelector(".app-filter");
        Assert.NotNull(filterSection);

        AssertFilterSectionLayout(filterSection, viewAs, resultAs, expectedSubCategories);

        var subCategorySections = page.QuerySelectorAll("section");
        Assert.Equal(expectedSubCategories.Length, subCategorySections.Length);

        for (var i = 0; i < subCategorySections.Length; i++)
        {
            var section = subCategorySections[i];
            var expected = expectedSubCategories[i];

            AssertSpendingSection(section, expected, viewAs == 0, chartError);
        }
    }

    private static void AssertFilterSectionLayout(
        IElement filterSection,
        int viewAs,
        int resultAs,
        ExpectedSubCategory[] expectedSubCategories)
    {
        var applyFiltersButton = filterSection.QuerySelectorAll("button")
            .FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(applyFiltersButton);

        foreach (var subCategory in AllSubCategories)
        {
            var checkbox = filterSection.QuerySelector($"input[type='checkbox'][value='{subCategory.Id}']");
            Assert.NotNull(checkbox);

            if (expectedSubCategories.Length < AllSubCategories.Length)
            {
                var shouldBeChecked = expectedSubCategories.Any(e => e.Id == subCategory.Id);
                var isChecked = checkbox.HasAttribute("checked");

                if (shouldBeChecked)
                {
                    Assert.True(isChecked);
                }
                else
                {
                    Assert.False(isChecked);
                }
            }
        }

        // Assert ResultsAs radio exists and correctly checked
        var resultAsRadio = filterSection.QuerySelector($"input[type='radio'][name='ResultAs'][value='1']");
        Assert.NotNull(resultAsRadio);
        Assert.True(resultAsRadio.HasAttribute("checked"));

        // Assert all ViewAs radios exist and correctly checked
        for (var i = 0; i <= 1; i++)
        {
            var radio = filterSection.QuerySelector($"input[type='radio'][name='ViewAs'][value='{i}']");
            Assert.NotNull(radio);

            if (i == viewAs)
            {
                Assert.True(radio.HasAttribute("checked"));
            }
            else
            {
                Assert.False(radio.HasAttribute("checked"));
            }
        }

        if (expectedSubCategories.Length < AllSubCategories.Length)
        {
            var chipLabels = filterSection.QuerySelectorAll("a.app-filter__tag")
                .Select(c => c.TextContent.Trim())
                .ToArray();

            Assert.Equal(expectedSubCategories.Length, chipLabels.Length);

            foreach (var subCategory in expectedSubCategories)
            {
                Assert.Contains(subCategory.ChipLabel, chipLabels);
            }
        }
        else
        {
            var chips = filterSection.QuerySelectorAll("a.app-filter__tag");
            Assert.Empty(chips);
        }
    }

    private static void AssertSpendingSection(
        IElement section,
        ExpectedSubCategory expectedSubCategory,
        bool isChartView,
        bool chartError)
    {
        var sectionHeading = section.QuerySelector("h2")?.TextContent;
        Assert.NotNull(sectionHeading);
        Assert.Equal(expectedSubCategory.Heading, sectionHeading);

        if (isChartView)
        {
            AssertChartSection(section, chartError);
        }
    }

    private static void AssertChartSection(IElement chartSection, bool chartError)
    {
        var chartSvg = chartSection.QuerySelector(".ssr-chart");
        var chartWarning = chartSection.QuerySelector(".ssr-chart-warning");
        var chartContainer = chartSection.QuerySelector(".composed-container");

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
    }

    private static ExpectedSubCategory[] BuildExpectedSubCategories(params int[]? ids)
    {
        if (ids is null || ids.Length == 0)
        {
            return AllSubCategories;
        }

        return ids.Select(id => AllSubCategories.First(c => c.Id == id)).ToArray();
    }

    private record ExpectedSubCategory(string Heading, string ChipLabel, int Id);
}