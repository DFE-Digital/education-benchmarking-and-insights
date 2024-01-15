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

    [Fact]
    public async Task CanDisplayAcademy()
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, EstablishmentTypes.Academies)
            .Create();
            
        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();
            
        var page = await SetupEstablishment(school)
            .SetupInsightsFromAcademy(school,finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolHome(school.Urn));
            
        AssertPageLayout(page, school);
    }
    
    [Fact]
    public async Task CanDisplayMaintainedSchool()
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();
            
        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();
            
        var page = await SetupEstablishment(school)
            .SetupInsightsFromMaintainedSchool(school,finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolHome(school.Urn));
            
        AssertPageLayout(page, school);
    }
    
    [Fact]
    public async Task CanNavigateToCompareYourCosts()
    {
        //TODO : Add test case to click on and follow compare your costs link
        Assert.True(true);
    }
    
    [Fact]
    public async Task CanNavigateToAreasForInvestigation()
    {
        //TODO : Add test case to click on and follow view your areas for investigation link
        Assert.True(true);
    }
    
    [Fact]
    public async Task CanNavigateToCurriculumPlanning()
    {
        //TODO : Add test case to click on and follow curriculum and financial planning link
        Assert.True(true);
    }
    
    [Fact]
    public async Task CanNavigateToWorkforceBenchmark()
    {
        //TODO : Add test case to click on and follow benchmark workforce data link
        Assert.True(true);
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
    
    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute())
        };
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.Urn).ToAbsolute());
        DocumentAssert.Breadcrumbs(page,expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Your school","Education benchmarking and insights");
        DocumentAssert.Heading2(page, school.Name);

        var toolsHeadingSection = page.Body.SelectSingleNode("//main/div[7]");
        DocumentAssert.Heading2(toolsHeadingSection, "Finance tools");
        //TODO: Get and assert finance tools section on page
    }
}