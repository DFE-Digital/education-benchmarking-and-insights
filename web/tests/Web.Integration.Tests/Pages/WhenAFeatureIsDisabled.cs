using System.Net;
using Web.App;
using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenAFeatureIsDisabled(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task LocalAuthorityHighNeedsRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.HighNeeds)
            .Navigate(Paths.LocalAuthorityHighNeedsDashboard("123"));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsDashboard("123").ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task NewsArticleRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.News)
            .Navigate(Paths.News("slug"));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.News("slug").ToAbsolute(), HttpStatusCode.Forbidden);
    }
}