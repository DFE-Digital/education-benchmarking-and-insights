using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningHelp(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningHelp(school.URN).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningStart(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Data required for curriculum and financial planning (CFP) - Financial Benchmarking and Insights Tool - GOV.UK", "Data required for curriculum and financial planning (CFP)");
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningStart(school.URN).ToAbsolute());*/
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .Navigate(Paths.SchoolFinancialPlanningHelp(school.URN));

        return (page, school);
    }
}