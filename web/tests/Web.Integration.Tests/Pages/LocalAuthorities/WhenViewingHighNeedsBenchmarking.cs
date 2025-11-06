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
        var authority = Fixture.Build<LocalAuthority>()
            .With(a => a.Code, "123")
            .Create();
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

        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Benchmark high needs - Financial Benchmarking and Insights Tool - GOV.UK", "Benchmark high needs");

        AssertHelpSection(page);
    }

    private static void AssertHelpSection(IHtmlDocument page)
    {
        var links = page.QuerySelectorAll(".app-links li > a");
        Assert.Equal(8, links.Length);

        Assert.Equal("SEND framework", links.ElementAt(0).TextContent.Trim());
        Assert.Equal("High needs budgets: effective management in LAs", links.ElementAt(1).TextContent.Trim());
        Assert.Equal("SEND2", links.ElementAt(2).TextContent.Trim());
        Assert.Equal("Section 251 (outturn)", links.ElementAt(3).TextContent.Trim());
        Assert.Equal("Section 251 (planned expenditure)", links.ElementAt(4).TextContent.Trim());
        Assert.Equal("Statistical neighbours", links.ElementAt(5).TextContent.Trim());
        Assert.Equal("Population", links.ElementAt(6).TextContent.Trim());
        Assert.Equal("Glossary of terms (opens in new tab)", links.ElementAt(7).TextContent.Replace(StringExtensions.WhitespaceRegex(), " ").Trim());
    }
}