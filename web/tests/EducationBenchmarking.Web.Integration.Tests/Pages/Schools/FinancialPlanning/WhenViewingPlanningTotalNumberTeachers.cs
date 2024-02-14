using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Moq;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningTotalNumberTeachers(BenchmarkingWebAppClient client) : PageBase(client)
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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanNavigateBack(bool isPrimary)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, isPrimary);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        var expectedPage = isPrimary
            ? Paths.SchoolFinancialPlanningTotalEducationSupport(school.Urn, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningTotalTeacherCost(school.Urn, CurrentYear).ToAbsolute();

        DocumentAssert.AssertPageUrl(page, expectedPage);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanSubmit(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, false);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TotalNumberOfTeachersFte",  "19.5"}
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Once);
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, CurrentYear).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
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
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, false);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalNumberTeachers(school.Urn, CurrentYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
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
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, false);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalNumberTeachers(school.Urn, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(-1.0)]
    [InlineData(0.0)]
    [InlineData(0.9)]
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
                { "TotalNumberOfTeachersFte",  value?.ToString() ?? "" }
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTotalNumberTeachers(school.Urn, CurrentYear).ToAbsolute());

        var expectedMsg = value is null
            ? "Enter your number of full-time equivalent teachers"
            : "Number of full-time equivalent teachers must be 1 or more";
        DocumentAssert.FormErrors(page, ("total-number-teacher", expectedMsg));
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool isPrimary)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .With(x => x.OverallPhase, isPrimary ? OverallPhaseTypes.Primary : OverallPhaseTypes.Secondary)
            .Create();

        var finances = Fixture.Build<Finances>()
            .Create();

        var plan = Fixture.Build<FinancialPlan>()
            .With(x => x.Urn, school.Urn)
            .With(x => x.Year, CurrentYear)
            .With(x => x.UseFigures, false)
            .Without(x => x.TotalNumberOfTeachersFte)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools, plan)
            .Navigate(Paths.SchoolFinancialPlanningTotalNumberTeachers(school.Urn, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBackLink = school.IsPrimary
            ? Paths.SchoolFinancialPlanningTotalEducationSupport(school.Urn, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningTotalTeacherCost(school.Urn, CurrentYear).ToAbsolute();

        DocumentAssert.BackLink(page, "Back", expectedBackLink);
        DocumentAssert.TitleAndH1(page, "How many full-time equivalent teachers do you have? - Education benchmarking and insights - GOV.UK", "How many full-time equivalent teachers do you have?");
    }
}