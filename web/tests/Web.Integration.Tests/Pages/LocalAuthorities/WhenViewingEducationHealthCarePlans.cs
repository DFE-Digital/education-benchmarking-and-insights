using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingEducationHealthCarePlans(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    #region Tests

    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority, plans) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority, plans);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupLocalAuthorityEndpointsWithException()
            .Navigate(Paths.LocalAuthorityEducationHealthCarePlans(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityEducationHealthCarePlans(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFoundForEstablishment()
    {
        const string code = "123";
        var page = await Client.SetupLocalAuthorityEndpointsWithNotFound()
            .Navigate(Paths.LocalAuthorityEducationHealthCarePlans(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityEducationHealthCarePlans(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(0, "?viewAs=0")]
    [InlineData(1, "?viewAs=1")]
    public async Task CanSubmitOptionsForViewAs(int viewAs, string expectedQueryParams)
    {
        var (page, authority, plans) = await SetupNavigateInitPage(expectedQueryParams);

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply");
        Assert.NotNull(action);
        var form = action.Closest("form");
        Assert.NotNull(form);
        page = await Client.SubmitForm(form, action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "ViewAs", viewAs.ToString() }
            });
        });

        AssertPageLayout(
            page,
            authority,
            plans,
            viewAs: viewAs,
            expectedQueryParams: expectedQueryParams);
    }


    [Fact]
    public async Task CanDisplayChartWarningWhenChartApiFails()
    {
        var (page, authority, plans) = await SetupNavigateInitPage(chartApiError: true);

        AssertPageLayout(
            page,
            authority,
            plans,
            viewAs: (int)Views.ViewAsOptions.Chart,
            chartApiError: true);
    }

    [Theory]
    [InlineData(0, "?selectedSubCategories=0&viewAs=0")]
    [InlineData(1, "?selectedSubCategories=1&viewAs=0")]
    [InlineData(2, "?selectedSubCategories=2&viewAs=0")]
    [InlineData(3, "?selectedSubCategories=3&viewAs=0")]
    [InlineData(4, "?selectedSubCategories=4&viewAs=0")]
    [InlineData(5, "?selectedSubCategories=5&viewAs=0")]
    [InlineData(6, "?selectedSubCategories=6&viewAs=0")]
    public async Task CanSubmitFilterOptionsForSubCategories(int expectedSubCategoryId, string expectedQueryParams)
    {
        var (page, authority, plans) = await SetupNavigateInitPage();

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
            authority,
            plans,
            expectedSubCategories: BuildExpectedSubCategories(expectedSubCategoryId),
            expectedQueryParams: expectedQueryParams);
    }

    [Fact]
    public async Task CanDownloadPageData()
    {
        var (page, authority, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a.govuk-button")
            .FirstOrDefault(x => x.TextContent.Trim() == "Download page data");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.LocalAuthorityEducationHealthCarePlansDownload(authority.Code).ToAbsolute());
    }

    [Theory]
    [InlineData(Views.ViewAsOptions.Chart, true)]
    [InlineData(Views.ViewAsOptions.Table, false)]
    public async Task CanSaveChartImages(Views.ViewAsOptions viewAs, bool expected)
    {
        var (page, authority, _) = await SetupNavigateInitPage(queryParams: $"?viewAs={(int)viewAs}");

        var button = page.QuerySelector("#page-actions-button");
        if (expected)
        {
            Assert.NotNull(button);
        }
        else
        {
            Assert.Null(button);
        }
    }

    #endregion

    #region Methods

    private async Task<(
        IHtmlDocument page,
        LocalAuthority authority,
        EducationHealthCarePlans[] plans)> SetupNavigateInitPage(
        string queryParams = "",
        bool chartApiError = false)
    {
        var authority = Fixture.Build<LocalAuthority>()
            .With(a => a.Code, "123")
            .Create();

        var plans = Fixture.Build<EducationHealthCarePlans>()
            .CreateMany(3).ToArray();

        Assert.NotNull(authority.Code);

        var horizontalBarChart = new ChartResponse
        {
            Html = "<svg />"
        };

        var client = Client.SetupInsights()
            .SetupLocalAuthorityEndpoints(authority, plans)
            .SetupLocalAuthoritiesComparators(authority.Code, ["123", "124"])
            .SetupChartRendering<EducationHealthCarePlansComparisonDatum>(horizontalBarChart);

        if (chartApiError)
        {
            Client.SetupChartRenderingWithException<EducationHealthCarePlansComparisonDatum>();
        }

        var page = await client.Navigate($"{Paths.LocalAuthorityEducationHealthCarePlans(authority.Code)}{queryParams}");

        return (page, authority, plans);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        LocalAuthority authority,
        EducationHealthCarePlans[] plans,
        int viewAs = 0,
        string expectedQueryParams = "",
        ExpectedSubCategory[]? expectedSubCategories = null,
        bool chartApiError = false)
    {
        expectedSubCategories ??= AllSubCategories;

        DocumentAssert.AssertPageUrl(page, $"{Paths.LocalAuthorityEducationHealthCarePlans(authority.Code)}{expectedQueryParams}".ToAbsolute());

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Benchmark education, health care plans - Financial Benchmarking and Insights Tool - GOV.UK", "Benchmark education, health care plans");

        var form = page.QuerySelector(".options-form");
        Assert.NotNull(form);

        AssertFormOptions(form, viewAs);

        var filterSection = page.QuerySelector(".app-filter");
        Assert.NotNull(filterSection);

        AssertFilterSection(filterSection, viewAs, expectedSubCategories);

        if (viewAs == 0)
        {
            AssertChartSection(page, plans, chartApiError);
        }
        else
        {
            AssertTableSection(page, plans);
        }
    }

    private static void AssertFormOptions(
        IElement form,
        int viewAs = 0)
    {
        var viewAsContainer = form.QuerySelector("#ViewAs");
        Assert.NotNull(viewAsContainer);

        var radioInputs = viewAsContainer.QuerySelectorAll("input[type='radio']");
        Assert.Equal(Views.All.Length, radioInputs.Length);

        foreach (var view in Views.All)
        {
            var value = ((int)view).ToString();

            var input = radioInputs.FirstOrDefault(x => x.GetAttribute("value") == value);
            Assert.NotNull(input);

            var shouldBeChecked = viewAs == (int)view;
            var isChecked = input.HasAttribute("checked");

            Assert.Equal(shouldBeChecked, isChecked);
        }
    }

    private static void AssertFilterSection(
        IElement filterSection,
        int viewAs,
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
    }

    private static void AssertTableSection(IHtmlDocument page, EducationHealthCarePlans[] plans)
    {
        var sections = page.QuerySelectorAll("[id*=\"cost-sub-category-\"]");
        Assert.NotEmpty(sections);

        foreach (var section in sections)
        {
            Assert.NotNull(section.Id);
            var match = Sections.Single(s => section.Id.Contains(s.Id));

            var rows = section.QuerySelectorAll("tbody tr");
            Assert.Equal(plans.Length, rows.Length);

            foreach (var row in rows)
            {
                var cells = row.TableCells();
                var expected = plans.Single(p => p.Name == cells[0]);

                Assert.Equal(cells[0], expected.Name);
                Assert.Equal(cells[1], expected.TotalPupils.ToString());
                var value = match.Select(expected);
                Assert.Equal(cells[2], value?.ToString());
            }
        }
    }

    private static void AssertChartSection(IHtmlDocument page, EducationHealthCarePlans[] plans, bool chartError = false)
    {
        var charts = page.QuerySelectorAll(".benchmarking-chart-container");
        Assert.NotNull(charts);

        foreach (var chartSection in charts)
        {
            var chartSvg = chartSection.QuerySelector(".ssr-chart");
            var chartWarning = chartSection.QuerySelector(".ssr-chart-warning");

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
        }
    }

    private record ExpectedSubCategory(string Heading, string ChipLabel, int Id);

    private static readonly ExpectedSubCategory[] AllSubCategories =
    [
        new("Total EHC plans", "Total pupils with EHC plans", 0),
        new("Mainstream schools or academies", "Placement of pupils with EHC plans in mainstream schools or academies", 1),
        new("Resourced provision or SEN units", "Placement of pupils with EHC plans resourced provision or SEN units", 2),
        new("Maintained special school or special academies", "Placement of pupils with EHC plans maintained special school or special academies", 3),
        new("NMSS or independent schools", "Placement of pupils with EHC plans NMSS or independent schools", 4),
        new("Hospital schools or alternative provisions", "Placement of pupils with EHC plans in hospital schools or alternative provisions", 5),
        new("Post 16", "Placement of pupils with EHC plans in post 16", 6),
        new("Other", "Placement of pupils with EHC plans in other types of provisions", 7)
    ];

    private static ExpectedSubCategory[] BuildExpectedSubCategories(params int[]? ids)
    {
        if (ids is null || ids.Length == 0)
        {
            return AllSubCategories;
        }

        return ids.Select(id => AllSubCategories.First(c => c.Id == id)).ToArray();
    }

    private static readonly (string Id, Func<EducationHealthCarePlans, decimal?> Select)[] Sections =
    [
        ("cost-sub-category-total-ehc-plans", p => p.Total),
        ("cost-sub-category-ehc-plans-supported-in-mainstream-schools-or-academies", p => p.Mainstream),
        ("cost-sub-category-ehc-plans-supported-in-resourced-provision-or-sen-units", p => p.Resourced),
        ("cost-sub-category-ehc-plans-supported-in-maintained-special-schools-or-special-academies", p => p.Special),
        ("cost-sub-category-ehc-plans-supported-in-nmss-or-independent-schools", p => p.Independent),
        ("cost-sub-category-ehc-plans-supported-in-hospital-schools-or-alternative-provisions", p => p.Hospital),
        ("cost-sub-category-ehc-plans-supported-in-post-16", p => p.Post16),
        ("cost-sub-category-ehc-plans-supported-in-other-settings", p => p.Other)
    ];

    #endregion
}