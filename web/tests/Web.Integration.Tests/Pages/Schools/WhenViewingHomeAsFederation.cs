using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingHomeAsFederation(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true, false, false, false)]
    [InlineData(true, true, true, false)]
    [InlineData(true, true, true, true)]
    [InlineData(false, false, false, false)]
    [InlineData(false, true, true, false)]
    [InlineData(false, true, true, true)]
    public async Task CanDisplay(bool isNonLeadFederation, bool ks4ProgressBandingEnabled, bool hasProgressIndicator, bool withUserDefinedUserData)
    {
        var (page, school, _) = await SetupNavigateInitPage(isNonLeadFederation, ks4ProgressBandingEnabled, hasProgressIndicator, withUserDefinedUserData);

        AssertPageLayout(page, school, ks4ProgressBandingEnabled, hasProgressIndicator, withUserDefinedUserData);
    }

    [Fact]
    public async Task CanNavigateToCompareYourCosts()
    {
        var (page, school, _) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToComparisonItSpend()
    {
        var (page, school, _) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparisonItSpend(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToCurriculumPlanning()
    {
        var (page, school, _) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[2].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToCensusBenchmark()
    {
        var (page, school, _) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[3].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCensus(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToFinancialBenchmarkingInsightsSummary()
    {
        var (page, school, _) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[8].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolFinancialBenchmarkingInsightsSummary(school.URN, "school-home").ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToChangeSchool()
    {
        var (page, _, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearch.ToAbsolute());
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

    [Fact]
    public async Task CanDisplayAppHeadlines()
    {
        var (page, school, balance) = await SetupNavigateInitPage();
        AssertAppHeadlines(page, school, balance);
    }

    private async Task<(IHtmlDocument page, School school, SchoolBalance balance)> SetupNavigateInitPage(
        bool isNonLeadFederation = false,
        bool ks4ProgressBandingEnabled = true,
        bool hasProgressIndicator = true,
        bool withUserDefinedUserData = false)
    {
        var federationLeadSchool = new FederationSchool
        {
            URN = "123456",
            SchoolName = "Test School"
        };
        var federationSchools = Fixture.Build<FederationSchool>().CreateMany(3).Append(federationLeadSchool).ToArray();

        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.SchoolName, "Test School")
            .With(x => x.TrustCompanyNumber, string.Empty)
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .With(x => x.FederationLeadURN, isNonLeadFederation ? "678900" : "123456")
            .With(x => x.FederationSchools, isNonLeadFederation ? [] : federationSchools)
            .Create();

        var balance = Fixture.Build<SchoolBalance>()
            .With(x => x.SchoolName, school.SchoolName)
            .With(x => x.URN, school.URN)
            .With(x => x.PeriodCoveredByReturn, 12)
            .Create();

        var census = Fixture.Build<App.Domain.Census>()
            .With(x => x.SchoolName, school.SchoolName)
            .With(x => x.URN, school.URN)
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        var characteristic = Fixture.Build<SchoolCharacteristic>()
                    .With(x => x.KS4ProgressBanding, hasProgressIndicator ? "Well above average" : null)
                    .Create();

        var userDefinedSetUserData = new[]
        {
                    new UserData
                    {
                        Type = "comparator-set",
                        Id = "456"
                    }
                };

        var userDefinedRatings = withUserDefinedUserData ? CreateRagRatings(school.URN!) : [];

        string[] features = ks4ProgressBandingEnabled ? [] : [FeatureFlags.KS4ProgressBanding, FeatureFlags.KS4ProgressBandingSchoolHome];
        var page = await Client
            .SetupDisableFeatureFlags(features)
            .SetupEstablishment(school)
            .SetupMetricRagRating([], userDefinedRatings)
            .SetupInsights()
            .SetupExpenditure(school)
            .SetupBalance(balance)
            .SetupUserData(withUserDefinedUserData ? userDefinedSetUserData : null)
            .SetupCensus(school, census)
            .SetupComparatorSet(school, comparatorSet)
            .SetupItSpend()
            .SetupSchoolInsight(characteristic)
            .Navigate(Paths.SchoolHome(school.URN));

        return (page, school, balance);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        bool ks4ProgressBandingEnabled = true,
        bool hasProgressIndicator = true,
        bool withUserDefinedUserData = false)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute());

        Assert.NotNull(school.SchoolName);
        DocumentAssert.TitleAndH1(page, "Your school - Financial Benchmarking and Insights Tool - GOV.UK", school.SchoolName);

        var changeLinkElement = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");

        DocumentAssert.Link(changeLinkElement, "Change school", Paths.SchoolSearch.ToAbsolute());

        // assertions for non lead federation schools
        if (school.FederationLeadURN != school.URN)
        {
            var message = page.QuerySelector("main > div > div:nth-of-type(2) > div > p:first-of-type");
            Assert.NotNull(message);
            DocumentAssert.TextEqual(
                message,
                "This school's finance data is not displayed, as it's part of a federated budget. The full federated data is shown on the federation page.",
                true);

            var federationLeadCta = message.QuerySelector("a");
            DocumentAssert.Link(federationLeadCta, "federation page", Paths.SchoolHome(school.FederationLeadURN).ToAbsolute());

            var giasLink = page.QuerySelector(".govuk-link[data-custom-event-id='gias-school-details']");
            Assert.NotNull(giasLink);
            DocumentAssert.Link(giasLink, "Get more information about this school Opens in a new window", $"https://www.get-information-schools.service.gov.uk/establishments/establishment/details/{school.URN}");

        }
        // assertions for lead federation schools
        else
        {
            var message = page.QuerySelector("main > div > div:nth-child(3) > div > p");
            Assert.NotNull(message);
            DocumentAssert.TextEqual(
                message,
                "This school is the lead school of its federation. The following schools are part of this federation:");

            var federationSchoolList = page.QuerySelectorAll("main > div > div:nth-child(4) > div > ul > li");
            Assert.NotNull(federationSchoolList);

            foreach (var federationSchoolItem in federationSchoolList)
            {
                var anchor = federationSchoolItem.QuerySelector("a");
                var federationSchool =
                    school.FederationSchools.FirstOrDefault(x => x.SchoolName == federationSchoolItem.TextContent.Trim());
                Assert.NotNull(federationSchool);
                Assert.NotNull(federationSchool.SchoolName);
                DocumentAssert.Link(anchor, federationSchool.SchoolName, Paths.SchoolHome(federationSchool.URN).ToAbsolute());
            }

            var dataSourceElement = page.QuerySelector("div[data-test-id='data-source-wrapper']");
            Assert.NotNull(dataSourceElement);
            AssertDataSource(dataSourceElement, school, ks4ProgressBandingEnabled, hasProgressIndicator, withUserDefinedUserData);

            var toolsSection = page.GetElementById("benchmarking-and-planning-tools");
            DocumentAssert.Heading2(toolsSection, "Benchmarking and planning tools");

            var toolsLinks = toolsSection?.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
            Assert.Equal(4, toolsLinks?.Count);

            DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(0), "Benchmark spending", Paths.SchoolComparison(school.URN).ToAbsolute());
            DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(1), "Benchmark IT spending", Paths.SchoolComparisonItSpend(school.URN).ToAbsolute());
            DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(2), "Curriculum and financial planning", Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
            DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(3), "Benchmark pupil and workforce data", Paths.SchoolCensus(school.URN).ToAbsolute());
        }
    }

    private static void AssertAppHeadlines(IHtmlDocument page, School school, SchoolBalance balance)
    {
        var headlineSection = page.Body.SelectSingleNode("//main/div/div[5]");

        var headlineFiguresTexts = headlineSection.ChildNodes
            .QuerySelectorAll(".app-headline-figures")
            .Select(e => string.Join(" ", e.ChildNodes.Select(n => n.TextContent.Trim())).Trim());
        Assert.Equal([
            $"In year balance  £{balance.InYearBalance}",
            $"Revenue reserve  £{balance.RevenueReserve}",
            $"School phase  {school.OverallPhase}"
        ], headlineFiguresTexts);
    }

    private static void AssertDataSource(
        IElement dataSourceElement,
        School school,
        bool ks4ProgressBandingEnabled,
        bool hasProgressIndicator,
        bool withUserDefinedUserData)
    {
        string? additionalText = null;
        if (withUserDefinedUserData)
        {
            additionalText = "You are now comparing with your chosen schools.";
        }

        switch (ks4ProgressBandingEnabled)
        {
            case true when hasProgressIndicator:
                {
                    SchoolDocumentAssert.AssertMaintainedSchoolWithIndicators(dataSourceElement, school.URN!, additionalText);
                    break;
                }
            case true:
                {
                    SchoolDocumentAssert.AssertMaintainedSchoolNoIndicators(dataSourceElement, additionalText);
                    break;
                }
            default:
                {
                    SchoolDocumentAssert.AssertMaintainedSchoolNoBanding(dataSourceElement, additionalText);
                    break;
                }
        }
    }

    private RagRating[] CreateRagRatings(string urn)
    {
        var random = new Random();
        var statusKeys = Lookups.StatusPriorityMap.Keys.ToList();
        return Category.All
            .Select(category => Fixture.Build<RagRating>()
                .With(r => r.Category, category)
                .With(r => r.RAG, () => statusKeys[random.Next(statusKeys.Count)])
                .With(r => r.URN, urn)
                .Create())
            .ToArray();
    }
}