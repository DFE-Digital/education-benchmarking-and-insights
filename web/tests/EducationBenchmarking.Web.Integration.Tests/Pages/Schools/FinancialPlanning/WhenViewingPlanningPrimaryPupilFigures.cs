using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using AutoFixture.Dsl;
using EducationBenchmarking.Web.Domain;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningPrimaryPupilFigures(BenchmarkingWebAppClient client) : PageBase(client)
{
    private static readonly int CurrentYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    
    [Theory]
    [InlineData(EstablishmentTypes.Academies, true)]
    [InlineData(EstablishmentTypes.Academies, false)]
    [InlineData(EstablishmentTypes.Maintained, true)]
    [InlineData(EstablishmentTypes.Maintained, false)]
    public async Task CanDisplay(string financeType, bool hasMixedAge)
    {
        var (page, school, plan) = await SetupNavigateInitPage(financeType, hasMixedAge);

        AssertPageLayout(page, school, plan);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanNavigateBack(bool hasMixedClasses)
    {
        var (page, school, _) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, hasMixedClasses);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        var expectedPage = hasMixedClasses
            ? Paths.SchoolFinancialPlanningMixedAgeClasses(school.Urn, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.Urn, CurrentYear).ToAbsolute();
        
        DocumentAssert.AssertPageUrl(page, expectedPage);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningPrimaryPupilFigures(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningPrimaryPupilFigures(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }


    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningPrimaryPupilFigures(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningPrimaryPupilFigures(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }
    
    private async Task<(IHtmlDocument page, School school, FinancialPlan plan)> SetupNavigateInitPage(string financeType, bool hasMixedClasses,IPostprocessComposer<FinancialPlan>? planComposer = null)
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
            .With(x => x.HasMixedAgeClasses, hasMixedClasses)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools, plan)
            .Navigate(Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.Urn, CurrentYear));

        return (page, school, plan);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, FinancialPlan plan)
    {
        var expectedPage = plan.HasMixedAgeClasses.GetValueOrDefault()
            ? Paths.SchoolFinancialPlanningMixedAgeClasses(school.Urn, CurrentYear).ToAbsolute()
            : Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.Urn, CurrentYear).ToAbsolute();
        
        DocumentAssert.BackLink(page, "Back", expectedPage);
        DocumentAssert.TitleAndH1(page,
            "What are your pupil figures? - Education benchmarking and insights - GOV.UK",
            "What are your pupil figures?");
    }
}