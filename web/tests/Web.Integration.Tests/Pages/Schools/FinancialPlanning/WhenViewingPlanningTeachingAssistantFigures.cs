using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using AutoFixture.Dsl;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningTeachingAssistantFigures(BenchmarkingWebAppClient client) : PageBase(client)
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
    [InlineData("1.2")]
    [InlineData(null)]
    public async Task CanDisplayWithPreviousValue(string? assistants)
    {
        var composer = Fixture.Build<FinancialPlan>()
            .With(x => x.PupilsYear6, "1")
            .With(x => x.AssistantsYear6, decimal.TryParse(assistants, out var parsed) ? parsed : null);

        var (page, _) =
            await SetupNavigateInitPage(EstablishmentTypes.Academies, composer);

        var input = page.GetElementById("AssistantsYear6");
        Assert.NotNull(input);

        Assert.Equal(input.GetAttribute("value"), assistants);
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
                { "AssistantsNursery",  "8"}
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.Urn, CurrentYear).ToAbsolute());
    }

    [Theory]
    [InlineData("", "Enter your teaching assistant figures for nursery")]
    [InlineData("-1", "Teaching assistant figures for nursery must be 0 or more")]
    public async Task ShowsErrorOnInValidSubmit(string assistants, string expectedMsg)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "AssistantsNursery",  assistants }
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTeachingAssistantFigures(school.Urn, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, ("AssistantsNursery", expectedMsg));
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateBack(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTeacherPeriodAllocation(school.Urn, CurrentYear).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupBenchmarkWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningTeachingAssistantFigures(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningTeachingAssistantFigures(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupBenchmarkWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTeachingAssistantFigures(school.Urn, CurrentYear).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client
            .SetupEstablishmentWithException()
            .SetupBenchmarkWithException()
            .Navigate(Paths.SchoolFinancialPlanningTeachingAssistantFigures(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningTeachingAssistantFigures(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupBenchmarkWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTeachingAssistantFigures(school.Urn, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, IPostprocessComposer<FinancialPlan>? planComposer = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .With(x => x.OverallPhase, OverallPhaseTypes.Primary)
            .Create();

        var finances = Fixture.Build<Finances>()
            .Create();

        planComposer ??= Fixture.Build<FinancialPlan>();

        var plan = planComposer
            .With(x => x.Urn, school.Urn)
            .With(x => x.Year, CurrentYear)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools, plan)
            .Navigate(Paths.SchoolFinancialPlanningTeachingAssistantFigures(school.Urn, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningTeacherPeriodAllocation(school.Urn, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "What are your teaching assistant figures? - Financial Benchmarking and Insights Tool - GOV.UK",
            "What are your teaching assistant figures?");
    }
}