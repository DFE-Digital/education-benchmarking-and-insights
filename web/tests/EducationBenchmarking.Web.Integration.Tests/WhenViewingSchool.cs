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
            .Navigate(Paths.SchoolHome(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var link = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(link);

        var newPage = await Follow(link);

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
            .Navigate(Paths.SchoolHome(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var link = liElements[1].QuerySelector("h3 > a");
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
            .Navigate(Paths.SchoolHome(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var link = liElements[2].QuerySelector("h3 > a");
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
            .Navigate(Paths.SchoolHome(school.Urn));

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var link = liElements[3].QuerySelector("h3 > a");
        Assert.NotNull(link);

        var newPage = await Follow(link);

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

        var toolsHeadingSection = page.Body.SelectSingleNode("//main/div[7]");
        DocumentAssert.Heading2(toolsHeadingSection, "Finance tools");

        var toolsListSection = toolsHeadingSection.ChildNodes.QuerySelector("ul");
        Assert.NotNull(toolsListSection);

        var toolsList = toolsListSection.QuerySelectorAll("li > h3 > a").ToList();
        Assert.Equal(4, toolsList.Count);

        var comparisonAnchor = toolsList[0];
        DocumentAssert.Link(comparisonAnchor, "Compare your costs", Paths.SchoolComparison(school.Urn).ToAbsolute());

        var investigationAnchor = toolsList[1];
        DocumentAssert.Link(investigationAnchor, "View your areas for investigation", Paths.SchoolInvestigation(school.Urn).ToAbsolute());

        var curriculumPlanningAnchor = toolsList[2];
        DocumentAssert.Link(curriculumPlanningAnchor, "Curriculum and financial planning", Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());

        var workforceAnchor = toolsList[3];
        DocumentAssert.Link(workforceAnchor, "Benchmark workforce data", Paths.SchoolWorkforce(school.Urn).ToAbsolute());



    }
}