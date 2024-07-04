using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingResources(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        /*
        See decision log: temp remove navigation to be review post private beta
        var (page, authority) = await SetupNavigateInitPage();

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(authority.Code).ToAbsolute());*/
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityResources(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityResources(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityResources(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityResources(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, LocalAuthority authority)> SetupNavigateInitPage()
    {
        var authority = Fixture.Build<LocalAuthority>()
            .Create();

        var page = await Client.SetupEstablishment(authority)
            .SetupInsights()
            .Navigate(Paths.LocalAuthorityResources(authority.Code));

        return (page, authority);
    }

    private static void AssertPageLayout(IHtmlDocument page, LocalAuthority authority)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityResources(authority.Code).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.LocalAuthorityHome(authority.Code).ToAbsolute());

        DocumentAssert.TitleAndH1(page, "Find ways to spend less - Financial Benchmarking and Insights Tool - GOV.UK", "Find ways to spend less");
    }
}