using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningMixedAgeClasses(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.URN, CurrentYear).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .SetupBenchmarkWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningMixedAgeClasses(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningMixedAgeClasses(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        var action = page.QuerySelector(".govuk-button");

        Assert.NotNull(action);

        Client.SetupBenchmarkWithNotFound();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningMixedAgeClasses(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.NotFound);
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

        Client.SetupBenchmarkWithException();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningMixedAgeClasses(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(true, true, true, true, true, true)]
    [InlineData(false, true, false, true, false, true)]
    [InlineData(true, false, true, false, true, false)]
    [InlineData(true, false, false, false, false, false)]
    [InlineData(false, true, false, false, false, false)]
    [InlineData(false, false, true, false, false, false)]
    [InlineData(false, false, false, true, false, false)]
    [InlineData(false, false, false, false, true, false)]
    [InlineData(false, false, false, false, true, true)]
    public async Task CanSubmit(bool mixedAgeReceptionYear1, bool mixedAgeYear1Year2, bool mixedAgeYear2Year3,
        bool mixedAgeYear3Year4, bool mixedAgeYear4Year5, bool mixedAgeYear5Year6)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "MixedAgeReceptionYear1", mixedAgeReceptionYear1.ToString() },
                { "MixedAgeYear1Year2", mixedAgeYear1Year2.ToString() },
                { "MixedAgeYear2Year3", mixedAgeYear2Year3.ToString() },
                { "MixedAgeYear3Year4", mixedAgeYear3Year4.ToString() },
                { "MixedAgeYear4Year5", mixedAgeYear4Year5.ToString() },
                { "MixedAgeYear5Year6", mixedAgeYear5Year6.ToString() }
            });
        });

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningPrimaryPupilFigures(school.URN, CurrentYear).ToAbsolute());
    }

    [Theory]
    [InlineData(true, true, true, true, true, true)]
    [InlineData(false, false, false, false, false, false)]
    [InlineData(false, true, false, true, false, true)]
    [InlineData(true, false, true, false, true, false)]
    public async Task CanDisplayWithPreviousValue(bool mixedAgeReceptionYear1, bool mixedAgeYear1Year2,
        bool mixedAgeYear2Year3, bool mixedAgeYear3Year4, bool mixedAgeYear4Year5, bool mixedAgeYear5Year6)
    {
        var (page, _) =
            await SetupNavigateInitPage(EstablishmentTypes.Academies, mixedAgeReceptionYear1, mixedAgeYear1Year2,
                mixedAgeYear2Year3, mixedAgeYear3Year4, mixedAgeYear4Year5, mixedAgeYear5Year6);

        var checkboxes = page.QuerySelector(".govuk-checkboxes");
        Assert.NotNull(checkboxes);
        var options = new[]
        {
            ("MixedAgeReceptionYear1", "Reception and year 1", mixedAgeReceptionYear1),
            ("MixedAgeYear1Year2", "Year 1 and year 2", mixedAgeYear1Year2),
            ("MixedAgeYear2Year3", "Year 2 and year 3", mixedAgeYear2Year3),
            ("MixedAgeYear3Year4", "Year 3 and year 4", mixedAgeYear3Year4),
            ("MixedAgeYear4Year5", "Year 4 and year 5", mixedAgeYear4Year5),
            ("MixedAgeYear5Year6", "Year 5 and year 6", mixedAgeYear5Year6)
        };

        DocumentAssert.Checkboxes(checkboxes, options);
    }

    [Fact]
    public async Task ShowsErrorOnInValidSubmit()
    {

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);


        page = await Client.SubmitForm(page.Forms[0], action);

        Client.BenchmarkApi.Verify(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningMixedAgeClasses(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, ("mixing-classes", "Select which years have mixed age classes"));
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType,
        bool? mixedAgeReceptionYear1 = null, bool? mixedAgeYear1Year2 = null, bool? mixedAgeYear2Year3 = null,
        bool? mixedAgeYear3Year4 = null, bool? mixedAgeYear4Year5 = null, bool? mixedAgeYear5Year6 = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .Create();

        var finances = Fixture.Build<Finances>()
            .Create();

        var plan = Fixture.Build<FinancialPlanInput>()
            .With(x => x.Urn, school.URN)
            .With(x => x.Year, CurrentYear)
            .With(x => x.HasMixedAgeClasses, true)
            .With(x => x.MixedAgeReceptionYear1, mixedAgeReceptionYear1)
            .With(x => x.MixedAgeYear1Year2, mixedAgeYear1Year2)
            .With(x => x.MixedAgeYear2Year3, mixedAgeYear2Year3)
            .With(x => x.MixedAgeYear3Year4, mixedAgeYear3Year4)
            .With(x => x.MixedAgeYear4Year5, mixedAgeYear4Year5)
            .With(x => x.MixedAgeYear5Year6, mixedAgeYear5Year6)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools, plan)
            .Navigate(Paths.SchoolFinancialPlanningMixedAgeClasses(school.URN, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back",
            Paths.SchoolFinancialPlanningHasMixedAgeClasses(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Which years have mixed age classes? - Financial Benchmarking and Insights Tool - GOV.UK",
            "Which years have mixed age classes?");
    }
}