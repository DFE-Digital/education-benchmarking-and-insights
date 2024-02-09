using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolPlanningTotalIncome(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
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
        
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningTotalExpenditure(school.Urn, CurrentYear).ToAbsolute());
    }
    
    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCurriculumPlanningYear(school.Urn, CurrentYear).ToAbsolute());
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
            .Navigate(Paths.SchoolCurriculumPlanningTotalIncome(school.Urn, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCurriculumPlanningYear(school.Urn, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Total Income", "Total Income");
    }
}