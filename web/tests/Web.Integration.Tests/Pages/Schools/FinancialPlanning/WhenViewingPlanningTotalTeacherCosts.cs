using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningTotalTeacherCosts(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly int CurrentYear = DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    [Theory]
    [InlineData(EstablishmentTypes.Academies, true)]
    [InlineData(EstablishmentTypes.Academies, false)]
    [InlineData(EstablishmentTypes.Maintained, true)]
    [InlineData(EstablishmentTypes.Maintained, false)]
    public async Task CanDisplay(string financeType, bool isPrimary)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, isPrimary);

        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, false);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTotalExpenditure(school.URN, CurrentYear).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupFinancialPlan()
            .Navigate(Paths.SchoolFinancialPlanningTotalTeacherCost(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningTotalTeacherCost(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, false);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalTeacherCost(school.URN, CurrentYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningTotalTeacherCost(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningTotalTeacherCost(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, false);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalTeacherCost(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(-1.0)]
    public async Task ShowsErrorOnInValidSubmit(double? value)
    {

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, false);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);


        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TotalTeacherCosts",  value?.ToString() ?? "" }
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTotalTeacherCost(school.URN, CurrentYear).ToAbsolute());

        var expectedMsg = value is null ? "Enter your total teacher costs" : "Total teacher costs must be 0 or more";
        DocumentAssert.FormErrors(page, ("TotalTeacherCosts", expectedMsg));
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, true)]
    [InlineData(EstablishmentTypes.Academies, false)]
    [InlineData(EstablishmentTypes.Maintained, true)]
    [InlineData(EstablishmentTypes.Maintained, false)]
    public async Task CanSubmit(string financeType, bool isPrimary)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, isPrimary);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TotalTeacherCosts",  168794.ToString()}
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        var expectedPage = isPrimary
            ? Paths.SchoolFinancialPlanningTotalEducationSupport(school.URN, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningTotalNumberTeachers(school.URN, CurrentYear).ToAbsolute();

        DocumentAssert.AssertPageUrl(page, expectedPage);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool isPrimary)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .With(x => x.OverallPhase, isPrimary ? OverallPhaseTypes.Primary : OverallPhaseTypes.Secondary)
            .Create();

        var plan = Fixture.Build<FinancialPlanInput>()
            .With(x => x.Urn, school.URN)
            .With(x => x.Year, CurrentYear)
            .With(x => x.UseFigures, false)
            .Without(x => x.TotalTeacherCosts)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningTotalTeacherCost(school.URN, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningTotalExpenditure(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "What is your total spend on teaching staff? - Financial Benchmarking and Insights Tool - GOV.UK", "What is your total spend on teaching staff?");
    }
}