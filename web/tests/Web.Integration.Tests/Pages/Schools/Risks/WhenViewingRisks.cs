using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.ViewModels;
using Xunit;
using LocalAuthority = Web.App.Domain.LocalAuthorities.LocalAuthority;

namespace Web.Integration.Tests.Pages.Schools.Risks;

public class WhenViewingRisks(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private const string Code = "123";
    private const string Urn = "123456";

    [Fact]
    public async Task CanDisplay()
    {
        var (page, school, indicators, metrics) = await SetupNavigateInitPage();

        AssertPageLayout(page, school, indicators, metrics);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        var page = await Client.SetupSchoolWithException()
            .Navigate(Paths.LocalAuthoritySchoolRisks(Code, Urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(
            page,
            Paths.LocalAuthoritySchoolRisks(Code, Urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFoundForEstablishment()
    {
        var page = await Client.SetupSchoolWithNotFound()
            .Navigate(Paths.LocalAuthoritySchoolRisks(Code, Urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(
            page,
            Paths.LocalAuthoritySchoolRisks(Code, Urn).ToAbsolute(),
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
            .Navigate(Paths.LocalAuthoritySchoolRisks(Code, Urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(
            page,
            Paths.LocalAuthoritySchoolRisks(Code, Urn).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school, _, _) = await SetupNavigateInitPage();

        var backLink = page.QuerySelector("a.govuk-back-link");
        Assert.NotNull(backLink);

        var newPage = await Client.Follow(backLink);

        DocumentAssert.AssertPageUrl(
            newPage,
            Paths.LocalAuthorityRisks(school.LACode).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToMethodology()
    {
        var (page, school, _, _) = await SetupNavigateInitPage();

        var methodologyLink = page.QuerySelectorAll("a.govuk-link")
            .FirstOrDefault(x => x.TextContent.Trim().Contains("Read about how we define these risk indicators"));
        Assert.NotNull(methodologyLink);

        var newPage = await Client.Follow(methodologyLink);

        DocumentAssert.AssertPageUrl(
            newPage,
            Paths.LocalAuthorityRisksMethodology(school.LACode).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToRiskHistory()
    {
        var (page, school, _, _) = await SetupNavigateInitPage();

        var historyLink = page.QuerySelectorAll("a.govuk-link")
            .FirstOrDefault(x => x.TextContent.Trim().Contains("See how these risk scores have changed over time"));
        Assert.NotNull(historyLink);

        var newPage = await Client.Follow(historyLink);

        DocumentAssert.AssertPageUrl(
            newPage,
            Paths.LocalAuthoritySchoolRisksHistory(school.LACode, school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanDownloadPageData()
    {
        var (page, school, _, _) = await SetupNavigateInitPage();

        var downloadButton = page.QuerySelectorAll("a.govuk-button")
            .FirstOrDefault(x => x.TextContent.Trim() == "Download page data");
        Assert.NotNull(downloadButton);

        var newPage = await Client.Follow(downloadButton);

        DocumentAssert.AssertPageUrl(
            newPage,
            Paths.LocalAuthoritySchoolRisksDownload(school.LACode, school.URN).ToAbsolute());
    }

    #region Methods

    private async Task<(
        IHtmlDocument page,
        School school,
        LocalAuthorityRiskIndicators indicators,
        RisksMetrics[] metrics)> SetupNavigateInitPage()
    {
        var authority = Fixture.Build<LocalAuthority>()
            .With(x => x.Code, Code)
            .Create();

        var school = Fixture.Build<School>()
            .With(x => x.URN, Urn)
            .With(x => x.LACode, authority.Code)
            .Create();

        var indicators = Fixture.Build<LocalAuthorityRiskIndicators>()
            .With(x => x.Urn, Urn)
            .With(x => x.SchoolName, school.SchoolName)
            .Create();

        var metrics = new RisksMetrics[]
        {
            // Financial Group (Decimal & Percentage)
            new()
            {
                Urn = Urn,
                RiskGroup = LocalAuthorityRisks.FinancialRiskGroup,
                RiskIndicator = "foo",
                RiskIndicatorValue = "125000.50",
                RiskIndicatorValueFormatting = RiskIndicatorValueFormatting.Decimal,
                RiskIndicatorFlag = LocalAuthorityRiskFlags.Major,
                RiskIndicatorContribution = 3.50m,
                RiskIndicatorContributionMax = 5.00m
            },
            new()
            {
                Urn = Urn,
                RiskGroup = LocalAuthorityRisks.FinancialRiskGroup,
                RiskIndicator = "bar",
                RiskIndicatorValue = "0.785",
                RiskIndicatorValueFormatting = RiskIndicatorValueFormatting.Percentage,
                RiskIndicatorFlag = LocalAuthorityRiskFlags.Minor,
                RiskIndicatorContribution = 1.00m,
                RiskIndicatorContributionMax = 5.00m
            },

            // Educational Performance Group (Percentage & String)
            new()
            {
                Urn = Urn,
                RiskGroup = LocalAuthorityRisks.EducationalPerformanceRiskGroup,
                RiskIndicator = "baz",
                RiskIndicatorValue = "0.62",
                RiskIndicatorValueFormatting = RiskIndicatorValueFormatting.Percentage,
                RiskIndicatorFlag = LocalAuthorityRiskFlags.Minor,
                RiskIndicatorContribution = 2.00m,
                RiskIndicatorContributionMax = 5.00m
            },
            new()
            {
                Urn = Urn,
                RiskGroup = LocalAuthorityRisks.EducationalPerformanceRiskGroup,
                RiskIndicator = "qux",
                RiskIndicatorValue = "quux",
                RiskIndicatorValueFormatting = RiskIndicatorValueFormatting.String,
                RiskIndicatorFlag = LocalAuthorityRiskFlags.Major,
                RiskIndicatorContribution = 4.00m,
                RiskIndicatorContributionMax = 5.00m
            },

            // School & Pupil Group (Boolean/String & Decimal)
            new()
            {
                Urn = Urn,
                RiskGroup = LocalAuthorityRisks.SchoolAndPupilRiskGroup,
                RiskIndicator = "waldo",
                RiskIndicatorValue = "Yes",
                RiskIndicatorValueFormatting = RiskIndicatorValueFormatting.Boolean,
                RiskIndicatorFlag = LocalAuthorityRiskFlags.NoFlag,
                RiskIndicatorContribution = 0.00m,
                RiskIndicatorContributionMax = 5.00m
            },
            new()
            {
                Urn = Urn,
                RiskGroup = LocalAuthorityRisks.SchoolAndPupilRiskGroup,
                RiskIndicator = "fred",
                RiskIndicatorValue = "1.12",
                RiskIndicatorValueFormatting = RiskIndicatorValueFormatting.Decimal,
                RiskIndicatorFlag = LocalAuthorityRiskFlags.Major,
                RiskIndicatorContribution = 5.00m,
                RiskIndicatorContributionMax = 5.00m
            }
        };

        var historyRows = Fixture.Build<LocalAuthorityRiskIndicatorsHistory>()
            .With(x => x.Urn, Urn)
            .With(x => x.SchoolName, school.SchoolName)
            .CreateMany(3)
            .ToArray();

        var risksHistory = Fixture.Build<LocalAuthorityRiskIndicatorsHistoryRows>()
            .With(x => x.Rows, historyRows)
            .Create();

        var pagedRisks = new PagedResults<LocalAuthorityRiskIndicators>
        {
            Results = [indicators],
            TotalResults = 1,
            Page = 1,
            PageSize = 10
        };

        var client = Client
            .SetupEstablishment(school)
            .SetupSchool(
                school,
                riskIndicators: indicators,
                riskMetrics: metrics,
                riskIndicatorsHistory: risksHistory)
            .SetupLocalAuthorityEndpoints(authority, risksResults: pagedRisks)
            .SetupInsights();

        var page = await client.Navigate(Paths.LocalAuthoritySchoolRisks(Code, Urn));

        return (page, school, indicators, metrics);
    }

    #endregion

    #region Private Assertion Helpers

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        LocalAuthorityRiskIndicators indicators,
        RisksMetrics[] metrics)
    {
        DocumentAssert.AssertPageUrl(
            page,
            Paths.LocalAuthoritySchoolRisks(school.LACode, school.URN).ToAbsolute());

        AssertTitleAndH1(page);
        AssertOverallScoreAndIntroduction(page, indicators, school);
        Assert.NotNull(school.LACode);
        AssertRiskGroupsAndMetrics(page, indicators, metrics, school.LACode);
    }

    private static void AssertTitleAndH1(IHtmlDocument page)
    {
        DocumentAssert.TitleAndH1(
            page,
            "School risk score - Financial Benchmarking and Insights Tool - GOV.UK",
            "School risk score");
    }

    private static void AssertOverallScoreAndIntroduction(
        IHtmlDocument page,
        LocalAuthorityRiskIndicators indicators,
        School school)
    {
        var scoreElement = page.QuerySelector("[data-testid='overall-risk-score']");
        Assert.NotNull(scoreElement);
        Assert.Contains($"Overall score:", scoreElement.TextContent);
        Assert.Contains($"{indicators.Overall}", scoreElement.TextContent);
        Assert.Contains($"/ {indicators.OverallMax}", scoreElement.TextContent);

        var tagElement = scoreElement.QuerySelector(".govuk-tag");
        Assert.NotNull(tagElement);
        Assert.Contains($"govuk-tag--{indicators.OverallGradeColour}", tagElement.ClassName);

        var links = page.QuerySelectorAll(".govuk-list--bullet a.govuk-link");
        Assert.Equal(3, links.Length);

        Assert.Equal(
            Paths.LocalAuthoritySchoolRisksHistory(school.LACode, school.URN),
            links[0].GetAttribute("href"));
        Assert.Equal(
            Paths.LocalAuthorityRisksMethodology(school.LACode),
            links[1].GetAttribute("href"));
        Assert.Equal(
            Paths.SchoolHome(school.URN),
            links[2].GetAttribute("href"));

        var downloadBtn = page.QuerySelector("a.govuk-button--secondary");
        Assert.NotNull(downloadBtn);
        Assert.Equal("Download page data", downloadBtn.TextContent.Trim());
    }

    private static void AssertRiskGroupsAndMetrics(
    IHtmlDocument page,
    LocalAuthorityRiskIndicators indicators,
    RisksMetrics[] metrics,
    string laCode)
    {
        var expectedGroupOrder = new[]
        {
        (Title: "Financial", Key: LocalAuthorityRisks.FinancialRiskGroup, Value: indicators.Financial, Max: indicators.FinancialMax),
        (Title: "Educational performance", Key: LocalAuthorityRisks.EducationalPerformanceRiskGroup, Value: indicators.EducationalPerformance, Max: indicators.EducationalPerformanceMax),
        (Title: "School & pupil", Key: LocalAuthorityRisks.SchoolAndPupilRiskGroup, Value: indicators.SchoolAndPupil, Max: indicators.SchoolAndPupilMax)
    };

        foreach (var expectedGroup in expectedGroupOrder)
        {
            var section = page.QuerySelector($"[data-testid='risk-group-section-{expectedGroup.Key}']");
            Assert.NotNull(section);

            // 1. Heading Assertions
            var heading = section.QuerySelector($"[data-testid='risk-group-heading-{expectedGroup.Key}']");
            Assert.NotNull(heading);
            Assert.Equal($"{expectedGroup.Title} risk", heading.TextContent.Trim());

            // 2. Score Summary Assertions
            var scoreElement = section.QuerySelector($"[data-testid='risk-group-score-{expectedGroup.Key}']");
            Assert.NotNull(scoreElement);
            Assert.Contains($"{expectedGroup.Title} risk score:", scoreElement.TextContent);
            Assert.Contains($"{expectedGroup.Value:F2} / {expectedGroup.Max:F2}", scoreElement.TextContent);

            // 3. Metric Table Assertions
            var table = section.QuerySelector($"[data-testid='risk-metric-table-{expectedGroup.Key}']");
            Assert.NotNull(table);

            var expectedMetricsForGroup = metrics
                .Where(m => string.Equals(m.RiskGroup, expectedGroup.Key, StringComparison.OrdinalIgnoreCase))
                .Select(m => new RiskMetricViewModel(m))
                .OrderBy(m => m.SortValue)
                .ThenByDescending(m => m.RiskIndicatorContribution)
                .ThenBy(m => m.RiskIndicator)
                .ToList();

            var rows = table.QuerySelectorAll("[data-testid='risk-metric-row']");
            Assert.Equal(expectedMetricsForGroup.Count, rows.Length);

            for (var j = 0; j < expectedMetricsForGroup.Count; j++)
            {
                var expectedMetric = expectedMetricsForGroup[j];
                var cells = rows[j].QuerySelectorAll("td");
                Assert.Equal(4, cells.Length);

                // Indicator
                Assert.Equal(expectedMetric.RiskIndicator, cells[0].TextContent.Trim());

                // Formatted Value
                Assert.Equal(expectedMetric.ValueDisplay, cells[1].TextContent.Trim());

                // Flag Tag
                if (expectedMetric.RiskIndicatorFlag == LocalAuthorityRiskFlags.NoFlag)
                {
                    Assert.Equal(LocalAuthorityRiskFlags.NoFlag, cells[2].TextContent.Trim());
                }
                else
                {
                    var tag = cells[2].QuerySelector("strong.govuk-tag");
                    Assert.NotNull(tag);
                    Assert.Equal(expectedMetric.RiskIndicatorFlag, tag.TextContent.Trim());
                    Assert.Contains(expectedMetric.TagColourClass, tag.ClassName);
                }

                // Contribution
                var expectedContribution = $"{expectedMetric.RiskIndicatorContribution:F2} / {expectedMetric.RiskIndicatorContributionMax:F2}";
                Assert.Equal(expectedContribution, cells[3].TextContent.Trim());
            }

            AssertRiskGroupDetails(section, expectedGroup.Key, laCode);
        }
    }

    private static void AssertRiskGroupDetails(IElement sectionContainer, string groupKey, string laCode)
    {
        var detailsElements = sectionContainer.QuerySelectorAll("details.govuk-details");

        switch (groupKey)
        {
            case LocalAuthorityRisks.FinancialRiskGroup:
                Assert.Equal(2, detailsElements.Length);

                var staffSpendElement = detailsElements[0];
                Assert.Contains("What we mean by % spend on staff", staffSpendElement.TextContent);
                Assert.Contains("% spend on teaching, supply, education support, back office and other staff.", staffSpendElement.TextContent);

                var methodologyLink = staffSpendElement.QuerySelector("a.govuk-link");
                Assert.NotNull(methodologyLink);
                Assert.Equal("Read about how we define all risk indicators and RAG-rate the scores", methodologyLink.TextContent.Trim());
                Assert.Equal(Paths.LocalAuthorityRisksMethodology(laCode), methodologyLink.GetAttribute("href"));

                var sourcesElement = detailsElements[1];
                Assert.Contains("Where this data comes from", sourcesElement.TextContent);

                var financialBulletItems = sourcesElement.QuerySelectorAll("ul.govuk-list--bullet li");
                Assert.Equal(2, financialBulletItems.Length);
                Assert.Equal("Consistent Financial Reporting (CFR)", financialBulletItems[0].TextContent.Trim());
                Assert.Equal("Pupil numbers come from the spring school census", financialBulletItems[1].TextContent.Trim());
                break;

            case LocalAuthorityRisks.EducationalPerformanceRiskGroup:
                var edPerformanceElement = Assert.Single(detailsElements);
                Assert.Contains("Where this data comes from", edPerformanceElement.TextContent);

                var edPerformanceBulletItems = edPerformanceElement.QuerySelectorAll("ul.govuk-list--bullet li");
                Assert.Equal(3, edPerformanceBulletItems.Length);
                Assert.Equal("Key stage 2 performance data", edPerformanceBulletItems[0].TextContent.Trim());
                Assert.Equal("Key stage 4 performance data", edPerformanceBulletItems[1].TextContent.Trim());
                Assert.Equal("Primary and secondary school applications and offers", edPerformanceBulletItems[2].TextContent.Trim());
                break;

            case LocalAuthorityRisks.SchoolAndPupilRiskGroup:
                var schoolPupilElement = Assert.Single(detailsElements);
                Assert.Contains("Where this data comes from", schoolPupilElement.TextContent);

                var schoolPupilBulletItems = schoolPupilElement.QuerySelectorAll("ul.govuk-list--bullet li");
                Assert.Equal(3, schoolPupilBulletItems.Length);
                Assert.Equal("Pupil absence in schools in England", schoolPupilBulletItems[0].TextContent.Trim());
                Assert.Equal("Pupil numbers come from the spring school census", schoolPupilBulletItems[1].TextContent.Trim());
                Assert.Equal("School capacity", schoolPupilBulletItems[2].TextContent.Trim());
                break;
        }
    }

    #endregion
}
