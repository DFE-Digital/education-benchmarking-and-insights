using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using AutoFixture.Dsl;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningTeacherPeriodAllocation(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly int CurrentYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    public async Task CanDisplay(string financeType, string phase)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, phase);

        AssertPageLayout(page, school);
    }

    [Theory]
    [InlineData(OverallPhaseTypes.Secondary)]
    [InlineData(OverallPhaseTypes.Primary)]
    public async Task CanNavigateBack(string phase)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, phase);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        var expectPage = school.IsPrimary
            ? Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.Urn, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningPupilFigures(school.Urn, CurrentYear).ToAbsolute();

        DocumentAssert.AssertPageUrl(page, expectPage);
    }


    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningTeacherPeriodAllocation(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningTeacherPeriodAllocation(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var composer = Fixture.Build<FinancialPlanInput>()
            .With(x => x.PupilsYear7, "145");

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary, composer);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupBenchmarkWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTeacherPeriodAllocation(school.Urn, CurrentYear).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningTeacherPeriodAllocation(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningTeacherPeriodAllocation(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceOnSubmit()
    {
        var composer = Fixture.Build<FinancialPlanInput>()
            .With(x => x.PupilsYear7, "145");

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary, composer);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupBenchmarkWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTeacherPeriodAllocation(school.Urn, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, string overallPhase, IPostprocessComposer<FinancialPlanInput>? planComposer = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.Urn, "12345")
            .With(x => x.FinanceType, financeType)
            .With(x => x.OverallPhase, overallPhase)
            .Create();

        var finances = Fixture.Build<Finances>()
            .Create();

        planComposer ??= Fixture.Build<FinancialPlanInput>();

        var plan = planComposer
            .With(x => x.Urn, school.Urn)
            .With(x => x.Year, CurrentYear)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools, plan)
            .Navigate(Paths.SchoolFinancialPlanningTeacherPeriodAllocation(school.Urn, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectBack = school.IsPrimary
            ? Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.Urn, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningPupilFigures(school.Urn, CurrentYear).ToAbsolute();

        DocumentAssert.BackLink(page, "Back", expectBack);
        DocumentAssert.TitleAndH1(page,
            "What are your teacher period allocation figures? - Financial Benchmarking and Insights Tool - GOV.UK",
            "What are your teacher period allocation figures?");
    }
}