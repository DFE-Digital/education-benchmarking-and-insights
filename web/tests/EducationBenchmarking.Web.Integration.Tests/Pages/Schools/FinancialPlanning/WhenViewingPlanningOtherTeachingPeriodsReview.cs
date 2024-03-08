using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using AutoFixture.Dsl;
using EducationBenchmarking.Web.Domain;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningOtherTeachingPeriodsReview(BenchmarkingWebAppClient client) : PageBase(client)
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
        new List<FinancialPlan.OtherTeachingPeriod>
        {
            new FinancialPlan.OtherTeachingPeriod { PeriodName = "Period1", PeriodsPerTimetable = "6" },
            new FinancialPlan.OtherTeachingPeriod { PeriodName = "Period2", PeriodsPerTimetable = "7" },
            new FinancialPlan.OtherTeachingPeriod { PeriodName = "Period3", PeriodsPerTimetable = "8" },
            new FinancialPlan.OtherTeachingPeriod { PeriodName = "Period4", PeriodsPerTimetable = "9" },
            new FinancialPlan.OtherTeachingPeriod { PeriodName = "Period5", PeriodsPerTimetable = "10" }
        }
        };

        yield return new object[]
        {
        new List<FinancialPlan.OtherTeachingPeriod>
        {
            new FinancialPlan.OtherTeachingPeriod { PeriodName = "Period11", PeriodsPerTimetable = "14" },
            new FinancialPlan.OtherTeachingPeriod { PeriodName = "Period12", PeriodsPerTimetable = "15" },
            new FinancialPlan.OtherTeachingPeriod { PeriodName = "Period13", PeriodsPerTimetable = "16" }
        }
        };
    }

    [Theory]
    [MemberData(nameof(OtherTeachingPeriodsTestData))]
    public async Task CanDisplayWithValues(List<FinancialPlan.OtherTeachingPeriod> otherTeachingPeriods)
    {

        var composer = Fixture.Build<FinancialPlan>()
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningManagementRoles(school.Urn, CurrentYear).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateBack(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.Urn, CurrentYear).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupBenchmarkWithNotFound()
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

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, IPostprocessComposer<FinancialPlan>? planComposer = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
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
            .Navigate(Paths.SchoolFinancialPlanningOtherTeachingPeriodsReview(school.Urn, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.Urn, CurrentYear).ToAbsolute());
        
        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolFinancialPlanningManagementRoles(school.Urn, CurrentYear));
        
        DocumentAssert.TitleAndH1(page,
            "Review other teaching periods - Education benchmarking and insights - GOV.UK",
            "Review other teaching periods");
    }
}
