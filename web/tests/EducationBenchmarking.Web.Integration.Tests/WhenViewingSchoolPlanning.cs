using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolPlanning(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory,
        output)
{
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


        var anchor = page.QuerySelector(".govuk-grid-row .govuk-link");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCurriculumPlanningHelp(school.Urn).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToContinue(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);


        var anchor = page.QuerySelector(".govuk-button");
        Assert.NotNull(anchor);

        var newPage = await Follow(anchor);

        //TODO: amend path once functionality added
        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());
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
            .Navigate(Paths.SchoolCurriculumPlanning(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute()),
            ("Curriculum and financial planning", Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        DocumentAssert.TitleAndH1(page, "Integrated Curriculum and financial planning (ICFP)",
            "Integrated Curriculum and financial planning (ICFP)");
        DocumentAssert.Heading2(page, $"{school.Name}");

        var cta = page.QuerySelector(".govuk-button");
        Assert.NotNull(cta);
        // TODO: update path when functionality added
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolCurriculumPlanning(school.Urn));

        var helpLink = page.QuerySelector(".govuk-grid-row .govuk-link");
        Assert.NotNull(helpLink);
        DocumentAssert.Link(helpLink, "can be found here", Paths.SchoolCurriculumPlanningHelp(school.Urn).ToAbsolute());
    }
}