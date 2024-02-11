using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.School.Planning;

public class WhenViewingSchoolPlanningTotalExpenditure(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory,
        output)
{
    private static readonly int CurrentYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplaySchool(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        AssertPageLayout(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanSubmitForm(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolCurriculumPlanningTotalTeacherCost(school.Urn, CurrentYear).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolCurriculumPlanningTotalIncome(school.Urn, CurrentYear).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, Domain.School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<Domain.School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var finances = Fixture.Build<Finances>()
            .Create();

        var plan = Fixture.Build<FinancialPlan>()
            .With(x => x.Urn, school.Urn)
            .With(x => x.Year, CurrentYear)
            .With(x => x.UseFigures, false)
            .Create();
        
        var page = await SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(plan)
            .Navigate(Paths.SchoolCurriculumPlanningTotalExpenditure(school.Urn, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, Domain.School school)
    {
        DocumentAssert.BackLink(page, "Back",
            Paths.SchoolCurriculumPlanningTotalIncome(school.Urn, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Total expenditure", "Total expenditure");
    }

}