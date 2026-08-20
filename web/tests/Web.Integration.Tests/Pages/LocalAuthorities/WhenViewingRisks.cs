using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Xunit;
using LocalAuthority = Web.App.Domain.LocalAuthorities.LocalAuthority;
using LocalAuthoritySchool = Web.App.Domain.LocalAuthorities.LocalAuthoritySchool;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingRisks(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    #region Tests

    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority, results) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority, results);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupLocalAuthorityEndpointsWithException()
            .Navigate(Paths.LocalAuthorityRisks(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityRisks(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFoundForEstablishment()
    {
        const string code = "123";
        var page = await Client.SetupLocalAuthorityEndpointsWithNotFound()
            .Navigate(Paths.LocalAuthorityRisks(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityRisks(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("Primary")]
    [InlineData("Secondary")]
    [InlineData("All phases")]
    public async Task CanSelectPhase(string phase)
    {
        var expectedQuery = $"?sortField=Overall&sortOrder=desc&selectedPhaseOption={phase.Replace(" ", "%20")}";
        var (page, authority, results) = await SetupNavigateInitPage();

        var form = page.QuerySelector("form[role='search']");
        Assert.NotNull(form);

        var applyButton = form.QuerySelector("button");
        Assert.NotNull(applyButton);

        page = await Client.SubmitForm(form, applyButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "SelectedPhaseOption", phase }
            });
        });

        AssertPageLayout(page, authority, results, selectedPhaseOption: phase, expectedQueryParams: expectedQuery);
    }

    [Fact]
    public async Task CanPaginateNextPage()
    {
        var (page, authority, _) = await SetupNavigateInitPage();

        var nextLink = page.QuerySelector(".govuk-pagination__next a.govuk-pagination__link");
        Assert.NotNull(nextLink);

        var newPage = await Client.Follow(nextLink);

        DocumentAssert.AssertPageUrl(newPage,
            $"{Paths.LocalAuthorityRisks(authority.Code)}?sortField=Overall&sortOrder=desc&page=2&selectedPhaseOption=All%20phases".ToAbsolute());
    }

    [Fact]
    public async Task CanPaginatePreviousPage()
    {
        var (page, authority, _) = await SetupNavigateInitPage("?page=2");

        var prevLink = page.QuerySelector(".govuk-pagination__prev a.govuk-pagination__link");
        Assert.NotNull(prevLink);

        var newPage = await Client.Follow(prevLink);

        DocumentAssert.AssertPageUrl(
            newPage,
            $"{Paths.LocalAuthorityRisks(authority.Code)}?sortField=Overall&sortOrder=desc&page=1&selectedPhaseOption=All%20phases".ToAbsolute());
    }


    [Fact]
    public async Task CanGoBackToLocalAuthorityHome()
    {
        var (page, authority, _) = await SetupNavigateInitPage("?page=2");

        var backLink = page.QuerySelector(".govuk-back-link");
        Assert.NotNull(backLink);

        var newPage = await Client.Follow(backLink);

        DocumentAssert.AssertPageUrl(newPage, $"{Paths.LocalAuthorityHome(authority.Code)}".ToAbsolute());
    }

    [Fact]
    public async Task CanDownloadPageData()
    {
        var (page, authority, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a.govuk-button")
            .FirstOrDefault(x => x.TextContent.Trim() == "Download page data");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage,
            $"{Paths.LocalAuthorityRisksDownload(authority.Code)}?selectedPhaseOption=All%20phases".ToAbsolute());
    }

    #endregion

    #region Methods

    private async Task<(IHtmlDocument page, LocalAuthority authority, LocalAuthorityRiskIndicators[] results)>
        SetupNavigateInitPage(string queryParams = "")
    {
        var phaseTypes = new[] { "Primary", "Secondary" };
        var schools = phaseTypes
            .SelectMany(GenerateSchools)
            .ToArray();
        var authority = Fixture.Build<LocalAuthority>()
            .With(a => a.Code, "123")
            .Create();
        authority.Schools = schools;

        var results = schools.Select(s =>
            Fixture.Build<LocalAuthorityRiskIndicators>()
                .With(r => r.Urn, s.URN)
                .Create())
            .ToArray();


        var paged = new PagedResults<LocalAuthorityRiskIndicators>
        {
            Results = results,
            TotalResults = 50,
            Page = queryParams.Contains("page=2") ? 2 : 1,
            PageSize = 10
        };

        var client = Client.SetupLocalAuthorityEndpoints(authority, risksResults: paged)
            .SetupInsights();

        var page = await client.Navigate($"{Paths.LocalAuthorityRisks(authority.Code)}{queryParams}");

        return (page, authority, results);
    }

    private LocalAuthoritySchool[] GenerateSchools(string phaseType)
    {
        return Fixture.Build<LocalAuthoritySchool>()
            .With(x => x.OverallPhase, phaseType)
            .CreateMany(10)
            .ToArray();
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        LocalAuthority authority,
        LocalAuthorityRiskIndicators[] results,
        string sortField = "Overall",
        string sortOrder = "desc",
        string selectedPhaseOption = "All phases",
        string expectedQueryParams = "")
    {
        DocumentAssert.AssertPageUrl(
            page,
            $"{Paths.LocalAuthorityRisks(authority.Code)}{expectedQueryParams}".ToAbsolute());

        DocumentAssert.TitleAndH1(
            page,
            "Schools risk overview - Financial Benchmarking and Insights Tool - GOV.UK",
            "Schools risk overview");

        AssertPhaseSelector(page, selectedPhaseOption);

        AssertSortHeaders(page, sortField, sortOrder);

        AssertTable(page, results);

        AssertPagination(page);
    }

    private static void AssertSortHeaders(IHtmlDocument page, string sortField, string sortOrder)
    {
        var headers = page.QuerySelectorAll("th");

        Assert.NotEmpty(headers);

        foreach (var header in headers)
        {
            var field = header.GetAttribute("data-sort-field");
            var order = header.GetAttribute("data-sort-order");

            if (field == sortField)
            {
                Assert.Equal(sortOrder, order);
            }
        }
    }

    private static void AssertPhaseSelector(IHtmlDocument page, string selected)
    {
        var select = page.QuerySelector("#SelectedPhaseOption");
        Assert.NotNull(select);

        var options = select.QuerySelectorAll("option");
        Assert.Contains(options, o => o.GetAttribute("value") == selected && o.HasAttribute("selected"));
    }

    private static void AssertTable(IHtmlDocument page, LocalAuthorityRiskIndicators[] results)
    {
        var headerRow = page.QuerySelector("thead tr");
        Assert.NotNull(headerRow);

        var headers = headerRow.QuerySelectorAll("th");
        Assert.Equal(6, headers.Length);

        Assert.Equal("School name", headers[0].TextContent.Trim());
        Assert.Equal("URN", headers[1].TextContent.Trim());
        Assert.Equal("Overall", headers[2].TextContent.Trim());
        Assert.Equal("Financial", headers[3].TextContent.Trim());
        Assert.Equal("Pupil & school", headers[4].TextContent.Trim());
        Assert.Equal("Educational performance", headers[5].TextContent.Trim());

        foreach (var th in headers)
        {
            var button = th.QuerySelector("button");
            Assert.NotNull(button);

            Assert.Equal("CurrentSort", button.GetAttribute("name"));

            var value = button.GetAttribute("value");
            Assert.NotNull(value);
            Assert.Contains("~", value);

            Assert.NotNull(th.GetAttribute("aria-sort"));
            Assert.NotNull(button.GetAttribute("aria-pressed"));
        }

        var rows = page.QuerySelectorAll("tbody tr");
        Assert.Equal(results.Length, rows.Length);
    }

    private static void AssertPagination(IHtmlDocument page)
    {
        var next = page.QuerySelector(".govuk-pagination__next a.govuk-pagination__link");
        Assert.NotNull(next);
    }

    #endregion
}
