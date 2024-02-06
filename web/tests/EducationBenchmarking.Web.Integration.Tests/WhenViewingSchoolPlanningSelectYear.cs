using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolPlanningSelectYear(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory,
        output)
{
    private static readonly int CurrentYear = DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    private static readonly int[] ListedYears = Enumerable.Range(CurrentYear, 4).ToArray();
    
    
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
    public async Task CanSelectAYear(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
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
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningYear(school.Urn,CurrentYear).ToAbsolute());
    }
    
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task ShowsErrorOnInValidSelect(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
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
    
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayNotFoundOnSubmit(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        var action = page.QuerySelector(".govuk-button");
        
        Assert.NotNull(action);
        
        SetupEstablishmentWithNotFound();
        
        page = await SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningSelectYear(school.Urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayProblemWithServiceOnSubmit(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        var action = page.QuerySelector(".govuk-button");
        
        Assert.NotNull(action);
        
        SetupEstablishmentWithException();
        
        page = await SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningSelectYear(school.Urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var finances = Fixture.Build<Finances>()
            .Create();

        var page = await SetupEstablishment(school)
            .SetupInsights(school, finances)
            .Navigate(Paths.SchoolCurriculumPlanningSelectYear(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
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