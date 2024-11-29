using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using AutoFixture.Dsl;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningPupilFigures(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly int CurrentYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;


    public static TheoryData<bool, int?, int?, int?, int?, int?, decimal?, decimal?> PlanInput =>
        new()
        {
            {
                true, 123, 123, 123, 123, 123, 12.6M, 12.59M
            },
            {
                false, 123, 123, 123, 123, 123, null, null
            }
        };

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        AssertPageLayout(page, school);
    }

    /*[Fact]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTimetableCycle(school.URN, CurrentYear).ToAbsolute());#1#
    }*/

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupFinancialPlan()
            .Navigate(Paths.SchoolFinancialPlanningPupilFigures(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningPupilFigures(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector("main .govuk-button");

        Assert.NotNull(action);

        Client.SetupFinancialPlan();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningPupilFigures(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningPupilFigures(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningPupilFigures(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    [Theory]
    [InlineData("PupilsYear7", "10")]
    [InlineData("PupilsYear8", "7")]
    [InlineData("PupilsYear9", "22")]
    [InlineData("PupilsYear10", "36")]
    [InlineData("PupilsYear11", "2")]
    [InlineData("PupilsYear12", "2.6")]
    [InlineData("PupilsYear13", "3.8")]
    public async Task CanSubmit(string prop, string value)
    {
        var composer = Fixture.Build<FinancialPlanInput>()
            .Without(x => x.PupilsYear7)
            .Without(x => x.PupilsYear8)
            .Without(x => x.PupilsYear9)
            .Without(x => x.PupilsYear10)
            .Without(x => x.PupilsYear11)
            .Without(x => x.PupilsYear12)
            .Without(x => x.PupilsYear13);

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, true, composer);
        AssertPageLayout(page, school);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    prop, value
                }
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTeacherPeriodAllocation(school.URN, CurrentYear).ToAbsolute());
    }


    [Theory]
    [MemberData(nameof(PlanInput))]
    public async Task CanDisplayWithPreviousValue(bool hasSixth, int? year7, int? year8, int? year9, int? year10, int? year11, decimal? year12, decimal? year13)
    {
        var composer = Fixture.Build<FinancialPlanInput>()
            .With(x => x.PupilsYear7, year7.ToString)
            .With(x => x.PupilsYear8, year8.ToString)
            .With(x => x.PupilsYear9, year9.ToString)
            .With(x => x.PupilsYear10, year10.ToString)
            .With(x => x.PupilsYear11, year11.ToString)
            .With(x => x.PupilsYear12, year12)
            .With(x => x.PupilsYear13, year13);

        var (page, _) =
            await SetupNavigateInitPage(EstablishmentTypes.Academies, hasSixth, composer);

        DocumentAssert.Input(page, "PupilsYear7", year7.ToString() ?? "");
        DocumentAssert.Input(page, "PupilsYear8", year8.ToString() ?? "");
        DocumentAssert.Input(page, "PupilsYear9", year9.ToString() ?? "");
        DocumentAssert.Input(page, "PupilsYear10", year10.ToString() ?? "");
        DocumentAssert.Input(page, "PupilsYear11", year11.ToString() ?? "");

        if (hasSixth)
        {
            DocumentAssert.Input(page, "PupilsYear12", year12.ToString() ?? "");
            DocumentAssert.Input(page, "PupilsYear13", year13.ToString() ?? "");
        }
    }

    [Fact]
    public async Task ShowsErrorOnNoFiguresSubmit()
    {

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "PupilsYear7", ""
                },
                {
                    "PupilsYear8", ""
                },
                {
                    "PupilsYear9", ""
                },
                {
                    "PupilsYear10", ""
                },
                {
                    "PupilsYear11", ""
                },
                {
                    "PupilsYear12", ""
                },
                {
                    "PupilsYear13", ""
                }
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningPupilFigures(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, ("pupil-figures", "Enter your pupil figures"));
    }

    [Theory]
    [InlineData("PupilsYear7", "0.5", "Pupil figures for year 7 must be a whole number")]
    [InlineData("PupilsYear7", "-1", "Pupil figures for year 7 must be 0 or more")]
    [InlineData("PupilsYear8", "0.5", "Pupil figures for year 8 must be a whole number")]
    [InlineData("PupilsYear8", "-1", "Pupil figures for year 8 must be 0 or more")]
    [InlineData("PupilsYear9", "0.5", "Pupil figures for year 9 must be a whole number")]
    [InlineData("PupilsYear9", "-1", "Pupil figures for year 9 must be 0 or more")]
    [InlineData("PupilsYear10", "0.5", "Pupil figures for year 10 must be a whole number")]
    [InlineData("PupilsYear10", "-1", "Pupil figures for year 10 must be 0 or more")]
    [InlineData("PupilsYear11", "0.5", "Pupil figures for year 11 must be a whole number")]
    [InlineData("PupilsYear11", "-1", "Pupil figures for year 11 must be 0 or more")]
    [InlineData("PupilsYear12", "-1", "Pupil figures for year 12 must be 0 or more")]
    [InlineData("PupilsYear13", "-1", "Pupil figures for year 13 must be 0 or more")]
    public async Task ShowsErrorOnInValidEntrySubmit(string prop, string value, string error)
    {

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, true);
        AssertPageLayout(page, school);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    prop, value
                }
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningPupilFigures(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, (prop, error));
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool isSixth = false, IPostprocessComposer<FinancialPlanInput>? planComposer = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .With(x => x.OverallPhase, OverallPhaseTypes.Secondary)
            .With(x => x.HasSixthForm, isSixth)
            .Create();
        planComposer ??= Fixture.Build<FinancialPlanInput>();

        var plan = planComposer
            .With(x => x.Urn, school.URN)
            .With(x => x.Year, CurrentYear)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningPupilFigures(school.URN, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back",
            Paths.SchoolFinancialPlanningTimetableCycle(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "What are your pupil figures? - Financial Benchmarking and Insights Tool - GOV.UK",
            "What are your pupil figures?");
    }
}