using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using AutoFixture.Dsl;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningPrimaryPupilFigures(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly int CurrentYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    [Theory]
    [InlineData(EstablishmentTypes.Academies, true, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, true, OverallPhaseTypes.Nursery)]
    [InlineData(EstablishmentTypes.Academies, false, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, false, OverallPhaseTypes.Nursery)]
    [InlineData(EstablishmentTypes.Maintained, true, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, true, OverallPhaseTypes.Nursery)]
    [InlineData(EstablishmentTypes.Maintained, false, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, false, OverallPhaseTypes.Nursery)]
    public async Task CanDisplay(string financeType, bool hasMixedAge, string overallPhase)
    {
        var (page, school, plan) = await SetupNavigateInitPage(financeType, hasMixedAge, overallPhase);

        AssertPageLayout(page, school, plan);
    }

    /*[Fact]
    // [InlineData(true)]
    // [InlineData(false)]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school, _) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, hasMixedClasses);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        var expectedPage = hasMixedClasses
            ? Paths.SchoolFinancialPlanningMixedAgeClasses(school.URN, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.URN, CurrentYear).ToAbsolute();

        DocumentAssert.AssertPageUrl(page, expectedPage);#1#
    }*/

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupFinancialPlan()
            .Navigate(Paths.SchoolFinancialPlanningPrimaryPupilFigures(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningPrimaryPupilFigures(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplayNotFoundOnSubmit(bool hasMixed)
    {
        var (page, school, _) = await SetupNavigateInitPage(EstablishmentTypes.Academies, hasMixed, OverallPhaseTypes.Primary);
        var action = page.QuerySelector("main .govuk-button");

        Assert.NotNull(action);

        Client.SetupFinancialPlan();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningPrimaryPupilFigures(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningPrimaryPupilFigures(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    [Theory]
    [InlineData("PupilsNursery", "-1", "Pupil figures for nursery must be 0 or more")]
    [InlineData("PupilsReception", "0.5", "Pupil figures for reception must be a whole number")]
    [InlineData("PupilsReception", "-1", "Pupil figures for reception must be 0 or more")]
    [InlineData("PupilsYear1", "0.5", "Pupil figures for year 1 must be a whole number")]
    [InlineData("PupilsYear1", "-1", "Pupil figures for year 1 must be 0 or more")]
    [InlineData("PupilsYear2", "0.5", "Pupil figures for year 2 must be a whole number")]
    [InlineData("PupilsYear2", "-1", "Pupil figures for year 2 must be 0 or more")]
    [InlineData("PupilsYear3", "0.5", "Pupil figures for year 3 must be a whole number")]
    [InlineData("PupilsYear3", "-1", "Pupil figures for year 3 must be 0 or more")]
    [InlineData("PupilsYear4", "0.5", "Pupil figures for year 4 must be a whole number")]
    [InlineData("PupilsYear4", "-1", "Pupil figures for year 4 must be 0 or more")]
    [InlineData("PupilsYear5", "0.5", "Pupil figures for year 5 must be a whole number")]
    [InlineData("PupilsYear5", "-1", "Pupil figures for year 5 must be 0 or more")]
    [InlineData("PupilsYear6", "0.5", "Pupil figures for year 6 must be a whole number")]
    [InlineData("PupilsYear6", "-1", "Pupil figures for year 6 must be 0 or more")]
    public async Task ShowsErrorOnInValidEntryNonMixedAgeSubmit(string prop, string value, string error)
    {
        var composer = Fixture.Build<FinancialPlanInput>()
            .Without(x => x.PupilsNursery)
            .With(x => x.MixedAgeReceptionYear1, false)
            .With(x => x.MixedAgeYear1Year2, false)
            .With(x => x.MixedAgeYear2Year3, false)
            .With(x => x.MixedAgeYear3Year4, false)
            .With(x => x.MixedAgeYear4Year5, false)
            .With(x => x.MixedAgeYear5Year6, false)
            .Without(x => x.PupilsReception)
            .Without(x => x.PupilsYear1)
            .Without(x => x.PupilsYear2)
            .Without(x => x.PupilsYear3)
            .Without(x => x.PupilsYear4)
            .Without(x => x.PupilsYear5)
            .Without(x => x.PupilsYear6);

        var (page, school, plan) = await SetupNavigateInitPage(EstablishmentTypes.Academies, false, OverallPhaseTypes.Primary, composer);
        AssertPageLayout(page, school, plan);
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
            Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, (prop, error));
    }

    [Theory]
    [InlineData("PupilsNursery", "-1", "Pupil figures for nursery must be 0 or more")]
    [InlineData("PupilsMixedReceptionYear1", "0.5", "Pupil figures for reception and year 1 must be a whole number")]
    [InlineData("PupilsMixedReceptionYear1", "-1", "Pupil figures for reception and year 1 must be 0 or more")]
    [InlineData("PupilsMixedYear1Year2", "0.5", "Pupil figures for year 1 and year 2 must be a whole number")]
    [InlineData("PupilsMixedYear1Year2", "-1", "Pupil figures for year 1 and year 2 must be 0 or more")]
    [InlineData("PupilsMixedYear2Year3", "0.5", "Pupil figures for year 2 and year 3 must be a whole number")]
    [InlineData("PupilsMixedYear2Year3", "-1", "Pupil figures for year 2 and year 3 must be 0 or more")]
    [InlineData("PupilsMixedYear3Year4", "0.5", "Pupil figures for year 3 and year 4 must be a whole number")]
    [InlineData("PupilsMixedYear3Year4", "-1", "Pupil figures for year 3 and year 4 must be 0 or more")]
    [InlineData("PupilsMixedYear4Year5", "0.5", "Pupil figures for year 4 and year 5 must be a whole number")]
    [InlineData("PupilsMixedYear4Year5", "-1", "Pupil figures for year 4 and year 5 must be 0 or more")]
    [InlineData("PupilsMixedYear5Year6", "0.5", "Pupil figures for year 5 and year 6 must be a whole number")]
    [InlineData("PupilsMixedYear5Year6", "-1", "Pupil figures for year 5 and year 6 must be 0 or more")]
    public async Task ShowsErrorOnInValidEntryMixedAgeSubmit(string prop, string value, string error)
    {
        var composer = Fixture.Build<FinancialPlanInput>()
            .Without(x => x.PupilsNursery)
            .With(x => x.MixedAgeReceptionYear1, true)
            .With(x => x.MixedAgeYear1Year2, true)
            .With(x => x.MixedAgeYear2Year3, true)
            .With(x => x.MixedAgeYear3Year4, true)
            .With(x => x.MixedAgeYear4Year5, true)
            .With(x => x.MixedAgeYear5Year6, true)
            .Without(x => x.PupilsMixedReceptionYear1)
            .Without(x => x.PupilsMixedYear1Year2)
            .Without(x => x.PupilsMixedYear2Year3)
            .Without(x => x.PupilsMixedYear3Year4)
            .Without(x => x.PupilsMixedYear4Year5)
            .Without(x => x.PupilsMixedYear5Year6);

        var (page, school, plan) = await SetupNavigateInitPage(EstablishmentTypes.Academies, true, OverallPhaseTypes.Primary, composer);
        AssertPageLayout(page, school, plan);
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
            Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, (prop, error));
    }

    private async Task<(IHtmlDocument page, School school, FinancialPlanInput plan)> SetupNavigateInitPage(
        string financeType, bool hasMixedClasses, string overallPhase, IPostprocessComposer<FinancialPlanInput>? planComposer = null)
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
            .With(x => x.HasMixedAgeClasses, hasMixedClasses)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.URN, CurrentYear));

        return (page, school, plan);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, FinancialPlanInput planInput)
    {
        var expectedPage = planInput.HasMixedAgeClasses.GetValueOrDefault()
            ? Paths.SchoolFinancialPlanningMixedAgeClasses(school.URN, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.URN, CurrentYear).ToAbsolute();

        DocumentAssert.BackLink(page, "Back", expectedPage);
        DocumentAssert.TitleAndH1(page,
            "What are your pupil figures? - Financial Benchmarking and Insights Tool - GOV.UK",
            "What are your pupil figures?");
    }
}