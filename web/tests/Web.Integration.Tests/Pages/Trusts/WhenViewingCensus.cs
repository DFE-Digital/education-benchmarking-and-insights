using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App.Domain;
using Xunit;
using Web.App;
using Web.App.Extensions;
using Newtonsoft.Json;

namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingCensus(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust) = await SetupNavigateInitPage();

        AssertPageLayout(page, trust);
    }

    [Fact]
    public async Task CanNavigateToCompareCosts()
    {
        var (page, trust) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.TrustComparison(trust.CompanyNumber).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyNumber = "12345678";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustCensus(companyNumber));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustCensus(companyNumber).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyNumber = "12345678";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustCensus(companyNumber));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustCensus(companyNumber).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage()
    {
        var trust = Fixture.Build<Trust>()
            .Create();

        var primarySchools = Fixture.Build<School>()
            .With(x => x.CompanyNumber, trust.CompanyNumber)
            .With(x => x.OverallPhase, OverallPhaseTypes.Primary)
            .CreateMany(9);

        var secondarySchools = Fixture.Build<School>()
            .With(x => x.CompanyNumber, trust.CompanyNumber)
            .With(x => x.OverallPhase, OverallPhaseTypes.Secondary)
            .CreateMany(11);

        var schools = primarySchools.Concat(secondarySchools).ToArray();

        var page = await Client.SetupEstablishment(trust, schools)
            .SetupInsights()
            .Navigate(Paths.TrustCensus(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your trust", Paths.TrustHome(trust.CompanyNumber).ToAbsolute()),
            ("Benchmark census data", Paths.TrustCensus(trust.CompanyNumber).ToAbsolute()),
        };

        DocumentAssert.AssertPageUrl(page, Paths.TrustCensus(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Benchmark census data - Financial Benchmarking and Insights Tool - GOV.UK", "Benchmark census data");

        var component = page.GetElementById("compare-your-census");
        Assert.NotNull(component);

        var dataId = component.GetAttribute("data-id");
        Assert.NotNull(dataId);
        Assert.Equal(trust.CompanyNumber, dataId);

        var dataType = component.GetAttribute("data-type");
        Assert.NotNull(dataType);
        Assert.Equal(OrganisationTypes.Trust, dataType);

        var dataPhases = component.GetAttribute("data-phases");
        Assert.NotNull(dataPhases);
        string[] expectedPhases = [OverallPhaseTypes.Secondary, OverallPhaseTypes.Primary];
        Assert.Equal(expectedPhases.ToJson(Formatting.None), dataPhases);


        var toolsSection = page.Body.SelectSingleNode("//main/div/div[4]");
        DocumentAssert.Heading2(toolsSection, "Finance tools");

        var toolsLinks = toolsSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(2, toolsLinks.Count);
        DocumentAssert.Link(toolsLinks[0], "Curriculum and financial planning",
            Paths.TrustFinancialPlanning(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Compare your costs", Paths.TrustComparison(trust.CompanyNumber).ToAbsolute());
    }
}
