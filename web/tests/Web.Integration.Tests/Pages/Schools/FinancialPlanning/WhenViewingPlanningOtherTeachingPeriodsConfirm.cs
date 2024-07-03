using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningOtherTeachingPeriodsConfirm(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly int CurrentYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        AssertPageLayout(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, true)]
    [InlineData(EstablishmentTypes.Academies, false)]
    [InlineData(EstablishmentTypes.Maintained, true)]
    [InlineData(EstablishmentTypes.Maintained, false)]
    public async Task CanSubmit(string financeType, bool value)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "Proceed",  value.ToString()}
            });
        });

        var expectedPath = value
            ? Paths.SchoolFinancialPlanningManagementRoles(school.URN, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.URN, CurrentYear).ToAbsolute();

        DocumentAssert.AssertPageUrl(page, expectedPath);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task ShowsErrorOnInValidSubmit(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "Proceed",  "" }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningOtherTeachingPeriodsConfirm(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, ("Proceed", "Select yes if you want to continue without adding other teaching periods"));
    }

    [Fact]
    // [InlineData(EstablishmentTypes.Academies)]
    // [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.URN, CurrentYear).ToAbsolute());*/
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningOtherTeachingPeriodsConfirm(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningOtherTeachingPeriodsConfirm(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupFinancialPlan();

        page = await Client.SubmitForm(page.Forms[0], action);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningOtherTeachingPeriodsConfirm(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningOtherTeachingPeriodsConfirm(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningOtherTeachingPeriodsConfirm(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .Create();

        var plan = Fixture.Build<FinancialPlanInput>()
            .With(x => x.Urn, school.URN)
            .With(x => x.Year, CurrentYear)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningOtherTeachingPeriodsConfirm(school.URN, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Proceed without adding other teaching periods? - Financial Benchmarking and Insights Tool - GOV.UK",
            "Proceed without adding other teaching periods?");
    }
}