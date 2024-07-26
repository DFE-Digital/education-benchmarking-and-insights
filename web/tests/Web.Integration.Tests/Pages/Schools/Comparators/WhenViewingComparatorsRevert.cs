using System.Collections.Concurrent;
using System.Text;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
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

    [Fact]
    public async Task CanRevert()
    {
        var (page, school) = await SetupNavigateInitPage(true);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute() + "?comparator-reverted=true");
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(bool setupUserData = false)
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

        var client = Client.SetupEstablishment(school)
            .SetupInsights()
            .SetupComparatorSetApi()
            .SetupHttpContextAccessor(sessionState);

        if (setupUserData)
        {
            var userData = Fixture.Build<UserData>()
                .With(x => x.Type, "comparator-set")
                .Create();
            var balance = Fixture.Build<SchoolBalance>()
                .With(x => x.SchoolName, school.SchoolName)
                .With(x => x.URN, school.URN)
                .Create();

            client
                .SetupUserData([userData])
                .SetupMetricRagRatingUserDefined()
                .SetupBalance(balance);
        }

        var page = await client.Navigate(Paths.SchoolComparatorsRevert(school.URN));
        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page,
            "Change back to the schools we chose? - Financial Benchmarking and Insights Tool - GOV.UK",
            "Change back to the schools we chose?");
        var cta = page.QuerySelector("main .govuk-button");
        DocumentAssert.PrimaryCta(cta, "Change back", Paths.SchoolComparatorsRevert(school.URN));
        var change = page.QuerySelector("#cancel-revert");
        DocumentAssert.Link(change, "Cancel", Paths.SchoolHome(school.URN).ToAbsolute());
    }
}