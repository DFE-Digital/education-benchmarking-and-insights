using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeedsHistoricData(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplay(bool hasHighNeeds)
    {
        var (page, authority, highNeeds) = await SetupNavigateInitPage(hasHighNeeds);

        AssertPageLayout(page, authority, highNeeds);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityHighNeedsHistoricData(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsHistoricData(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHighNeedsHistoricData(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsHistoricData(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    private async Task<(
        IHtmlDocument page,
        LocalAuthority authority,
        LocalAuthority<HighNeeds>[]? highNeeds)> SetupNavigateInitPage(bool hasHighNeeds = true)
    {
        var authority = Fixture.Build<LocalAuthority>()
            .Create();

        var highNeeds = hasHighNeeds ? Fixture.Build<LocalAuthority<HighNeeds>>().CreateMany().ToArray() : [];

        var page = await Client.SetupEstablishment(authority)
            .SetupHighNeeds(highNeeds, null)
            .SetupInsights()
            .Navigate(Paths.LocalAuthorityHighNeedsHistoricData(authority.Code));

        return (page, authority, highNeeds);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        LocalAuthority authority,
        LocalAuthority<HighNeeds>[]? highNeeds)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsHistoricData(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "High needs historic data - Financial Benchmarking and Insights Tool - GOV.UK", "High needs historic data");

        var warning = page.QuerySelector(".govuk-warning-text");
        if (highNeeds != null && highNeeds.Length != 0)
        {
            Assert.Null(warning);
        }
        else
        {
            Assert.NotNull(warning);
            DocumentAssert.AssertNodeText(warning, "!  Warning\n            There isn't enough information available to view High needs historic data for this local authority.");
        }
    }
}