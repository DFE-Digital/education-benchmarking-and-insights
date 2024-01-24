using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolPlanning : BenchmarkingWebAppClient
{
    public WhenViewingSchoolPlanning(BenchmarkingWebAppFactory factory) : base(factory)
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
    public async Task CanNavigateToHelp(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        

        var anchor = page.GetElementById("financial-planning-help");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolPlanningHelp(school.Urn).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToContinue(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);


        var anchor = page.GetElementById("financial-planning-continue");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        //TODO: amend path once functionality added
        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolPlanning(school.Urn).ToAbsolute());
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

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var page = await SetupEstablishment(school)
            .Navigate(Paths.SchoolPlanning(school.Urn));

        return (page, school);
    }
    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute()),
            ("Curriculum and financial planning", Paths.SchoolPlanning(school.Urn).ToAbsolute()),
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        DocumentAssert.TitleAndH1(page, "Integrated Curriculum and financial planning", "Integrated Curriculum and financial planning");
        DocumentAssert.Heading2(page, $"{school.Name}");

        var cta = page.GetElementById("financial-planning-continue");
        Assert.NotNull(cta);
        // TODO: update path when functionality added
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolPlanning(school.Urn));

        var helpLink = page.GetElementById("financial-planning-help");
        Assert.NotNull(helpLink);
        DocumentAssert.Link(helpLink, "can be found here", Paths.SchoolPlanningHelp(school.Urn).ToAbsolute());
    }
}
