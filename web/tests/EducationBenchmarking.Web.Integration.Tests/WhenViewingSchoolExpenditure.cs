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
            .With(x => x.URN, school.Urn)
            .Create();
            
        var page = await SetupEstablishment(school)
            .SetupAcademyInsights(school,finances)
            .Navigate($"/school/{school.Urn}/comparison");
            
        DocumentAssert.TitleAndH1(page, "Education benchmarking and insights",$"Compare your costs for {school.Name}");
            
        var changeLinkElement = page.GetElementById("change-school");
        Assert.NotNull(changeLinkElement);
        Assert.Contains("Change school", changeLinkElement.TextContent);
            
        var viewYourComparatorLinkElement = page.GetElementById("view-comparator-set");
        Assert.NotNull(viewYourComparatorLinkElement);
        Assert.Contains("View your comparator set", viewYourComparatorLinkElement.TextContent);
    }
    
    [Fact]
    public async Task CanDisplayMaintainedSchool()
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();
            
        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.URN, school.Urn)
            .Create();
            
        var page = await SetupEstablishment(school)
            .SetupMaintainedSchoolInsights(school,finances)
            .Navigate($"/school/{school.Urn}/comparison");
            
        DocumentAssert.TitleAndH1(page, "Education benchmarking and insights",$"Compare your costs for {school.Name}");
            
        var changeLinkElement = page.GetElementById("change-school");
        Assert.NotNull(changeLinkElement);
        Assert.Contains("Change school", changeLinkElement.TextContent);
            
        var viewYourComparatorLinkElement = page.GetElementById("view-comparator-set");
        Assert.NotNull(viewYourComparatorLinkElement);
        Assert.Contains("View your comparator set", viewYourComparatorLinkElement.TextContent);
    }
}