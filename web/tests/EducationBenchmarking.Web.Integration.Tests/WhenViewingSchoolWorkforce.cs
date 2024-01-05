using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolWorkforce : BenchmarkingWebAppClient
{
    public WhenViewingSchoolWorkforce(BenchmarkingWebAppFactory factory) : base(factory)
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
            .Navigate($"/school/{school.Urn}/workforce");
            
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
            .Navigate($"/school/{school.Urn}/workforce");

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
    public async Task CanNavigateToCompareCosts()
    {
        //TODO : Add test case to click on and follow compare your costs link
        Assert.True(true);
    }
    
    [Fact]
    public async Task CanDisplayNotFound()
    {
        //TODO : Add test case for when API returns status code exception
        Assert.True(true);
    }
    
    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        //TODO : Add test case for when controller throws exception that isn't a status code exception
        Assert.True(true);
    }
    
    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.TitleAndH1(page, "Education benchmarking and insights",$"Benchmark the workforce data for {school.Name}");
            
        var changeLinkElement = page.GetElementById("change-school");
        Assert.NotNull(changeLinkElement);
        DocumentAssert.Link(changeLinkElement, "Change school", $"https://localhost/school/{school.Urn}/workforce");
            
        var viewYourComparatorLinkElement = page.GetElementById("view-comparator-set");
        Assert.NotNull(viewYourComparatorLinkElement);
        DocumentAssert.PrimaryCTA(viewYourComparatorLinkElement, "View your comparator set", $"/school/{school.Urn}/workforce");
        
        var workforceComponent = page.GetElementById("compare-your-workforce");
        Assert.NotNull(workforceComponent);

        //TODO: Get and assert more tools section on page
    }
}