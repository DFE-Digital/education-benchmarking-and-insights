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

    [Fact]
    public async Task CanSubmitAndContinueToPrePopulatedData()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);

        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        const int year = 2027;
        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "Year", year.ToString()
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningPrePopulatedData(school.URN, year).ToAbsolute());
    }

    [Theory]
    [InlineData(null, null, null, null, null)]
    [InlineData(1_234_567, 987_654, null, null, null)]
    [InlineData(1_234_567, null, null, null, null)]
    [InlineData(null, null, null, null, 123)]
    [InlineData(1_234_567, 987_654, 98_765, null, 123)]
    [InlineData(1_234_567, 987_654, null, 9_876, 123)]
    public async Task CanSubmitAndContinueToTotalIncome(
        int? totalIncome,
        int? totalExpenditure,
        int? teachingStaffCosts,
        int? educationSupportStaffCosts,
        int? teachers)
    {
        var (page, school) = await SetupNavigateInitPage(
            EstablishmentTypes.Academies,
            true,
            totalIncome,
            totalExpenditure,
            teachingStaffCosts,
            educationSupportStaffCosts,
            teachers);
        AssertPageLayout(page, school);

        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        const int year = 2027;
        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "Year", year.ToString()
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTotalIncome(school.URN, year).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(
        string financeType,
        bool seedPlan = true,
        decimal? totalIncome = 1_234_567,
        decimal? totalExpenditure = 987_654,
        decimal? teachingStaffCosts = 98_765,
        decimal? educationSupportStaffCosts = 9_876,
        decimal? teachers = 123)
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

        var income = Fixture.Build<SchoolIncome>()
            .With(i => i.TotalIncome, totalIncome)
            .Create();

        var expenditure = Fixture.Build<SchoolExpenditure>()
            .With(i => i.TotalExpenditure, totalExpenditure)
            .With(i => i.TeachingStaffCosts, teachingStaffCosts)
            .With(i => i.EducationSupportStaffCosts, educationSupportStaffCosts)
            .Create();

        var census = Fixture.Build<Census>()
            .With(i => i.Teachers, teachers)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .SetupIncome(school, income)
            .SetupExpenditure(school, expenditure)
            .SetupCensus(school, census)
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