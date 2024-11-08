using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingHomeAsFederation(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplay(bool isNonLeadFederation)
    {
        var (page, school) = await SetupNavigateInitPage(isNonLeadFederation);

        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanNavigateToCompareYourCosts()
    {
        var (page, school) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToCurriculumPlanning()
    {
        var (page, school) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToCensusBenchmark()
    {
        var (page, school) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[2].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCensus(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToBenchmarkingReportCards()
    {
        var (page, school) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[3].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolBenchmarkingReportCards(school.URN, "school-home").ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToChangeSchool()
    {
        var (page, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, $"{Paths.FindOrganisation.ToAbsolute()}?method=school");
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolHome(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolHome(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(bool isNonLeadFederation = false)
    {
        var federationLeadSchool = new FederationSchool { URN = "12345", SchoolName = "Test School" };
        var federationSchools = Fixture.Build<FederationSchool>().CreateMany(3).Append(federationLeadSchool).ToArray();

        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.SchoolName, "Test School")
            .With(x => x.TrustCompanyNumber, string.Empty)
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .With(x => x.OfstedDescription, "0")
            .With(x => x.FederationLeadURN, isNonLeadFederation ? "67890" : "12345")
            .With(x => x.FederationSchools, isNonLeadFederation ? [] : federationSchools)
            .Create();

        var balance = Fixture.Build<SchoolBalance>()
            .With(x => x.SchoolName, school.SchoolName)
            .With(x => x.URN, school.URN)
            .With(x => x.PeriodCoveredByReturn, 12)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupMetricRagRating()
            .SetupInsights()
            .SetupExpenditure(school)
            .SetupBalance(balance)
            .SetupUserData()
            .Navigate(Paths.SchoolHome(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute());

        Assert.NotNull(school.SchoolName);
        DocumentAssert.TitleAndH1(page, "Your school - Financial Benchmarking and Insights Tool - GOV.UK", school.SchoolName);

        var changeLinkElement = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");
        DocumentAssert.Link(changeLinkElement, "Change school", $"{Paths.FindOrganisation.ToAbsolute()}?method=school");

        // assertions for non lead federation schools
        if (school.FederationLeadURN != school.URN)
        {
            var message = page.QuerySelector("main > div > div:nth-of-type(2) > div > p:first-of-type"); ;
            Assert.NotNull(message);
            DocumentAssert.TextEqual(
                message,
                "This school’s finance data is not displayed, as it’s part of a federated budget. The full federated data is shown on the federation page.");

            var federationLeadCta = message.QuerySelector("a");
            DocumentAssert.Link(federationLeadCta, "federation page", Paths.SchoolHome(school.FederationLeadURN).ToAbsolute());
        }
        // assertions for lead federation schools
        else
        {
            var message = page.QuerySelector("main > div > div:nth-child(3) > div > p"); ;
            Assert.NotNull(message);
            DocumentAssert.TextEqual(
                message,
                "This school is the lead school of its federation. The following schools are part of this federation:");

            var federationSchoolList = page.QuerySelectorAll("main > div > div:nth-child(4) > div > ul > li"); ;
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

            var dataSourceElement = page.QuerySelector("main > div > div:nth-child(4) > div > p");
            Assert.NotNull(dataSourceElement);
            DocumentAssert.TextEqual(dataSourceElement, "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");

            var toolsSection = page.Body.SelectSingleNode("//main/div/div[6]");
            DocumentAssert.Heading2(toolsSection, "Benchmarking and planning tools");

            var toolsLinks = toolsSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
            Assert.Equal(4, toolsLinks.Count);

            DocumentAssert.Link(toolsLinks[0], "Benchmark spending", Paths.SchoolComparison(school.URN).ToAbsolute());
            DocumentAssert.Link(toolsLinks[1], "Curriculum and financial planning", Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
            DocumentAssert.Link(toolsLinks[2], "Benchmark pupil and workforce data", Paths.SchoolCensus(school.URN).ToAbsolute());
            DocumentAssert.Link(toolsLinks[3], "Benchmarking report cards", Paths.SchoolBenchmarkingReportCards(school.URN, "school-home").ToAbsolute());
        }
    }
}