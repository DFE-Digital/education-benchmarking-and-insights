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

        var form = page.QuerySelector(".actions-form");
        Assert.NotNull(form);

        AssertFormOptions(form, viewAs);

        var filterSection = page.QuerySelector(".app-filter");
        Assert.NotNull(filterSection);

        AssertFilterSection(filterSection, viewAs, expectedSubCategories);

        if (viewAs == 0)
        {
            //TODO: asserts chart once implemented
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
        var tables = page.QuerySelectorAll(".govuk-table");
        Assert.NotNull(tables);

        foreach (var table in tables)
        {
            var heading = table.QuerySelector("thead th:nth-child(2)")!.TextContent.Trim();

            // get required prop from table heading to later extract expected value from plan
            Assert.True(HeadingToValue.TryGetValue(heading, out var extractor),
                $"Unknown heading: {heading}");

            var rows = table.QuerySelectorAll("tbody tr");
            Assert.Equal(plans.Length, rows.Length);

            foreach (var row in rows)
            {
                var cells = row.TableCells();

                var expected = plans.FirstOrDefault(g => g.Name == cells[0]);
                Assert.NotNull(expected);

                Assert.Equal(cells[0], expected.Name);
                Assert.Equal(cells[1], extractor(expected).ToString());
                Assert.Equal(cells[2], expected.TotalPupils.ToString());
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

    private static readonly Dictionary<string, Func<EducationHealthCarePlans, decimal?>> HeadingToValue =
        new()
        {
            ["Total pupils with EHC plans (per 1000 pupils)"] = p => p.Total,
            ["EHC plans in Mainstream schools or academies (per 1000 pupils)"] = p => p.Mainstream,
            ["EHC plans in Resourced provision or SEN units (per 1000 pupils)"] = p => p.Resourced,
            ["EHC plans in Maintained special school or special academies (per 1000 pupils)"] = p => p.Special,
            ["EHC plans in NMSS or independent schools (per 1000 pupils)"] = p => p.Independent,
            ["EHC plans in Hospital schools or alternative provisions (per 1000 pupils)"] = p => p.Hospital,
            ["EHC plans in Post 16 (per 1000 pupils)"] = p => p.Post16,
            ["EHC plans in other types of provisions (per 1000 pupils)"] = p => p.Other
        };

    #endregion
}