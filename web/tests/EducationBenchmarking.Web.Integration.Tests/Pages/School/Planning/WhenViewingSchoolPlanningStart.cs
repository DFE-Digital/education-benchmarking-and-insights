using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.School.Planning;

public class WhenViewingSchoolPlanningStart(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory,
        output)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
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
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningHelp(school.Urn).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToContinue(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        var anchor = page.QuerySelector(".govuk-button");
        page = await Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningSelectYear(school.Urn).ToAbsolute());
    }


    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateBack(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());
    }
    
    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolCurriculumPlanningStart(urn));

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningStart(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolCurriculumPlanningStart(urn));

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningStart(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Domain.School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<Domain.School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var page = await SetupEstablishment(school)
            .Navigate(Paths.SchoolCurriculumPlanningStart(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, Domain.School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCurriculumPlanning(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Curriculum and financial planning (CFP)", "Curriculum and financial planning (CFP)");

        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolCurriculumPlanningSelectYear(school.Urn));

        var helpLink = page.QuerySelector(".govuk-grid-row .govuk-link");
        DocumentAssert.Link(helpLink, "can be found here", Paths.SchoolCurriculumPlanningHelp(school.Urn).ToAbsolute());
    }
}