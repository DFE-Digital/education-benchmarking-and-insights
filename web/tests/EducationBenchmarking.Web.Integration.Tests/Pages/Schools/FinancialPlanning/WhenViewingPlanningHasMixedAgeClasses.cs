using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Moq;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningHasMixedAgeClasses(BenchmarkingWebAppClient client) : PageBase(client)
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
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, CurrentYear).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningHasMixedAgeClasses(urn, year));


        var expectedUrl = Paths.SchoolFinancialPlanningHasMixedAgeClasses(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.Urn, CurrentYear).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningHasMixedAgeClasses(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningHasMixedAgeClasses(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    [Fact]
    public async Task CanDisplayProblemWithServiceOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupEstablishmentWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.Urn, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task ShowsErrorOnInValidSubmit()
    {

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);


        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "HasMixedAgeClasses",  "" }
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.Urn, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, ("mixed-age", "Select yes if you have mixed age classes"));
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, true)]
    [InlineData(EstablishmentTypes.Maintained, false)]
    public async Task CanSubmit(string financeType, bool value)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "HasMixedAgeClasses",  value.ToString()}
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.Urn, CurrentYear).ToAbsolute());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplayWithPreviousValue(bool value)
    {
        var (page, _) =
            await SetupNavigateInitPage(EstablishmentTypes.Academies, value);

        var radios = page.QuerySelector(".govuk-radios--inline");
        Assert.NotNull(radios);
        var options = new[] { ("HasMixedAgeClasses", "true", "Yes", value), ("HasMixedAgeClasses", "false", "No", !value) };

        DocumentAssert.Radios(radios, options);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool? hasMixedAgeClasses = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var finances = Fixture.Build<Finances>()
            .Create();

        var plan = Fixture.Build<FinancialPlan>()
            .With(x => x.Urn, school.Urn)
            .With(x => x.Year, CurrentYear)
            .With(x => x.HasMixedAgeClasses, hasMixedAgeClasses)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools, plan)
            .Navigate(Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.Urn, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanningTimetableCycle(school.Urn, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Do you have any mixed age classes? - Education benchmarking and insights - GOV.UK", "Do you have any mixed age classes?");
    }
}