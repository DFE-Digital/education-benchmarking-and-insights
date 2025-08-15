using System.Net;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Comparison;

public class WhenViewingComparisonItSpend(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplayForMaintainedSchool()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanDisplayChartWarningForMaintainedSchoolWhenChartApiFails()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, true);

        AssertPageLayout(page, school, chartError: true);
    }

    [Fact]
    public async Task CanDisplayNotFoundForAcademy()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparisonItSpend(school.URN).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanNavigateToSchoolComparators()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector("a[data-test-id='comparators-link']");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparators(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolComparisonItSpend(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparisonItSpend(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolComparisonItSpend(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparisonItSpend(urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(0, "?viewAs=0&resultAs=0")]
    [InlineData(1, "?viewAs=1&resultAs=0")]
    public async Task CanSubmitFilterOptionsForViewAs(int viewAs, string expectedQueryParams)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "ViewAs", viewAs.ToString()
                }
            });
        });

        AssertPageLayout(
            page,
            school,
            viewAs: viewAs,
            expectedQueryParams: expectedQueryParams);
    }

    [Theory]
    [InlineData(0, "?viewAs=0&resultAs=0")]
    [InlineData(1, "?viewAs=0&resultAs=1")]
    [InlineData(2, "?viewAs=0&resultAs=2")]
    [InlineData(3, "?viewAs=0&resultAs=3")]
    public async Task CanSubmitFilterOptionsForResultsAs(int resultAs, string expectedQueryParams)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "ResultAs", resultAs.ToString()
                }
            });
        });

        AssertPageLayout(
            page,
            school,
            resultAs: resultAs,
            expectedQueryParams: expectedQueryParams);
    }

    [Theory]
    [InlineData(0, "?viewAs=0&resultAs=0&selectedSubCategories=0")]
    [InlineData(1, "?viewAs=0&resultAs=0&selectedSubCategories=1")]
    [InlineData(2, "?viewAs=0&resultAs=0&selectedSubCategories=2")]
    [InlineData(3, "?viewAs=0&resultAs=0&selectedSubCategories=3")]
    [InlineData(4, "?viewAs=0&resultAs=0&selectedSubCategories=4")]
    [InlineData(5, "?viewAs=0&resultAs=0&selectedSubCategories=5")]
    [InlineData(6, "?viewAs=0&resultAs=0&selectedSubCategories=6")]
    public async Task CanSubmitFilterOptionsForSubCategories(int expectedSubCategoryId, string expectedQueryParams)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "SelectedSubCategories", expectedSubCategoryId.ToString()
                }
            });
        });

        AssertPageLayout(
            page,
            school,
            expectedSubCategories: BuildExpectedSubCategories(expectedSubCategoryId),
            expectedQueryParams: expectedQueryParams);
    }

    [Fact]
    public async Task CanFollowChipsCorrectlyUpdatesPage()
    {
        var (page, school) = await SetupNavigateInitPage(
            EstablishmentTypes.Maintained,
            queryParams: "?viewAs=0&resultAs=0&selectedSubCategories=0&SelectedSubCategories=1");

        var target = page.QuerySelectorAll("a.app-filter__tag")
            .FirstOrDefault(el => el.TextContent.Contains("Connectivity (E20A)"));
        Assert.NotNull(target);

        page = await Client.Follow(target);

        AssertPageLayout(
            page,
            school,
            expectedQueryParams: "?ViewAs=0&ResultAs=0&SelectedSubCategories=AdministrationSoftwareSystems",
            expectedSubCategories: BuildExpectedSubCategories(0));
    }

    [Fact]
    public async Task CanFollowClearCorrectlyUpdatesPage()
    {
        var (page, school) = await SetupNavigateInitPage(
            EstablishmentTypes.Maintained,
            queryParams: "?viewAs=0&resultAs=0&selectedSubCategories=0&SelectedSubCategories=1");

        var target = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Clear");
        Assert.NotNull(target);

        page = await Client.Follow(target);

        AssertPageLayout(
            page,
            school,
            expectedQueryParams: "?ViewAs=0&ResultAs=0");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task SuppressNegativeOrZeroWarningCorrectlyShown(bool hasNegativeValues)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        var spend = Fixture.Build<SchoolItSpend>()
            .CreateMany()
            .ToArray();
        spend.ElementAt(0).URN = school.URN;

        if (hasNegativeValues)
        {
            spend.ElementAt(1).AdministrationSoftwareAndSystems = -1;
        }

        var horizontalBarChart = new ChartResponse { Html = "<svg />" };

        var client = Client
            .SetupEstablishment(school)
            .SetupComparatorSet(school, comparatorSet)
            .SetupItSpend(spend)
            .SetupChartRendering<SchoolComparisonDatum>(horizontalBarChart);

        var page = await client.Navigate(Paths.SchoolComparisonItSpend(school.URN));

        var adminSoftwareSection = page.QuerySelector("#cost-sub-category-administration-software-and-systems-e20d");
        Assert.NotNull(adminSoftwareSection);
        var warningParagraph = adminSoftwareSection.QuerySelectorAll("p").FirstOrDefault();

        if (hasNegativeValues)
        {
            Assert.NotNull(warningParagraph);
            DocumentAssert.TextEqual(warningParagraph, "Only displaying schools with positive expenditure.");
        }
        else
        {
            Assert.Null(warningParagraph);
        }
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(
        string financeType,
        bool chartApiException = false,
        string queryParams = "")
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        var spend = Fixture.Build<SchoolItSpend>()
            .CreateMany()
            .ToArray();
        spend.ElementAt(0).URN = school.URN;

        var horizontalBarChart = new ChartResponse { Html = "<svg />" };

        var client = Client
            .SetupEstablishment(school)
            .SetupComparatorSet(school, comparatorSet)
            .SetupItSpend(spend)
            .SetupChartRendering<SchoolComparisonDatum>(horizontalBarChart);

        if (chartApiException)
        {
            Client.SetupChartRenderingWithException<SchoolComparisonDatum>();
        }

        var page = await client.Navigate($"{Paths.SchoolComparisonItSpend(school.URN)}{queryParams}");
        return (page, school);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        int viewAs = 0,
        int resultAs = 0,
        ExpectedSubCategory[]? expectedSubCategories = null,
        bool chartError = false,
        string expectedQueryParams = "")
    {
        expectedSubCategories ??= AllSubCategories;

        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, $"{Paths.SchoolComparisonItSpend(school.URN)}{expectedQueryParams}".ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Benchmark your IT spending - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark your IT spending");

        var filterSection = page.QuerySelector(".app-filter");
        Assert.NotNull(filterSection);

        AssertFilterSectionLayout(filterSection, viewAs, resultAs, expectedSubCategories);

        var subCategorySections = page.QuerySelectorAll("section");
        Assert.Equal(expectedSubCategories.Length, subCategorySections.Length);

        for (var i = 0; i < subCategorySections.Length; i++)
        {
            var section = subCategorySections[i];
            var expected = expectedSubCategories[i];

            AssertChartSection(section, expected, isChartView: viewAs == 0, chartError);
        }
    }

    private static void AssertFilterSectionLayout(
        IElement filterSection,
        int viewAs,
        int resultAs,
        ExpectedSubCategory[] expectedSubCategories)
    {
        var applyFiltersButton = filterSection.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
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

        // Assert all ResultsAs radios exist and correctly checked
        for (var i = 0; i <= 3; i++)
        {
            var radio = filterSection.QuerySelector($"input[type='radio'][name='ResultAs'][value='{i}']");
            Assert.NotNull(radio);

            if (i == resultAs)
            {
                Assert.True(radio.HasAttribute("checked"));
            }
            else
            {
                Assert.False(radio.HasAttribute("checked"));
            }
        }

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

    private static void AssertChartSection(
        IElement chartSection,
        ExpectedSubCategory expectedSubCategory,
        bool isChartView,
        bool chartError)
    {
        var sectionHeading = chartSection.QuerySelector("h2")?.TextContent;
        Assert.NotNull(sectionHeading);
        Assert.Equal(expectedSubCategory.Heading, sectionHeading);

        if (isChartView)
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
        else
        {
            var table = chartSection.QuerySelector(".govuk-table");
            Assert.NotNull(table);
        }
    }

    private record ExpectedSubCategory(string Heading, string ChipLabel, int Id);

    private static readonly ExpectedSubCategory[] AllSubCategories =
    [
        new ExpectedSubCategory("Administration software and systems E20D", "Administration software and systems (E20D)", 0),
        new ExpectedSubCategory("Connectivity E20A", "Connectivity (E20A)", 1),
        new ExpectedSubCategory("IT learning resources E20C", "IT learning resources (E20C)", 2),
        new ExpectedSubCategory("IT support E20G", "IT support (E20G)", 3),
        new ExpectedSubCategory("Laptops, desktops and tablets E20E", "Laptops, desktops and tablets (E20E)", 4),
        new ExpectedSubCategory("Onsite servers E20B", "Onsite servers (E20B)", 5),
        new ExpectedSubCategory("Other hardware E20F", "Other hardware (E20F)", 6)
    ];

    private static ExpectedSubCategory[] BuildExpectedSubCategories(params int[]? ids)
    {
        if (ids is null || ids.Length == 0)
            return AllSubCategories;

        return ids.Select(id => AllSubCategories.First(c => c.Id == id)).ToArray();
    }
}