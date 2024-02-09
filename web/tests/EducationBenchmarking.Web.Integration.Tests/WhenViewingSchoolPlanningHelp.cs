using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolPlanningHelp(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory,
        output)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningHelp(school.Urn).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCurriculumPlanningStart(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Data required for curriculum and financial planning (CFP)", "Data required for curriculum and financial planning (CFP)");
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningStart(school.Urn).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var page = await SetupEstablishment(school)
            .Navigate(Paths.SchoolCurriculumPlanningHelp(school.Urn));

        return (page, school);
    }
}