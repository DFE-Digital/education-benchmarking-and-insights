using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningTotalExpenditure(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanSubmit(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TotalExpenditure",  168794.ToString()}
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalTeacherCost(school.URN, CurrentYear).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalIncome(school.URN, CurrentYear).ToAbsolute());*/
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupFinancialPlan()
            .Navigate(Paths.SchoolFinancialPlanningTotalExpenditure(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningTotalExpenditure(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalExpenditure(school.URN, CurrentYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningTotalExpenditure(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningTotalExpenditure(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalExpenditure(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(-1.0)]
    public async Task ShowsErrorOnInValidSubmit(double? value)
    {

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);


        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TotalExpenditure",  value?.ToString() ?? "" }
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTotalExpenditure(school.URN, CurrentYear).ToAbsolute());

        var expectedMsg = value is null ? "Enter your total expenditure" : "Total expenditure must be 0 or more";
        DocumentAssert.FormErrors(page, ("TotalExpenditure", expectedMsg));
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
            .With(x => x.UseFigures, false)
            .Without(x => x.TotalExpenditure)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningTotalExpenditure(school.URN, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back",
            Paths.SchoolFinancialPlanningTotalIncome(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "What is your total expenditure? - Financial Benchmarking and Insights Tool - GOV.UK", "What is your total expenditure?");
    }
}