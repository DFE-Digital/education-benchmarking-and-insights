using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanning(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        AssertPageLayout(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCreateNewPlan(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        var anchor = page.QuerySelector(".govuk-button");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningStart(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanning(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanning(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanning(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanning(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }


    [Fact]
    public async Task CanNavigateToCompareYourCosts()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToCensusBenchmark()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCensus(school.URN).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.SchoolName)
            .With(x => x.Urn, school.URN)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools)
            .Navigate(Paths.SchoolFinancialPlanning(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute()),
            ("Curriculum and financial planning", Paths.SchoolFinancialPlanning(school.URN).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Curriculum and financial planning (CFP) - Financial Benchmarking and Insights Tool - GOV.UK", "Curriculum and financial planning (CFP)");

        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolFinancialPlanningStart(school.URN));

        var toolsSection = page.Body.SelectSingleNode("//main/div/div[3]");
        DocumentAssert.Heading2(toolsSection, "Finance tools");

        var toolsLinks = toolsSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(2, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "Compare your costs", Paths.SchoolComparison(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Benchmark pupil and workforce data", Paths.SchoolCensus(school.URN).ToAbsolute());
    }
}