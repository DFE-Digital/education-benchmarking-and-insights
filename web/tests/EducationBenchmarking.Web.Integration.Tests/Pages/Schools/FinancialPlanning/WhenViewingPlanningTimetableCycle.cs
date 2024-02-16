using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Moq;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningTimetableCycle(BenchmarkingWebAppClient client) : PageBase(client)
{
    private static readonly int CurrentYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    [Theory]
    [InlineData(EstablishmentTypes.Academies, true)]
    [InlineData(EstablishmentTypes.Academies, false)]
    [InlineData(EstablishmentTypes.Maintained, true)]
    [InlineData(EstablishmentTypes.Maintained, false)]
    public async Task CanDisplay(string financeType, bool useFigures)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, useFigures);

        AssertPageLayout(page, school, useFigures);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, true)]
    [InlineData(EstablishmentTypes.Academies, false)]
    [InlineData(EstablishmentTypes.Maintained, true)]
    [InlineData(EstablishmentTypes.Maintained, false)]
    public async Task CanSubmit(string financeType, bool useFigures)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, useFigures);
        AssertPageLayout(page, school, useFigures);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TimetablePeriods",  "25"}
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        //TODO: update path once next page in icfp is created
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, CurrentYear).ToAbsolute());
    }

    [Theory]
    [InlineData("", "Enter the number of periods in one timetable cycle")]
    [InlineData("-1", "Number of periods in one timetable cycle must be 1 or more")]
    [InlineData("1.1", "Number of periods in one timetable cycle must be a whole number")]
    public async Task ShowsErrorOnInValidSubmit(string timetablePeriods, string expectedMsg)
    {

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);


        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TimetablePeriods",  timetablePeriods }
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, ("timetable-periods", expectedMsg));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanNavigateBack(bool useFigures)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, useFigures);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        var path = useFigures
            ? Paths.SchoolFinancialPlanningPrePopulatedData(school.Urn, CurrentYear)
            : Paths.SchoolFinancialPlanningTotalNumberTeachers(school.Urn, CurrentYear);

        DocumentAssert.AssertPageUrl(page, path.ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningTimetableCycle(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningTimetableCycle(urn, year).ToAbsolute();
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

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, CurrentYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningTimetableCycle(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningTimetableCycle(urn, year).ToAbsolute();
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

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceWhenUseFiguresIsNull()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, useFigures: null);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }
       
    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool? useFigures = true)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var finances = Fixture.Build<Finances>()
            .Create();

        var plan = Fixture.Build<FinancialPlan>()
            .With(x => x.Urn, school.Urn)
            .With(x => x.Year, CurrentYear)
            .With(x => x.UseFigures, useFigures)
            .Without(x => x.TimetablePeriods)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools, plan)
            .Navigate(Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, bool useFigures = true)
    {
        var path = useFigures
            ? Paths.SchoolFinancialPlanningPrePopulatedData(school.Urn, CurrentYear)
            : Paths.SchoolFinancialPlanningTotalNumberTeachers(school.Urn, CurrentYear);

        DocumentAssert.BackLink(page, "Back", path.ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Timetable cycle - Education benchmarking and insights - GOV.UK", "Timetable cycle");
    }
}