using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Domain.Content;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingHome(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies, true, false)]
    [InlineData(EstablishmentTypes.Academies, false, false)]
    [InlineData(EstablishmentTypes.Academies, true, true)]
    [InlineData(EstablishmentTypes.Maintained, false, false)]
    [InlineData(EstablishmentTypes.Maintained, false, true)]
    public async Task CanDisplay(string financeType, bool isPartOfTrust, bool showBanner)
    {
        var (page, school, _, banner) = await SetupNavigateInitPage(financeType, isPartOfTrust, showBanner: showBanner);

        AssertPageLayout(page, school, banner);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCompareYourCosts(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToComparisonItSpendIfNotPartOfTrust(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType, financeType == EstablishmentTypes.Academies);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, school.IsPartOfTrust
            ? Paths.SchoolFinancialPlanning(school.URN).ToAbsolute()
            : Paths.SchoolComparisonItSpend(school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCurriculumPlanning(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[school.IsPartOfTrust ? 1 : 2].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCensusBenchmark(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[school.IsPartOfTrust ? 2 : 3].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCensus(school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToFinancialBenchmarkingInsightsSummary(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[school.IsPartOfTrust ? 7 : 8].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolFinancialBenchmarkingInsightsSummary(school.URN, "school-home").ToAbsolute());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanNavigateToChangeSchool(bool filteredSearchFeatureEnabled)
    {
        var (page, _, _, _) = await SetupNavigateInitPage(EstablishmentTypes.Academies, false, filteredSearchFeatureEnabled);

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, filteredSearchFeatureEnabled
            ? Paths.SchoolSearch.ToAbsolute()
            : $"{Paths.FindOrganisation.ToAbsolute()}?method=school");
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolHome(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolHome(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayAppHeadlines(string financeType)
    {
        var (page, school, balance, _) = await SetupNavigateInitPage(financeType);
        AssertAppHeadlines(page, school, balance);
    }

    private async Task<(IHtmlDocument page, School school, SchoolBalance balance, Banner? banner)> SetupNavigateInitPage(
        string financeType,
        bool isPartOfTrust = false,
        bool filteredSearchFeatureEnabled = false,
        bool showBanner = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.TrustCompanyNumber, isPartOfTrust ? "12345678" : "")
            .With(x => x.FinanceType, financeType)
            .With(x => x.FederationLeadURN, string.Empty)
            .With(x => x.FederationSchools, [])
            .Create();

        var balance = Fixture.Build<SchoolBalance>()
            .With(x => x.SchoolName, school.SchoolName)
            .With(x => x.URN, school.URN)
            .With(x => x.PeriodCoveredByReturn, 12)
            .Create();

        var banner = showBanner
            ? Fixture.Create<Banner>()
            : null;

        string[] disabledFlags = [];
        if (!filteredSearchFeatureEnabled)
        {
            disabledFlags = [FeatureFlags.FilteredSearch];
        }

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        var page = await Client
            .SetupDisableFeatureFlags(disabledFlags)
            .SetupEstablishment(school)
            .SetupMetricRagRating()
            .SetupInsights()
            .SetupExpenditure(school)
            .SetupBalance(balance)
            .SetupUserData()
            .SetupBanner(banner)
            .SetupComparatorSet(school, comparatorSet)
            .SetupItSpend()
            .Navigate(Paths.SchoolHome(school.URN));

        return (page, school, balance, banner);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, Banner? banner)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(school.SchoolName);
        DocumentAssert.TitleAndH1(page, "Your school - Financial Benchmarking and Insights Tool - GOV.UK", school.SchoolName);
        if (school.IsPartOfTrust)
        {
            DocumentAssert.Heading2(page, $"Part of {school.TrustName}");
        }

        var dataSourceElement = page.QuerySelectorAll($"main > div > div:nth-child({(banner == null ? "3" : "4")}) > div > p");
        Assert.NotNull(dataSourceElement);

        if (school.IsPartOfTrust)
        {
            DocumentAssert.TextEqual(dataSourceElement.ElementAt(0), "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
            DocumentAssert.TextEqual(dataSourceElement.ElementAt(1), "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
        }
        else
        {
            DocumentAssert.TextEqual(dataSourceElement.ElementAt(0), "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
        }

        var changeLinkElement = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");

        // todo: if feature flag enabled...
        DocumentAssert.Link(changeLinkElement, "Change school", $"{Paths.FindOrganisation.ToAbsolute()}?method=school");

        var toolsSection = page.GetElementById("benchmarking-and-planning-tools"); //NB: No RAG therefore section not shown
        DocumentAssert.Heading2(toolsSection, "Benchmarking and planning tools");

        var toolsLinks = toolsSection?.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(school.IsPartOfTrust ? 3 : 4, toolsLinks?.Count);

        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(0), "Benchmark spending", Paths.SchoolComparison(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(school.IsPartOfTrust ? 1 : 2), "Curriculum and financial planning", Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(school.IsPartOfTrust ? 2 : 3), "Benchmark pupil and workforce data", Paths.SchoolCensus(school.URN).ToAbsolute());
        if (!school.IsPartOfTrust)
        {
            DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(1), "Benchmark IT spending", Paths.SchoolComparisonItSpend(school.URN).ToAbsolute());
        }

        DocumentAssert.Banner(page, banner);
    }

    private static void AssertAppHeadlines(IHtmlDocument page, School school, SchoolBalance balance)
    {
        var headlineSection = page.Body.SelectSingleNode("//main/div/div[4]");

        var headlineFiguresTexts = headlineSection.ChildNodes
            .QuerySelectorAll(".app-headline-figures")
            .Select(e => string.Join(" ", e.ChildNodes.Select(n => n.TextContent.Trim())).Trim());
        Assert.Equal([
            $"In year balance  £{balance.InYearBalance}",
            $"Revenue reserve  £{balance.RevenueReserve}",
            $"School phase  {school.OverallPhase}"
        ], headlineFiguresTexts);
    }
}