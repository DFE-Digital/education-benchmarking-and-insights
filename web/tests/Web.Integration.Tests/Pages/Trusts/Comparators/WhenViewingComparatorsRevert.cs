using System.Collections.Concurrent;
using System.Text;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Extensions;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparatorsCreateRevert(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
    }

    [Fact]
    public async Task CanRevert()
    {
        var (page, trust) = await SetupNavigateInitPage(true);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);
        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(trust.CompanyNumber).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage(bool setupUserData = false)
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345")
            .Create();

        var key = SessionKeys.TrustComparatorSetUserDefined(trust.CompanyNumber!);
        var set = new UserDefinedTrustComparatorSet
        {
            Set = ["1", "2", "3"],
            TotalTrusts = 123
        };
        var sessionState = new ConcurrentDictionary<string, byte[]>
        {
            [key] = Encoding.ASCII.GetBytes(set.ToJson())
        };

        var client = Client.SetupEstablishment(trust)
            .SetupBalance(trust)
            .SetupTrustInsightApi()
            .SetupComparatorSetApi()
            .SetupHttpContextAccessor(sessionState);

        if (setupUserData)
        {
            var userData = Fixture.Build<UserData>()
                .With(x => x.Type, "comparator-set")
                .Create();
            client
                .SetupUserData([userData])
                .SetupMetricRagRating()
                .SetupInsights();
        }

        var page = await client.Navigate(Paths.TrustComparatorsRevert(trust.CompanyNumber));
        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your trust", Paths.TrustHome(trust.CompanyNumber).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page,
            "Remove all your trusts? - Financial Benchmarking and Insights Tool - GOV.UK",
            "Remove all your trusts?");
        var cta = page.QuerySelector("main .govuk-button");
        DocumentAssert.PrimaryCta(cta, "Remove all trusts", Paths.TrustComparatorsRevert(trust.CompanyNumber));
        var change = page.QuerySelector("#cancel-revert");
        DocumentAssert.Link(change, "Cancel", Paths.TrustHome(trust.CompanyNumber).ToAbsolute());
    }
}