using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolComparison : BenchmarkingWebAppClient
{
    public WhenViewingSchoolComparison(BenchmarkingWebAppFactory factory) : base(factory)
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
            .Navigate(Paths.SchoolComparison(school.Urn));
            
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
            .Navigate(Paths.SchoolComparison(school.Urn));
            
        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanNavigateToChangeSchool()
    {
        //TODO : Add test case to click on and follow change school link
        Assert.True(true);
    }
    
    [Fact]
    public async Task CanNavigateToComparatorSet()
    {
        //TODO : Add test case to click on and follow view your comparator set button
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
            .Navigate(Paths.SchoolComparison("12345"));
        
        DocumentAssert.AssertPageUrl(page, Paths.StatusError(404).ToAbsolute());
    }
    
    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolComparison("12345"));
        
        DocumentAssert.AssertPageUrl(page, Paths.StatusError(500).ToAbsolute());
    }
    
    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute()),
            ("Compare your costs", Paths.SchoolComparison(school.Urn).ToAbsolute()),
        };
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(school.Urn).ToAbsolute());
        DocumentAssert.Breadcrumbs(page,expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Compare your costs",$"Compare your costs for {school.Name}");
            
        var changeLinkElement = page.GetElementById("change-school");
        Assert.NotNull(changeLinkElement);
        DocumentAssert.Link(changeLinkElement, "Change school", Paths.SchoolComparison(school.Urn).ToAbsolute());
            
        var viewYourComparatorLinkElement = page.GetElementById("view-comparator-set");
        Assert.NotNull(viewYourComparatorLinkElement);
        DocumentAssert.PrimaryCta(viewYourComparatorLinkElement, "View your comparator set", Paths.SchoolComparison(school.Urn));
        
        var comparisonComponent = page.GetElementById("compare-your-school");
        Assert.NotNull(comparisonComponent);
        
        var toolsHeadingSection = page.Body.SelectSingleNode("//main/div[4]");
        DocumentAssert.Heading2(toolsHeadingSection, "More tools");
        //TODO: Get and assert more tools section on page
    }
}