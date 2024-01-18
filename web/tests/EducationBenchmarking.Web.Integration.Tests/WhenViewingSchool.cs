using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchool : BenchmarkingWebAppClient
{
    public WhenViewingSchool(BenchmarkingWebAppFactory factory) : base(factory)
    {
    }
    
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
    public async Task CanNavigateToCompareYourCosts(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.Urn).ToAbsolute());
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
        var anchor = liElements[2].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());
    }
    
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToWorkforceBenchmark(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[3].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolWorkforce(school.Urn).ToAbsolute());
    }
    
    [Fact]
    public async Task CanDisplayNotFound()
    {
        var page = await SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolHome("12345"));
        
        DocumentAssert.AssertPageUrl(page, Paths.StatusError(404).ToAbsolute());
    }
    
    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolHome("12345"));
        
        DocumentAssert.AssertPageUrl(page, Paths.StatusError(500).ToAbsolute());
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
            .Navigate(Paths.SchoolHome(school.Urn));

        return (page, school);
    }
    
    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.Urn).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Your school", "Education benchmarking and insights");
        DocumentAssert.Heading2(page, school.Name);

        var toolsSection = page.Body.SelectSingleNode("//main/div[7]");
        DocumentAssert.Heading2(toolsSection, "Finance tools");

        var toolsLinks = toolsSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(4, toolsLinks.Count);
        
        DocumentAssert.Link(toolsLinks[0], "Compare your costs", Paths.SchoolComparison(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "View your areas for investigation", Paths.SchoolInvestigation(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[2], "Curriculum and financial planning", Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[3], "Benchmark workforce data", Paths.SchoolWorkforce(school.Urn).ToAbsolute());
    }
}