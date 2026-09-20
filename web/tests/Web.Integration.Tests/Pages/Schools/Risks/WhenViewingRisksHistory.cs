using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using Web.App.ViewModels;
using Xunit;
using LocalAuthority = Web.App.Domain.LocalAuthorities.LocalAuthority;

namespace Web.Integration.Tests.Pages.Schools.Risks;

public class WhenViewingRisksHistory(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private const string Code = "123";
    private const string Urn = "123456";

    [Fact]
    public async Task CanDisplay()
    {
        var (page, school, risksHistoryRows) = await SetupNavigateInitPage();

        AssertPageLayout(page, school, risksHistoryRows);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        var page = await Client.SetupSchoolWithException()
            .Navigate(Paths.LocalAuthoritySchoolRisksHistory(Code, Urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(
            page,
            Paths.LocalAuthoritySchoolRisksHistory(Code, Urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFoundForEstablishment()
    {
        var page = await Client.SetupSchoolWithNotFound()
            .Navigate(Paths.LocalAuthoritySchoolRisksHistory(Code, Urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(
            page,
            Paths.LocalAuthoritySchoolRisksHistory(Code, Urn).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayNotFoundWhenSchoolDoesNotBelongToLocalAuthority()
    {
        const string otherCode = "999";

        var school = Fixture.Build<School>()
            .With(x => x.URN, Urn)
            .With(x => x.LACode, otherCode)
            .Create();

        var page = await Client.SetupSchool(school)
            .Navigate(Paths.LocalAuthoritySchoolRisksHistory(Code, Urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(
            page,
            Paths.LocalAuthoritySchoolRisksHistory(Code, Urn).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school, _) = await SetupNavigateInitPage();

        var backLink = page.QuerySelector("a.govuk-back-link");
        Assert.NotNull(backLink);

        var newPage = await Client.Follow(backLink);

        DocumentAssert.AssertPageUrl(newPage, Paths.LocalAuthoritySchoolRisks(school.LACode, school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(Views.ViewAsOptions.Chart, "?viewAs=0")]
    [InlineData(Views.ViewAsOptions.Table, "?viewAs=1")]
    public async Task CanSubmitOptionsForViewAs(Views.ViewAsOptions viewAs, string expectedQueryParams)
    {
        var (page, school, risksHistoryRows) = await SetupNavigateInitPage();

        var form = page.QuerySelector(".options-form");
        Assert.NotNull(form);

        var action = form.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { nameof(SchoolRisksHistoryViewModel.ViewAs), ((int)viewAs).ToString() }
            });
        });

        AssertPageLayout(
            page,
            school,
            risksHistoryRows,
            viewAs: viewAs,
            expectedQueryParams: expectedQueryParams);
    }

    [Fact]
    public async Task CanDisplayChartWarningWhenChartApiFails()
    {
        var (page, school, risksHistoryRows) = await SetupNavigateInitPage(chartApiError: true);

        AssertPageLayout(
            page,
            school,
            risksHistoryRows,
            viewAs: Views.ViewAsOptions.Chart,
            chartApiError: true);
    }

    [Fact]
    public async Task CanDownloadPageData()
    {
        var (page, school, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a.govuk-button")
            .FirstOrDefault(x => x.TextContent.Trim() == "Download page data");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(
            newPage,
            Paths.LocalAuthoritySchoolRisksHistoryDownload(school.LACode, school.URN).ToAbsolute());
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

    #region Methods

    private async Task<(
        IHtmlDocument page,
        School school,
        LocalAuthorityRiskIndicatorsHistoryRows risksHistoryRows)> SetupNavigateInitPage(
        string queryParams = "",
        bool chartApiError = false)
    {
        var authority = Fixture.Build<LocalAuthority>()
            .With(x => x.Code, Code)
            .Create();

        var school = Fixture.Build<School>()
            .With(x => x.URN, Urn)
            .With(x => x.LACode, authority.Code)
            .Create();

        var rows = Fixture.Build<LocalAuthorityRiskIndicatorsHistory>()
            .With(x => x.Urn, Urn)
            .With(x => x.SchoolName, school.SchoolName)
            .CreateMany(5)
            .ToArray();

        var risksHistoryRows = Fixture.Build<LocalAuthorityRiskIndicatorsHistoryRows>()
            .With(x => x.Rows, rows)
            .Create();

        var lineChart = new ChartResponse
        {
            Html = "<svg />"
        };

        var client = Client
            .SetupEstablishment(school)
            .SetupSchool(school, riskIndicatorsHistory: risksHistoryRows)
            .SetupChartRendering<RiskHistoryData>(lineChart);

        if (chartApiError)
        {
            Client.SetupChartRenderingWithException<RiskHistoryData>();
        }

        var page = await client.Navigate($"{Paths.LocalAuthoritySchoolRisksHistory(Code, Urn)}{queryParams}");

        return (page, school, risksHistoryRows);
    }

    #endregion

    #region Private Assertion Helpers

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        LocalAuthorityRiskIndicatorsHistoryRows risksHistoryRows,
        Views.ViewAsOptions viewAs = Views.ViewAsOptions.Chart,
        bool chartApiError = false,
        string expectedQueryParams = "")
    {
        DocumentAssert.AssertPageUrl(
            page,
            $"{Paths.LocalAuthoritySchoolRisksHistory(school.LACode, school.URN)}{expectedQueryParams}".ToAbsolute());

        AssertTitleAndH1(page);
        AssertPageActions(page, viewAs);

        if (viewAs == Views.ViewAsOptions.Chart)
        {
            AssertChartSection(page, chartApiError);
        }
        else
        {
            AssertTableSection(page, risksHistoryRows);
        }
    }

    private static void AssertTitleAndH1(IHtmlDocument page)
    {
        DocumentAssert.TitleAndH1(
            page,
            "Trend in risk scores - Financial Benchmarking and Insights Tool - GOV.UK",
            "Trend in risk scores");
    }

    private static void AssertPageActions(IHtmlDocument page, Views.ViewAsOptions selectedViewAs)
    {
        var form = page.QuerySelector(".options-form");
        Assert.NotNull(form);

        var viewAsContainer = form.QuerySelector($"#{nameof(SchoolRisksHistoryViewModel.ViewAs)}");
        Assert.NotNull(viewAsContainer);

        var radioInputs = viewAsContainer.QuerySelectorAll("input[type='radio']");
        Assert.Equal(Views.All.Length, radioInputs.Length);

        foreach (var view in Views.All)
        {
            var value = ((int)view).ToString();
            var input = radioInputs.FirstOrDefault(x => x.GetAttribute("value") == value);
            Assert.NotNull(input);

            var shouldBeChecked = selectedViewAs == view;
            var isChecked = input.HasAttribute("checked");
            Assert.Equal(shouldBeChecked, isChecked);
        }

        var applyButton = form.QuerySelector("button[type='submit']");
        Assert.NotNull(applyButton);
        Assert.Equal("Apply", applyButton.TextContent.Trim());

        var downloadLink = page.QuerySelector("a.govuk-button[href*='history/download']");
        Assert.NotNull(downloadLink);
        Assert.Equal("Download page data", downloadLink.TextContent.Trim());
    }

    private static void AssertChartSection(IHtmlDocument page, bool chartApiError)
    {
        var expectedTitles = new[]
        {
            "Overall risk score",
            "Financial risk score",
            "Educational performance risk score*",
            "School & pupil risk score"
        };

        var sections = page.QuerySelectorAll("h2.govuk-heading-m")
            .Select(h => h.TextContent.Trim())
            .ToArray();

        foreach (var title in expectedTitles)
        {
            Assert.Contains(title, sections);
        }

        var chartContainers = page.QuerySelectorAll(".costs-chart-container");
        var chartWarnings = page.QuerySelectorAll(".ssr-chart-warning");

        if (chartApiError)
        {
            Assert.Empty(chartContainers);
            Assert.Equal(expectedTitles.Length, chartWarnings.Length);
        }
        else
        {
            Assert.Equal(expectedTitles.Length, chartContainers.Length);
            Assert.Empty(chartWarnings);

            foreach (var container in chartContainers)
            {
                var svg = container.QuerySelector(".ssr-chart");
                Assert.NotNull(svg);
            }
        }
    }

    private static void AssertTableSection(
        IHtmlDocument page,
        LocalAuthorityRiskIndicatorsHistoryRows risksHistoryRows)
    {
        var expectedTitles = new[]
        {
            "Overall risk score",
            "Financial risk score",
            "Educational performance risk score*",
            "School & pupil risk score"
        };

        var headings = page.QuerySelectorAll("h2[data-testid='risk-history-heading']");
        Assert.Equal(expectedTitles.Length, headings.Length);

        for (var i = 0; i < expectedTitles.Length; i++)
        {
            Assert.Equal(expectedTitles[i], headings[i].TextContent.Trim());
        }

        var tables = page.QuerySelectorAll("table.govuk-table");
        Assert.Equal(expectedTitles.Length, tables.Length);

        var trends = risksHistoryRows.ToTrends();
        var trendSeries = new[]
        {
            trends.Overall,
            trends.Financial,
            trends.EducationalPerformance,
            trends.SchoolAndPupil
        };

        for (var i = 0; i < expectedTitles.Length; i++)
        {
            var table = tables[i];
            var series = trendSeries[i];

            var header = table.QuerySelector("thead tr th.govuk-table__header--numeric");
            Assert.NotNull(header);

            var expectedMaxFormatted = series.MaxValue.ToString($"0.{new string('0', 2)}");
            Assert.Contains($"Score (out of {expectedMaxFormatted})", header.TextContent);

            var rows = table.QuerySelectorAll("tbody tr");
            var expectedData = series.Data.ToArray();
            Assert.Equal(expectedData.Length, rows.Length);

            for (var j = 0; j < expectedData.Length; j++)
            {
                var cells = rows[j].QuerySelectorAll("td");
                Assert.Equal(2, cells.Length);
                Assert.Equal(expectedData[j].Year, cells[0].TextContent.Trim());
                Assert.Equal(expectedData[j].Value.ToString($"0.{new string('0', 2)}"), cells[1].TextContent.Trim());
            }
        }
    }

    #endregion
}
