using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeedsBenchmarking(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority, set) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority, set);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityHighNeedsBenchmarking(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsBenchmarking(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFoundForEstablishment()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHighNeedsBenchmarking(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsBenchmarking(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayNotFoundForSet()
    {
        var (page, authority, _) = await SetupNavigateInitPage([]);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsBenchmarking(authority.Code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    private async Task<(IHtmlDocument page, LocalAuthority authority, string[] set)> SetupNavigateInitPage(string[]? comparatorSet = null)
    {
        var authority = Fixture.Build<LocalAuthority>().Create();
        var set = comparatorSet ?? Fixture.Build<string>().CreateMany().ToArray();

        var page = await Client.SetupEstablishment(authority)
            .SetupInsights()
            .SetupLocalAuthoritiesComparators(authority.Code!, set)
            .Navigate(Paths.LocalAuthorityHighNeedsBenchmarking(authority.Code));

        return (page, authority, set);
    }

    private static void AssertPageLayout(IHtmlDocument page, LocalAuthority authority, string[] _)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsBenchmarking(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Benchmark High needs - Financial Benchmarking and Insights Tool - GOV.UK", "Benchmark High needs");

        var backLink = page.QuerySelector("a.govuk-back-link") as IHtmlAnchorElement;
        Assert.NotNull(backLink);
        Assert.Equal(Paths.LocalAuthorityHighNeedsDashboard(authority.Code).ToAbsolute(), backLink.Href);
    }
}