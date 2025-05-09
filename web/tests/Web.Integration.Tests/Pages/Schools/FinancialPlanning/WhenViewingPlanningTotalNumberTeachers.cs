using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningTotalNumberTeachers(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly int CurrentYear = DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Nursery)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Nursery)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    public async Task CanDisplay(string financeType, string overallPhase)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, overallPhase);

        AssertPageLayout(page, school, overallPhase);
    }

    /*
    [Fact]
    // [InlineData(true)]
    // [InlineData(false)]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, isPrimary);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        var expectedPage = isPrimary
            ? Paths.SchoolFinancialPlanningTotalEducationSupport(school.URN, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningTotalTeacherCost(school.URN, CurrentYear).ToAbsolute();

        DocumentAssert.AssertPageUrl(page, expectedPage);#1#
    }*/

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanSubmit(string financeType)
    {
        const string overallPhase = OverallPhaseTypes.Secondary;
        var (page, school) = await SetupNavigateInitPage(financeType, overallPhase);
        AssertPageLayout(page, school, overallPhase);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "TotalNumberOfTeachersFte", "19.5"
                }
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTimetableCycle(school.URN, CurrentYear).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningTotalNumberTeachers(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningTotalNumberTeachers(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary);
        var action = page.QuerySelector("main .govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalNumberTeachers(school.URN, CurrentYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningTotalNumberTeachers(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningTotalNumberTeachers(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary);
        var action = page.QuerySelector("main .govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalNumberTeachers(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(-1.0)]
    [InlineData(0.0)]
    [InlineData(0.9)]
    public async Task ShowsErrorOnInValidSubmit(double? value)
    {
        const string overallPhase = OverallPhaseTypes.Secondary;
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, overallPhase);
        AssertPageLayout(page, school, overallPhase);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);


        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "TotalNumberOfTeachersFte", value?.ToString() ?? ""
                }
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTotalNumberTeachers(school.URN, CurrentYear).ToAbsolute());

        var expectedMsg = value is null
            ? "Enter the number of full-time equivalent teachers you have"
            : "Number of full-time equivalent teachers must be 1 or more";
        DocumentAssert.FormErrors(page, ("TotalNumberOfTeachersFte", expectedMsg));
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, string overallPhase)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .With(x => x.OverallPhase, overallPhase)
            .Create();

        var plan = Fixture.Build<FinancialPlanInput>()
            .With(x => x.Urn, school.URN)
            .With(x => x.Year, CurrentYear)
            .With(x => x.UseFigures, false)
            .Without(x => x.TotalNumberOfTeachersFte)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningTotalNumberTeachers(school.URN, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, string overallPhase)
    {
        var expectedBackLink = overallPhase is OverallPhaseTypes.Primary or OverallPhaseTypes.Nursery
            ? Paths.SchoolFinancialPlanningTotalEducationSupport(school.URN, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningTotalTeacherCost(school.URN, CurrentYear).ToAbsolute();

        DocumentAssert.BackLink(page, "Back", expectedBackLink);
        DocumentAssert.TitleAndH1(page, "How many full-time equivalent teachers do you have? - Financial Benchmarking and Insights Tool - GOV.UK", "How many full-time equivalent teachers do you have?");
    }
}