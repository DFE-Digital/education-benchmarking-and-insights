using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeedsStartBenchmarking(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    private async Task<(IHtmlDocument page, LocalAuthorityStatisticalNeighbours authority)> SetupNavigateInitPage()
    {
        var authority = Fixture.Build<LocalAuthorityStatisticalNeighbours>()
            .Create();

        var statisticalNeighbours = Fixture.Build<LocalAuthorityStatisticalNeighbour>()
            .CreateMany()
            .ToArray();

        authority.StatisticalNeighbours = statisticalNeighbours;

        var page = await Client.SetupEstablishment(authority)
            .SetupInsights()
            .Navigate(Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code));

        return (page, authority);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        LocalAuthorityStatisticalNeighbours authority)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Choose local authorities to benchmark against - Financial Benchmarking and Insights Tool - GOV.UK", "Choose local authorities to benchmark against");

        var orderedList = page.QuerySelector(".govuk-inset-text > ol.govuk-list--number");
        Assert.NotNull(orderedList);
        var listItems = orderedList.QuerySelectorAll("li").Select(q => q.TextContent).ToArray();
        var expectedListItems = authority.StatisticalNeighbours?
            .OrderBy(n => n.Order)
            .ThenBy(n => n.Name)
            .Select(n => n.Name)
            .ToArray();
        Assert.Equal(expectedListItems, listItems);
    }
}