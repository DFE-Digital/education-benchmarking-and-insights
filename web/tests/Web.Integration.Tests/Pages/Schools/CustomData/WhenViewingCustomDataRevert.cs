using System.Collections.Concurrent;
using System.Text;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Extensions;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.CustomData;

public class WhenViewingCustomDataRevert(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.URN).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(bool setupUserData = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var key = SessionKeys.CustomData(school.URN!);
        var customData = new App.Domain.CustomData
        {
            AdministrativeClericalStaffCosts = 12345.67m
        };
        var sessionState = new ConcurrentDictionary<string, byte[]>
        {
            [key] = Encoding.ASCII.GetBytes(customData.ToJson())
        };

        var client = Client.SetupEstablishment(school)
            .SetupInsights()
            .SetUpCustomData()
            .SetupHttpContextAccessor(sessionState);

        if (setupUserData)
        {
            var userData = Fixture.Build<UserData>()
                .With(x => x.Type, "custom-data")
                .Create();
            var balance = Fixture.Build<SchoolBalance>()
                .With(x => x.SchoolName, school.SchoolName)
                .With(x => x.URN, school.URN)
                .Create();

            client
                .SetupUserData([userData])
                .SetupMetricRagRating()
                .SetupBalance(balance);
        }

        var page = await client.Navigate(Paths.SchoolCustomDataRevert(school.URN));
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
            "Change back to the original data? - Financial Benchmarking and Insights Tool - GOV.UK",
            "Change back to the original data?");
        var cta = page.QuerySelector("main .govuk-button");
        DocumentAssert.PrimaryCta(cta, "Remove custom data", Paths.SchoolCustomDataRevert(school.URN));
        var change = page.QuerySelector("#cancel-revert");
        DocumentAssert.Link(change, "Cancel", Paths.SchoolHome(school.URN).ToAbsolute());
    }
}