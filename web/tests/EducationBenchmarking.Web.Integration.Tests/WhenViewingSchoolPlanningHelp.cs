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
    public async Task PageLayoutIsCorrect()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningHelp(school.Urn).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCurriculumPlanningStart(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Data required for ICFP", "Data required for ICFP");

        var helpLink = page.QuerySelector(".govuk-grid-row .govuk-link");
        Assert.NotNull(helpLink);
        DocumentAssert.Link(helpLink, "submit an enquiry", "/submit-an-enquiry".ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningStart(school.Urn).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToSubmitEnquiry()
    {
        var (page, _) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-grid-row .govuk-link");
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, "/submit-an-enquiry".ToAbsolute());
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