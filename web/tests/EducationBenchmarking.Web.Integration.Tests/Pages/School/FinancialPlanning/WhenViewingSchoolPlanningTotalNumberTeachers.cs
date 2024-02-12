using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.School.FinancialPlanning;

public class WhenViewingSchoolPlanningTotalNumberTeachers(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory,
        output)
{
    private static readonly int CurrentYear = DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        AssertPageLayout(page, school);
    }
    
    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningTotalTeacherCost(school.Urn, CurrentYear).ToAbsolute());
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
            .Without(x => x.TotalNumberOfTeachersFte)
            .Create();
        
        var page = await SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(plan)
            .Navigate(Paths.SchoolCurriculumPlanningTotalNumberTeachers(school.Urn, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, Domain.School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCurriculumPlanningTotalTeacherCost(school.Urn, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Total number of teachers", "Total number of teachers");
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

        page = await SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TotalNumberTeachers",  19.5.ToString()}
            });
        });
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningTotalNumberTeachers(school.Urn, CurrentYear).ToAbsolute());
    }
    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolCurriculumPlanningTotalNumberTeachers(urn, year));

        
        var expectedUrl = Paths.SchoolCurriculumPlanningTotalNumberTeachers(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
    }

    
    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolCurriculumPlanningTotalNumberTeachers(urn, year));

        var expectedUrl = Paths.SchoolCurriculumPlanningTotalNumberTeachers(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
    }
    

    [Fact]
    public async Task ShowsErrorOnInValidSubmit()
    {
        
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);
        
        
        page = await SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TotalNumberTeachers",  ""}
            });
        });
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningTotalNumberTeachers(school.Urn, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, ("total-number-teacher","Enter number of FTE teachers"));
    }

    
    
}