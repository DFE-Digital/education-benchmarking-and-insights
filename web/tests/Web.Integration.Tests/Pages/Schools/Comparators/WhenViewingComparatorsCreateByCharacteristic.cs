using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.Comparators;

public class WhenViewingComparatorsCreateByCharacteristic(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanSubmitCharacteristics()
    {
        var (page, school) = await SetupNavigateInitPage();
        var action = page.QuerySelector("#submit-characteristic");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparatorsCreatePreview(school.URN).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, EstablishmentTypes.Academies)
            .With(x => x.OverallPhase, OverallPhaseTypes.Primary)
            .With(x => x.LAName, "Kent")
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupSchoolInsight(new[]
            {
                new SchoolCharacteristic
                {
                    URN = school.URN,
                    SchoolName = school.SchoolName,
                    FinanceType = school.FinanceType,
                    OverallPhase = school.OverallPhase,
                    LAName = school.LAName
                }
            })
            .SetupComparatorApi(new ComparatorSchools
            {
                Schools = new[]
                {
                    "1",
                    "2",
                    "3"
                },
                TotalSchools = 123
            })
            .SetupHttpContextAccessor()
            .Navigate(Paths.SchoolComparatorsCreateByCharacteristic(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolComparatorsCreateBy(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Choose characteristics to find matching schools - Financial Benchmarking and Insights Tool - GOV.UK",
            "Choose characteristics to find matching schools");
        var cta = page.QuerySelector("#submit-characteristic");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolComparatorsCreateByCharacteristic(school.URN));
    }
}