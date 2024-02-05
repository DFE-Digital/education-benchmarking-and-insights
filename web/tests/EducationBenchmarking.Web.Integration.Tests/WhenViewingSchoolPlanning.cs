using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolPlanning(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory,
        output)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplaySchool(string financeType)
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
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningStart(school.Urn).ToAbsolute());
    }


    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolCurriculumPlanning(urn));

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanning(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolCurriculumPlanning(urn));

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanning(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCompareYourCosts(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(school.Urn).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToAreasForInvestigation(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolInvestigation(school.Urn).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToWorkforceBenchmark(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[2].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolWorkforce(school.Urn).ToAbsolute());
    }
    
    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();
        
        var page = await SetupEstablishment(school)
            .SetupInsights(school,finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolCurriculumPlanning(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute()),
            ("Curriculum and financial planning", Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Curriculum and financial planning (CFP)","Curriculum and financial planning (CFP)");

        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Create new plan", Paths.SchoolCurriculumPlanningStart(school.Urn));
        
        var toolsSection = page.Body.SelectSingleNode("//main/div/div[3]");
        DocumentAssert.Heading2(toolsSection, "Finance tools");
        
        var toolsLinks = toolsSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(3, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "Compare your costs", Paths.SchoolComparison(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "View your areas for investigation", Paths.SchoolInvestigation(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[2], "Benchmark workforce data", Paths.SchoolWorkforce(school.Urn).ToAbsolute());
        
    }
}