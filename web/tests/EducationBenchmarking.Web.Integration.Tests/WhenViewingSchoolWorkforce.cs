using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
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
            .SetupInsights(school,finances)
            .Navigate(Paths.SchoolWorkforce(school.Urn));
            
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
            .SetupInsights(school,finances)
            .Navigate(Paths.SchoolWorkforce(school.Urn));

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
            .SetupInsights(school, finances)
            .Navigate(Paths.SchoolWorkforce(school.Urn));

        var anchor = page.QuerySelector("#change-school");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        //TODO: amend path once functionality added
        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolWorkforce(school.Urn).ToAbsolute());
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
            .SetupInsights(school, finances)
            .Navigate(Paths.SchoolWorkforce(school.Urn));

        var anchor = page.QuerySelector("#view-comparator-set");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        //TODO: amend path once functionality added
        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolWorkforce(school.Urn).ToAbsolute());
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
            .SetupInsights(school, finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolWorkforce(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

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
            .SetupInsights(school, finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolWorkforce(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());
    }
    
    [Fact]
    public async Task CanNavigateToCompareCosts()
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();

        var page = await SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark()
            .Navigate(Paths.SchoolWorkforce(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[2].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.Urn).ToAbsolute());
    }
    
    [Fact]
    public async Task CanDisplayNotFound()
    {
        var page = await SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolWorkforce("123456"));
        
        DocumentAssert.AssertPageUrl(page, Paths.StatusError(404).ToAbsolute());
    }
    
    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolWorkforce("123456"));
        
        DocumentAssert.AssertPageUrl(page, Paths.StatusError(500).ToAbsolute());
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
        DocumentAssert.TitleAndH1(page, "Benchmark workforce data",$"Benchmark the workforce data for {school.Name}");
            
        var changeLinkElement = page.GetElementById("change-school");
        Assert.NotNull(changeLinkElement);
        DocumentAssert.Link(changeLinkElement, "Change school", Paths.SchoolWorkforce(school.Urn).ToAbsolute());
            
        var viewYourComparatorLinkElement = page.GetElementById("view-comparator-set");
        Assert.NotNull(viewYourComparatorLinkElement);
        DocumentAssert.PrimaryCta(viewYourComparatorLinkElement, "View your comparator set", Paths.SchoolWorkforce(school.Urn));
        
        var workforceComponent = page.GetElementById("compare-workforce");
        Assert.NotNull(workforceComponent);

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

        var comparisonAnchor = toolsList[2];
        DocumentAssert.Link(comparisonAnchor, "Compare your costs", Paths.SchoolComparison(school.Urn).ToAbsolute());
    }
}