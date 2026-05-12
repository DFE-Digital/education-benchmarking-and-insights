using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using Web.App.Extensions;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeedsSpending(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    #region Tests

    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority, expenditures) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority, expenditures);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupLocalAuthorityEndpointsWithException()
            .Navigate(Paths.LocalAuthorityHighNeedsSpending(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsSpending(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFoundForEstablishment()
    {
        const string code = "123";
        var page = await Client.SetupLocalAuthorityEndpointsWithNotFound()
            .Navigate(Paths.LocalAuthorityHighNeedsSpending(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsSpending(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(0, "?viewAs=0&resultAs=0&type=0")]
    [InlineData(1, "?viewAs=1&resultAs=0&type=0")]
    public async Task CanSubmitOptionsForViewAs(int viewAs, string expectedQueryParams)
    {
        var (page, authority, expenditures) = await SetupNavigateInitPage(expectedQueryParams);

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
            expenditures,
            viewAs: viewAs,
            expectedQueryParams: expectedQueryParams);
    }

    [Theory]
    [InlineData(0, "?viewAs=0&resultAs=0&type=0")]
    [InlineData(1, "?viewAs=0&resultAs=1&type=0")]
    [InlineData(2, "?viewAs=0&resultAs=2&type=0")]
    [InlineData(3, "?viewAs=0&resultAs=3&type=0")]
    public async Task CanSubmitOptionsForResultsAsAs(int resultAs, string expectedQueryParams)
    {
        var (page, authority, expenditures) = await SetupNavigateInitPage(expectedQueryParams);

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply");
        Assert.NotNull(action);
        var form = action.Closest("form");
        Assert.NotNull(form);
        page = await Client.SubmitForm(form, action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "ResultAs", resultAs.ToString() }
            });
        });

        AssertPageLayout(
            page,
            authority,
            expenditures,
            resultAs: resultAs,
            expectedQueryParams: expectedQueryParams);
    }


    [Fact]
    public async Task CanDisplayChartWarningWhenChartApiFails()
    {
        var (page, authority, expenditures) = await SetupNavigateInitPage(chartApiError: true);

        AssertPageLayout(
            page,
            authority,
            expenditures,
            viewAs: (int)Views.ViewAsOptions.Chart,
            chartApiError: true);
    }

    [Theory]
    [InlineData(1, 0, "?selectedSubCategories=1&viewAs=0&resultAs=0&type=0")]
    [InlineData(1, 1, "?selectedSubCategories=1&viewAs=0&resultAs=0&type=1")]
    [InlineData(10, 0, "?selectedSubCategories=10&viewAs=0&resultAs=0&type=0")]
    [InlineData(20, 1, "?selectedSubCategories=20&viewAs=0&resultAs=0&type=1")]
    [InlineData(20, 0, "?selectedSubCategories=20&viewAs=0&resultAs=0&type=0")]
    public async Task CanSubmitFilterOptionsForSubCategoriesAndType(int expectedSubCategoryId, int type, string expectedQueryParams)
    {
        var (page, authority, expenditures) = await SetupNavigateInitPage();

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "SelectedSubCategories", expectedSubCategoryId.ToString() },
                { "Type", type.ToString() }
            });
        });

        AssertPageLayout(
            page,
            authority,
            expenditures,
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

        DocumentAssert.AssertPageUrl(newPage, $"{Paths.LocalAuthorityHighNeedsSpendingDownload(authority.Code)}?resultAs=0&type=0".ToAbsolute());
    }

    [Theory]
    [InlineData(Views.ViewAsOptions.Chart, true)]
    [InlineData(Views.ViewAsOptions.Table, false)]
    public async Task CanSaveChartImages(Views.ViewAsOptions viewAs, bool expected)
    {
        var (page, _, _) = await SetupNavigateInitPage(queryParams: $"?viewAs={(int)viewAs}");

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
        HighNeedsSpending[] expenditures)> SetupNavigateInitPage(
        string queryParams = "",
        bool chartApiError = false)
    {
        var authority = Fixture.Build<LocalAuthority>()
            .With(a => a.Code, "123")
            .Create();

        var expenditures = Fixture.Build<HighNeedsSpending>()
            .CreateMany(3).ToArray();

        Assert.NotNull(authority.Code);

        var horizontalBarChart = new ChartResponse
        {
            Html = "<svg />"
        };

        var client = Client.SetupInsights()
            .SetupLocalAuthorityEndpoints(authority, highNeedsSpendings: expenditures)
            .SetupLocalAuthoritiesComparators(authority.Code, ["123", "124"])
            .SetupChartRendering<HighNeedsSpendingComparisonDatum>(horizontalBarChart);

        if (chartApiError)
        {
            Client.SetupChartRenderingWithException<HighNeedsSpendingComparisonDatum>();
        }

        var page = await client.Navigate($"{Paths.LocalAuthorityHighNeedsSpending(authority.Code)}{queryParams}");

        return (page, authority, expenditures);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        LocalAuthority authority,
        HighNeedsSpending[] expenditures,
        int viewAs = 0,
        int resultAs = 0,
        string expectedQueryParams = "",
        SubCategoryId[]? expectedSubCategories = null,
        bool chartApiError = false)
    {
        expectedSubCategories ??= AllSubCategories;

        DocumentAssert.AssertPageUrl(page, $"{Paths.LocalAuthorityHighNeedsSpending(authority.Code)}{expectedQueryParams}".ToAbsolute());

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Benchmark high needs spending - Financial Benchmarking and Insights Tool - GOV.UK", "Benchmark high needs spending");

        var form = page.QuerySelector(".options-form");
        Assert.NotNull(form);

        AssertFormOptions(form, viewAs, resultAs);

        var filterSection = page.QuerySelector(".app-filter");
        Assert.NotNull(filterSection);

        AssertFilterSection(filterSection, expectedSubCategories);

        if (viewAs == 0)
        {
            AssertChartSection(page, chartApiError);
        }
        else
        {
            AssertTableSection(page, expenditures);
        }
    }

    private static void AssertFormOptions(
        IElement form,
        int viewAs = 0,
        int resultsAs = 0)
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

        var resultAsContainer = form.QuerySelector("#ResultAs");
        Assert.NotNull(resultAsContainer);

        var options = resultAsContainer.QuerySelectorAll("option");
        Assert.Equal(HighNeedsDimensions.AllResultsAsOptions.Length, options.Length);

        foreach (var result in HighNeedsDimensions.AllResultsAsOptions)
        {
            var value = ((int)result).ToString();

            var option = options.FirstOrDefault(o => o.GetAttribute("value") == value);
            Assert.NotNull(option);

            var shouldBeSelected = resultsAs == (int)result;
            var isSelected = option.HasAttribute("selected");

            Assert.Equal(shouldBeSelected, isSelected);
        }
    }

    private static void AssertFilterSection(
        IElement filterSection,
        SubCategoryId[] expectedSubCategories)
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

    private static void AssertTableSection(IHtmlDocument page, HighNeedsSpending[] expenditures)
    {
        var sections = page.QuerySelectorAll("[data-testid=\"subcategory-section\"]");
        Assert.NotEmpty(sections);

        foreach (var section in sections)
        {
            var match = Sections.Single(s => section.Id!.Contains(s.Id));

            var rows = section.QuerySelectorAll("tbody tr");
            Assert.Equal(expenditures.Length, rows.Length);

            foreach (var row in rows)
            {
                var cells = row.TableCells();
                var expected = expenditures.Single(p => p.Name == cells[0]);

                Assert.Equal(cells[0], expected.Name);
                Assert.Equal(cells[1], expected.TotalPupils.ToString());

                var value = match.Select(expected);
                Assert.Equal(cells[2], value?.ToCurrency());
            }
        }
    }

    private static void AssertChartSection(IHtmlDocument page, bool chartError = false)
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

    private static readonly SubCategoryId[] AllSubCategories =
        Enumerable.Range(0, 31).Select(i => new SubCategoryId(i)).ToArray();


    private record SubCategoryId(int Id);

    private static SubCategoryId[] BuildExpectedSubCategories(params int[]? ids)
    {
        if (ids is null || ids.Length == 0)
        {
            return AllSubCategories;
        }

        return ids.Select(id => AllSubCategories.First(c => c.Id == id)).ToArray();
    }

    private static readonly (string Id, Func<HighNeedsSpending, decimal?> Select)[] Sections =
[
    ("highneeds-subcategory-total-place-funding-for-special-schools-and-apprus",
        s => s.TotalPlaceFunding),
    ("highneeds-subcategory-top-up-funding-maintained-schools-academies-free-schools-and-colleges",
        s => s.TotalTopUpFundingMaintained),
    ("highneeds-subcategory-top-up-funding-non-maintained-schools-academies-free-schools-and-colleges",
        s => s.TotalTopUpFundingNonMaintained),
    ("highneeds-subcategory-sen-support-and-inclusion-services",
        s => s.TotalSenServices),
    ("highneeds-subcategory-alternative-provision-services",
        s => s.TotalAlternativeProvisionServices),
    ("highneeds-subcategory-hospital-education-services",
        s => s.TotalHospitalServices),
    ("highneeds-subcategory-therapies-and-other-health-related-services",
        s => s.TotalOtherHealthServices),
    ("highneeds-subcategory-primary-place-funding",
        s => s.PlaceFundingPrimary),
    ("highneeds-subcategory-secondary-place-funding",
        s => s.PlaceFundingSecondary),
    ("highneeds-subcategory-special-place-funding",
        s => s.PlaceFundingSpecial),
    ("highneeds-subcategory-pru-and-alternative-provision-place-funding",
        s => s.PlaceFundingAlternativeProvision),
    ("highneeds-subcategory-early-years-top-up-funding-maintained",
        s => s.TopFundingMaintainedEarlyYears),
    ("highneeds-subcategory-primary-top-up-funding-maintained",
        s => s.TopFundingMaintainedPrimary),
    ("highneeds-subcategory-secondary-top-up-funding-maintained",
        s => s.TopFundingMaintainedSecondary),
    ("highneeds-subcategory-special-top-up-funding-maintained",
        s => s.TopFundingMaintainedSpecial),
    ("highneeds-subcategory-alternative-provision-top-up-funding-maintained",
        s => s.TopFundingMaintainedAlternativeProvision),
    ("highneeds-subcategory-post-school-top-up-funding-maintained",
        s => s.TopFundingMaintainedPostSchool),
    ("highneeds-subcategory-top-up-funding-income-maintained",
        s => s.TopFundingMaintainedIncome),
    ("highneeds-subcategory-early-years-top-up-funding-non-maintained",
        s => s.TopFundingNonMaintainedEarlyYears),
    ("highneeds-subcategory-primary-top-up-funding-non-maintained",
        s => s.TopFundingNonMaintainedPrimary),
    ("highneeds-subcategory-secondary-top-up-funding-non-maintained",
        s => s.TopFundingNonMaintainedSecondary),
    ("highneeds-subcategory-special-top-up-funding-non-maintained",
        s => s.TopFundingNonMaintainedSpecial),
    ("highneeds-subcategory-alternative-provision-top-up-funding-non-maintained",
        s => s.TopFundingNonMaintainedAlternativeProvision),
    ("highneeds-subcategory-post-school-top-up-funding-non-maintained",
        s => s.TopFundingNonMaintainedPostSchool),
    ("highneeds-subcategory-top-up-funding-income-non-maintained",
        s => s.TopFundingNonMaintainedIncome),
    ("highneeds-subcategory-home-to-school-transport-pre-16",
        s => s.HometoSchoolTransportPre16),
    ("highneeds-subcategory-home-to-post-16-provision-16-18",
        s => s.HometoSchoolTransport1618),
    ("highneeds-subcategory-home-to-post-16-provision-19-25",
        s => s.HometoSchoolTransport1925),
    ("highneeds-subcategory-sen-transport-la-managed",
        s => s.SenTransportDsg),
    ("highneeds-subcategory-educational-psychology-service",
        s => s.EdPsychologyService),
    ("highneeds-subcategory-sen-admin",
        s => s.SenAdmin)
];

    #endregion
}