using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using AutoFixture.Dsl;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningOtherTeachingPeriodsReview(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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

    public static IEnumerable<object[]> OtherTeachingPeriodsTestData()
    {
        yield return new object[]
        {
        new List<FinancialPlanInput.OtherTeachingPeriod>
            {
                new FinancialPlanInput.OtherTeachingPeriod { PeriodName = "Period1", PeriodsPerTimetable = "6" },
                new FinancialPlanInput.OtherTeachingPeriod { PeriodName = "Period2", PeriodsPerTimetable = "7" },
                new FinancialPlanInput.OtherTeachingPeriod { PeriodName = "Period3", PeriodsPerTimetable = "8" },
                new FinancialPlanInput.OtherTeachingPeriod { PeriodName = "Period4", PeriodsPerTimetable = "9" },
                new FinancialPlanInput.OtherTeachingPeriod { PeriodName = "Period5", PeriodsPerTimetable = "10" }
            }
        };

        yield return new object[]
        {
            new List<FinancialPlanInput.OtherTeachingPeriod>
            {
                new FinancialPlanInput.OtherTeachingPeriod { PeriodName = "Period11", PeriodsPerTimetable = "14" },
                new FinancialPlanInput.OtherTeachingPeriod { PeriodName = "Period12", PeriodsPerTimetable = "15" },
                new FinancialPlanInput.OtherTeachingPeriod { PeriodName = "Period13", PeriodsPerTimetable = "16" }
            }
        };
    }

    [Theory]
    [MemberData(nameof(OtherTeachingPeriodsTestData))]
    public async Task CanDisplayWithValues(List<FinancialPlanInput.OtherTeachingPeriod> otherTeachingPeriods)
    {
        var composer = Fixture.Build<FinancialPlanInput>()
            .With(x => x.OtherTeachingPeriods, otherTeachingPeriods);

        var (page, school) =
            await SetupNavigateInitPage(EstablishmentTypes.Academies, composer);

        AssertPageLayout(page, school);

        var tableRows = page.QuerySelectorAll(".govuk-table__body .govuk-table__row");
        Assert.NotNull(tableRows);
        Assert.Equal(otherTeachingPeriods.Count, tableRows.Length);

        for (var i = 0; i < tableRows.Length; i++)
        {
            var periodName = tableRows[i].QuerySelector(".govuk-table__header");
            Assert.NotNull(periodName);
            Assert.Equal(otherTeachingPeriods[i].PeriodName, periodName.TextContent);

            var periodsPerTimetable = tableRows[i].QuerySelector(".govuk-table__cell");
            Assert.NotNull(periodsPerTimetable);
            Assert.Equal(otherTeachingPeriods[i].PeriodsPerTimetable, periodsPerTimetable.TextContent);
        }
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanContinue(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        AssertPageLayout(page, school);
        var anchor = page.QuerySelector(".govuk-button");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningManagementRoles(school.URN, CurrentYear).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateBack(string financeType)
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.URN, CurrentYear).ToAbsolute());*/
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningOtherTeachingPeriodsReview(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningOtherTeachingPeriodsReview(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }


    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningOtherTeachingPeriodsReview(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningOtherTeachingPeriodsReview(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, IPostprocessComposer<FinancialPlanInput>? planComposer = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .Create();

        planComposer ??= Fixture.Build<FinancialPlanInput>();

        var plan = planComposer
            .With(x => x.Urn, school.URN)
            .With(x => x.Year, CurrentYear)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningOtherTeachingPeriodsReview(school.URN, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.URN, CurrentYear).ToAbsolute());

        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolFinancialPlanningManagementRoles(school.URN, CurrentYear));

        DocumentAssert.TitleAndH1(page,
            "Review other teaching periods - Financial Benchmarking and Insights Tool - GOV.UK",
            "Review other teaching periods");
    }
}
