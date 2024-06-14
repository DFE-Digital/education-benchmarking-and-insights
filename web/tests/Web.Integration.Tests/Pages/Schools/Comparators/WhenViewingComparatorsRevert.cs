using System.Collections.Concurrent;
using System.Text;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Domain.Benchmark;
using Web.App.Domain.Insight;
using Web.App.Extensions;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.Comparators;

public class WhenViewingComparatorsRevert(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var key = SessionKeys.ComparatorSetUserDefined(school.URN!);
        var set = new UserDefinedSchoolComparatorSet
        {
            Set = ["1", "2", "3"],
            TotalSchools = 123
        };
        var sessionState = new ConcurrentDictionary<string, byte[]>
        {
            [key] = Encoding.ASCII.GetBytes(set.ToJson())
        };

        var page = await Client.SetupEstablishment(school)
            .SetupSchoolInsightApi(new[]
            {
                new SchoolCharacteristic
                {
                    URN = "114504",
                    SchoolName = "Forest Row Church of England Primary School",
                    OverallPhase = "Primary",
                    Address = "Forest Row, RH18 5EB"
                }
            })
            .SetupComparatorSetApi()
            .SetupHttpContextAccessor(sessionState)
            .Navigate($"{Paths.SchoolComparatorsRevert(school.URN)}?comparator-generated=true");

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute()),
            ("Comparator sets", Paths.SchoolComparators(school.URN).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page,
            "Change back to the schools we chose? - Financial Benchmarking and Insights Tool - GOV.UK",
            "Change back to the schools we chose?");
        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Change back", Paths.SchoolComparatorsRevert(school.URN));
        var change = page.QuerySelector("#cancel-revert");
        DocumentAssert.Link(change, "Cancel", Paths.SchoolHome(school.URN).ToAbsolute());
    }
}