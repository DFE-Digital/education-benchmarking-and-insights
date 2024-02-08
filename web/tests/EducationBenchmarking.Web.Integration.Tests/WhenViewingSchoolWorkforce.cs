using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolWorkforce(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory, output)
{
    private const string Referrer = "school-workforce";
    
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
    public async Task CanNavigateToChangeSchool(string financeType)
    {
        var (page, _) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");
        Assert.NotNull(anchor);

        page = await Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.FindOrganisation.ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToComparatorSet(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "View your comparator set");
        Assert.NotNull(anchor);

        page = await Follow(anchor);
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparatorSet(school.Urn, Referrer).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToAreasForInvestigation(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolInvestigation(school.Urn).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCurriculumPlanning(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCompareCosts(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[2].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.Urn).ToAbsolute());
    }
    
    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolWorkforce(urn));
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolWorkforce(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolWorkforce(urn));
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolWorkforce(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
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
            .SetupInsights(school, finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolWorkforce(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute()),
            ("Benchmark workforce data", Paths.SchoolWorkforce(school.Urn).ToAbsolute()),
        };
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolWorkforce(school.Urn).ToAbsolute());
        DocumentAssert.Breadcrumbs(page,expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Benchmark workforce data","Benchmark workforce data");
            
        var changeLinkElement = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");
        DocumentAssert.Link(changeLinkElement, "Change school", Paths.FindOrganisation.ToAbsolute());
            
        var viewYourComparatorLinkElement = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "View your comparator set");
        DocumentAssert.PrimaryCta(viewYourComparatorLinkElement, "View your comparator set", Paths.SchoolComparatorSet(school.Urn, Referrer));
        
        var workforceComponent = page.GetElementById("compare-workforce");
        Assert.NotNull(workforceComponent);

        var toolsSection = page.Body.SelectSingleNode("//main/div/div[4]");
        DocumentAssert.Heading2(toolsSection, "Finance tools");
        
        var toolsLinks = toolsSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(3, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "View your areas for investigation", Paths.SchoolInvestigation(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Curriculum and financial planning", Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[2], "Compare your costs", Paths.SchoolComparison(school.Urn).ToAbsolute());
    }
}