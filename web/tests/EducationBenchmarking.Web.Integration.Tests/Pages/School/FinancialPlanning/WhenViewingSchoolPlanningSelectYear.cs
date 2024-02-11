using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.School.FinancialPlanning;

public class WhenViewingSchoolPlanningSelectYear(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory,
        output)
{
    private static readonly int CurrentYear = DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    private static readonly int[] ListedYears = Enumerable.Range(CurrentYear, 4).ToArray();
    
    
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        AssertPageLayout(page, school);
    }
    
    [Fact]
    public async Task CanSelectAYear()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);
        
        page = await SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "year", CurrentYear.ToString() }
            });
        });
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningPrePopulatedData(school.Urn,CurrentYear).ToAbsolute());
    }
    
    [Fact]
    public async Task ShowsErrorOnInValidSelect()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);
        
        page = await SubmitForm(page.Forms[0], action);
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningSelectYear(school.Urn).ToAbsolute());
        DocumentAssert.FormErrors(page, ("year","Select an academic year"));
    }
    
    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolCurriculumPlanningSelectYear(urn));

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningSelectYear(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolCurriculumPlanningSelectYear(urn));

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningSelectYear(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }
    
    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");
        
        Assert.NotNull(action);
        
        SetupEstablishmentWithNotFound();
        
        page = await SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningSelectYear(school.Urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");
        
        Assert.NotNull(action);
        
        SetupEstablishmentWithException();
        
        page = await SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningSelectYear(school.Urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningStart(school.Urn).ToAbsolute());
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
            .Create();

        var page = await SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(plan)
            .Navigate(Paths.SchoolCurriculumPlanningSelectYear(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, Domain.School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCurriculumPlanningStart(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Select academic year to plan", "Select academic year to plan");

        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolCurriculumPlanningSelectYear(school.Urn));
        
        var radios = page.QuerySelectorAll("input[type='radio']");
        Assert.Equal(ListedYears.Length, radios.Length);
        Assert.Equal(ListedYears[0].ToString(), radios[0].GetAttribute("value"));
        Assert.Equal(ListedYears[1].ToString(), radios[1].GetAttribute("value"));
        Assert.Equal(ListedYears[2].ToString(), radios[2].GetAttribute("value"));
        Assert.Equal(ListedYears[3].ToString(), radios[3].GetAttribute("value"));
    }
}