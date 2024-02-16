using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningHelp(BenchmarkingWebAppClient client) : PageBase(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningHelp(school.Urn).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningStart(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Data required for curriculum and financial planning (CFP) - Education benchmarking and insights - GOV.UK", "Data required for curriculum and financial planning (CFP)");
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningStart(school.Urn).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .Navigate(Paths.SchoolFinancialPlanningHelp(school.Urn));

        return (page, school);
    }
}