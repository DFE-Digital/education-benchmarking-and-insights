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
    private readonly SchoolBenchmarkingWebAppClient _client = client;

    private static readonly int CurrentYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    public async Task CanDisplay(string financeType, string overallPhase)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, overallPhase);

        AssertPageLayout(page, school, overallPhase);
    }

    /*[Fact]
    // [InlineData(OverallPhaseTypes.Secondary)]
    // [InlineData(OverallPhaseTypes.Primary)]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, phase);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        var expectPage = school.IsPrimary
            ? Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.URN, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningPupilFigures(school.URN, CurrentYear).ToAbsolute();

        DocumentAssert.AssertPageUrl(page, expectPage);#1#
    }*/


    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
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
        var action = page.QuerySelector("main .govuk-button");

        _client.SetupFinancialPlan();

        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTeacherPeriodAllocation(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningTeacherPeriodAllocation(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningTeacherPeriodAllocation(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, string overallPhase, IPostprocessComposer<FinancialPlanInput>? planComposer = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .With(x => x.OverallPhase, overallPhase)
            .Create();

        planComposer ??= Fixture.Build<FinancialPlanInput>();

        var plan = planComposer
            .With(x => x.Urn, school.URN)
            .With(x => x.Year, CurrentYear)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningTeacherPeriodAllocation(school.URN, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, string overallPhase)
    {
        var expectedBackLink = overallPhase is OverallPhaseTypes.Primary or OverallPhaseTypes.Nursery
            ? Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.URN, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningPupilFigures(school.URN, CurrentYear).ToAbsolute();

        DocumentAssert.BackLink(page, "Back", expectedBackLink);
        DocumentAssert.TitleAndH1(page,
            "What are your teacher period allocation figures? - Financial Benchmarking and Insights Tool - GOV.UK",
            "What are your teacher period allocation figures?");
    }
}