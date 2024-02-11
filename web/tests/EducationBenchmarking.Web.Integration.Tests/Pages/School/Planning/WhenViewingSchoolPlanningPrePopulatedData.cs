using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.School.Planning;

public class WhenViewingSchoolPlanningPrePopulatedData(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory,
        output)
{
    private static readonly int PlanYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    private static readonly int FinancesYear = DateTime.UtcNow.Year - 2;
    
    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    public async Task CanDisplay(string financeType, string phase)
    {
        var (page, school, finances) = await SetupNavigateInitPage(financeType, phase);

        AssertPageLayout(page, school, finances);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary, true)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary, false)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary, false)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary, true)]
    public async Task CanDisplayWithPreviousValue(string financeType, string phase, bool useFigures)
    {
        var (page, _, _) = await SetupNavigateInitPage(financeType, phase, useFigures);

        var radios = page.QuerySelector(".govuk-radios--inline");
        Assert.NotNull(radios);
        var options = new[] { ("useFigures", "true", "Yes", useFigures), ("useFigures", "false", "No", !useFigures) };

        DocumentAssert.Radios(radios, options);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    public async Task CanSelectToUseFigure(string financeType, string phase)
    {
        var (page, school, finances) = await SetupNavigateInitPage(financeType, phase);
        AssertPageLayout(page, school, finances);

        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "useFigures", true.ToString() }
            });
        });

        BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningTimetable(school.Urn, PlanYear).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    public async Task CanSelectToNotUseFigure(string financeType, string phase)
    {
        var (page, school, finances) = await SetupNavigateInitPage(financeType, phase);
        AssertPageLayout(page, school, finances);

        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "useFigures", false.ToString() }
            });
        });

        BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolCurriculumPlanningTotalIncome(school.Urn, PlanYear).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    public async Task ShowsErrorOnInValidSelect(string financeType, string phase)
    {
        var (page, school, finances) = await SetupNavigateInitPage(financeType, phase);
        AssertPageLayout(page, school, finances);

        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await SubmitForm(page.Forms[0], action);

        BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        AssertPageLayout(page, school, finances);
        DocumentAssert.FormErrors(page, ("use-figures", "Select whether to use the above figures in your plan"));
    }


    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolCurriculumPlanningPrePopulatedData(urn, PlanYear));

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolCurriculumPlanningPrePopulatedData(urn, PlanYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolCurriculumPlanningPrePopulatedData(urn, PlanYear));

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolCurriculumPlanningPrePopulatedData(urn, PlanYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    public async Task CanDisplayNotFoundOnSubmit(string financeType, string phase)
    {
        var (page, school, _) = await SetupNavigateInitPage(financeType, phase);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        SetupEstablishmentWithNotFound();

        page = await SubmitForm(page.Forms[0], action);

        BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolCurriculumPlanningPrePopulatedData(school.Urn, PlanYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    public async Task CanDisplayProblemWithServiceOnSubmit(string financeType, string phase)
    {
        var (page, school, _) = await SetupNavigateInitPage(financeType, phase);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        SetupEstablishmentWithException();

        page = await SubmitForm(page.Forms[0], action);

        BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolCurriculumPlanningPrePopulatedData(school.Urn, PlanYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }


    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    public async Task CanNavigateBack(string financeType, string phase)
    {
        var (page, school, _) = await SetupNavigateInitPage(financeType, phase);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningSelectYear(school.Urn).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, Domain.School school, Finances finances)> SetupNavigateInitPage(string financeType,
        string phase, bool? useFigures = null)
    {
        var school = Fixture.Build<Domain.School>()
            .With(x => x.FinanceType, financeType)
            .With(x => x.OverallPhase, phase)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .With(x => x.YearEnd, FinancesYear)
            .Create();

        var plan = Fixture.Build<FinancialPlan>()
                .With(x => x.Urn, school.Urn)
                .With(x => x.Year, PlanYear)
                .With(x => x.UseFigures, useFigures)
                .Create();
        
        var page = await SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(plan)
            .Navigate(Paths.SchoolCurriculumPlanningPrePopulatedData(school.Urn, PlanYear));

        return (page, school, finances);
    }

    private static void AssertPageLayout(IHtmlDocument page, Domain.School school, Finances finances)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCurriculumPlanningSelectYear(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Prepopulated data", "Prepopulated data");
        DocumentAssert.Heading2(page, $"Figures from {finances.YearEnd - 1} - {finances.YearEnd}");

        AssertCostsTable(page, school, finances);

        var radios = page.QuerySelector(".govuk-radios--inline");

        Assert.NotNull(radios);
        var options = new[] { ("useFigures", "true", "Yes", false), ("useFigures", "false", "No", false) };
        DocumentAssert.Radios(radios, options);

        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue",
            Paths.SchoolCurriculumPlanningPrePopulatedData(school.Urn, PlanYear));
    }

    private static void AssertCostsTable(IParentNode page, Domain.School school, Finances finances)
    {
        var values = page.QuerySelectorAll("dd");

        if (school.OverallPhase == OverallPhaseTypes.Primary)
        {
            Assert.Equal(5, values.Length);
            Assert.Equal($"{finances.TotalIncome:C}", values[0].TextContent.Trim());
            Assert.Equal($"{finances.TotalExpenditure:C}", values[1].TextContent.Trim());
            Assert.Equal($"{finances.TeachingStaffCosts:C}", values[2].TextContent.Trim());
            Assert.Equal($"{finances.EducationSupportStaffCosts:C}", values[3].TextContent.Trim());
            Assert.Equal($"{finances.TotalNumberOfTeachersFte:F1}", values[4].TextContent.Trim());
        }
        else
        {
            Assert.Equal(4, values.Length);
            Assert.Equal($"{finances.TotalIncome:C}", values[0].TextContent.Trim());
            Assert.Equal($"{finances.TotalExpenditure:C}", values[1].TextContent.Trim());
            Assert.Equal($"{finances.TeachingStaffCosts:C}", values[2].TextContent.Trim());
            Assert.Equal($"{finances.TotalNumberOfTeachersFte:F1}", values[3].TextContent.Trim());
        }
    }
}