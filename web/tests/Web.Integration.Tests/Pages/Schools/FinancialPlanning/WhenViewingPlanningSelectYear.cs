using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningSelectYear(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly int CurrentYear = DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    private static readonly int[] ListedYears = Enumerable.Range(CurrentYear, 4).ToArray();

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task ShowsErrorOnInValidSelect()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);

        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningSelectYear(school.URN).ToAbsolute());
        DocumentAssert.FormErrors(page, ("Year", "Select an academic year to plan"));
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningSelectYear(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningSelectYear(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningSelectYear(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningSelectYear(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector("main .govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningSelectYear(school.URN).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector("main .govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningSelectYear(school.URN).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningStart(school.URN).ToAbsolute());*/
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool seedPlan = true)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .Create();

        var plan = !seedPlan
            ? null
            : Fixture.Build<FinancialPlanInput>()
                .With(x => x.Urn, school.URN)
                .With(x => x.Year, CurrentYear)
                .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningSelectYear(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningStart(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Which academic year do you want to plan? - Financial Benchmarking and Insights Tool - GOV.UK", "Which academic year do you want to plan?");

        var cta = page.QuerySelector("main .govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolFinancialPlanningSelectYear(school.URN));

        var radios = page.QuerySelectorAll("input[type='radio']");
        Assert.Equal(ListedYears.Length, radios.Length);
        Assert.Equal(ListedYears[0].ToString(), radios[0].GetAttribute("value"));
        Assert.Equal(ListedYears[1].ToString(), radios[1].GetAttribute("value"));
        Assert.Equal(ListedYears[2].ToString(), radios[2].GetAttribute("value"));
        Assert.Equal(ListedYears[3].ToString(), radios[3].GetAttribute("value"));
    }
}