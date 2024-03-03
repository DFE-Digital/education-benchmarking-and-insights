using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Moq;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningPrePopulateData(BenchmarkingWebAppClient client) : PageBase(client)
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
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplayWithPreviousValue(bool useFigures)
    {
        var (page, _, _) =
            await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Primary, useFigures);

        var radios = page.QuerySelector(".govuk-radios--inline");
        Assert.NotNull(radios);
        var options = new[] { ("UseFigures", "true", "Yes", useFigures), ("UseFigures", "false", "No", !useFigures) };

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

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "UseFigures", true.ToString() }
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, PlanYear).ToAbsolute());
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

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "UseFigures", false.ToString() }
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningTotalIncome(school.Urn, PlanYear).ToAbsolute());
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

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        AssertPageLayout(page, school, finances);
        DocumentAssert.FormErrors(page, ("UseFigures", "Select yes if you want to use these figures"));
    }


    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupBenchmarkWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningPrePopulatedData(urn, PlanYear));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningPrePopulatedData(urn, PlanYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningPrePopulatedData(urn, PlanYear));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningPrePopulatedData(urn, PlanYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school, _) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Primary);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningPrePopulatedData(school.Urn, PlanYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceOnSubmit()
    {
        var (page, school, _) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Primary);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningPrePopulatedData(school.Urn, PlanYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school, _) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Primary);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningSelectYear(school.Urn).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school, Finances finances)> SetupNavigateInitPage(string financeType,
        string phase, bool? useFigures = null)
    {
        var school = Fixture.Build<School>()
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

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools, plan)
            .Navigate(Paths.SchoolFinancialPlanningPrePopulatedData(school.Urn, PlanYear));

        return (page, school, finances);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, Finances finances)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningSelectYear(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Prepopulated data - Education benchmarking and insights - GOV.UK", "Prepopulated data");
        DocumentAssert.Heading2(page, $"Figures from {finances.YearEnd - 1} - {finances.YearEnd}");

        AssertCostsTable(page, school, finances);

        var radios = page.QuerySelector(".govuk-radios--inline");

        Assert.NotNull(radios);
        var options = new[] { ("UseFigures", "true", "Yes", false), ("UseFigures", "false", "No", false) };
        DocumentAssert.Radios(radios, options);

        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue",
            Paths.SchoolFinancialPlanningPrePopulatedData(school.Urn, PlanYear));
    }

    private static void AssertCostsTable(IParentNode page, School school, Finances finances)
    {
        var values = page.QuerySelectorAll("dd");

        if (school.OverallPhase == OverallPhaseTypes.Primary)
        {
            Assert.Equal(5, values.Length);
            DocumentAssert.TextEqual(values[0], $"{finances.TotalIncome:C}");
            DocumentAssert.TextEqual(values[1], $"{finances.TotalExpenditure:C}");
            DocumentAssert.TextEqual(values[2], $"{finances.TeachingStaffCosts:C}");
            DocumentAssert.TextEqual(values[3], $"{finances.EducationSupportStaffCosts:C}");
            DocumentAssert.TextEqual(values[4], $"{finances.TotalNumberOfTeachersFte:F1}");
        }
        else
        {
            Assert.Equal(4, values.Length);
            DocumentAssert.TextEqual(values[0], $"{finances.TotalIncome:C}");
            DocumentAssert.TextEqual(values[1], $"{finances.TotalExpenditure:C}");
            DocumentAssert.TextEqual(values[2], $"{finances.TeachingStaffCosts:C}");
            DocumentAssert.TextEqual(values[3], $"{finances.TotalNumberOfTeachersFte:F1}");
        }
    }
}