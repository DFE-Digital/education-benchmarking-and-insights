using AngleSharp.Dom;
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
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();

        var page = await SetupEstablishment(school)
            .SetupInsightsFromMaintainedSchool(school, finances)
            .Navigate(Paths.SchoolComparison(school.Urn));

        var changeSchoolAnchor = page.QuerySelector("#change-school");
        Assert.NotNull(changeSchoolAnchor);

        var newPage = await Follow(changeSchoolAnchor);

        //TODO: amend path once functionality added
        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.Urn).ToAbsolute());
    }
    
    [Fact]
    public async Task CanNavigateToComparatorSet()
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();

        var page = await SetupEstablishment(school)
            .SetupInsightsFromMaintainedSchool(school, finances)
            .Navigate(Paths.SchoolComparison(school.Urn));

        var comparatorAnchor = page.QuerySelector("#view-comparator-set");
        Assert.NotNull(comparatorAnchor);

        var newPage = await Follow(comparatorAnchor);

        //TODO: amend path once functionality added
        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.Urn).ToAbsolute());
    }
    
    [Fact]
    public async Task CanNavigateToAreasForInvestigation()
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();

        var page = await SetupEstablishment(school)
            .SetupInsightsFromMaintainedSchool(school, finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolComparison(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var link = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(link);

        var newPage = await Follow(link);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolInvestigation(school.Urn).ToAbsolute());
    }
    
    [Fact]
    public async Task CanNavigateToCurriculumPlanning()
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();

        var page = await SetupEstablishment(school)
            .SetupInsightsFromMaintainedSchool(school, finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolComparison(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var link = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(link);

        var newPage = await Follow(link);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());
    }
    
    [Fact]
    public async Task CanNavigateToWorkforceBenchmark()
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();

        var page = await SetupEstablishment(school)
            .SetupInsightsFromMaintainedSchool(school, finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolComparison(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var link = liElements[2].QuerySelector("h3 > a");
        Assert.NotNull(link);

        var newPage = await Follow(link);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolWorkforce(school.Urn).ToAbsolute());
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

        var toolsListSection = toolsHeadingSection.ChildNodes.QuerySelector("ul");
        Assert.NotNull(toolsListSection);

        var toolsList = toolsListSection.QuerySelectorAll("li > h3 > a").ToList();
        Assert.Equal(3, toolsList.Count);

        var investigationAnchor = toolsList[0];
        DocumentAssert.Link(investigationAnchor, "View your areas for investigation", Paths.SchoolInvestigation(school.Urn).ToAbsolute());

        var curriculumPlanningAnchor = toolsList[1];
        DocumentAssert.Link(curriculumPlanningAnchor, "Curriculum and financial planning", Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());

        var workforceAnchor = toolsList[2];
        DocumentAssert.Link(workforceAnchor, "Benchmark workforce data", Paths.SchoolWorkforce(school.Urn).ToAbsolute());
    }
}