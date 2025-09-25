using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Newtonsoft.Json;
using Web.App;
using Web.App.Domain;
using Web.App.Extensions;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingComparison(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplay(bool bfrItSpendFeatureEnabled)
    {
        var (page, trust) = await SetupNavigateInitPage(bfrItSpendFeatureEnabled);

        AssertPageLayout(page, trust, bfrItSpendFeatureEnabled);
    }

    [Fact]
    public async Task CanNavigateToCensusBenchmark()
    {
        var (page, trust) = await SetupNavigateInitPage();

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.TrustCensus(trust.CompanyNumber).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyNumber = "12345678";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustComparison(companyNumber));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparison(companyNumber).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyNumber = "12345678";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustComparison(companyNumber));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparison(companyNumber).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage(bool bfrItSpendFeatureEnabled = true)
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345678")
            .Create();

        var primarySchools = Fixture.Build<TrustSchool>()
            .With(x => x.OverallPhase, OverallPhaseTypes.Primary)
            .CreateMany(9);

        var secondarySchools = Fixture.Build<TrustSchool>()
            .With(x => x.OverallPhase, OverallPhaseTypes.Secondary)
            .CreateMany(11);

        var schools = primarySchools.Concat(secondarySchools).ToArray();

        var page = await Client
            .SetupDisableFeatureFlags(bfrItSpendFeatureEnabled ? [] : [FeatureFlags.TrustItSpendBreakdown])
            .SetupEstablishment(trust, schools)
            .SetupInsights()
            .Navigate(Paths.TrustComparison(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, bool bfrItSpendFeatureEnabled = true)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your trust", Paths.TrustHome(trust.CompanyNumber).ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.TrustComparison(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "View school spending - Financial Benchmarking and Insights Tool - GOV.UK", "View school spending");

        var dataSourceElement = page.QuerySelectorAll("main > div > div:nth-child(3) > div > p");
        Assert.NotNull(dataSourceElement);
        DocumentAssert.TextEqual(dataSourceElement.ElementAt(1), "This trust's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
        DocumentAssert.TextEqual(dataSourceElement.ElementAt(2), "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");

        var component = page.GetElementById("compare-your-costs");
        Assert.NotNull(component);

        var dataId = component.GetAttribute("data-id");
        Assert.NotNull(dataId);
        Assert.Equal(trust.CompanyNumber, dataId);

        var dataType = component.GetAttribute("data-type");
        Assert.NotNull(dataType);
        Assert.Equal(OrganisationTypes.Trust, dataType);

        var dataPhases = component.GetAttribute("data-phases");
        Assert.NotNull(dataPhases);
        string[] expectedPhases = trust.Schools.Length > 1 ? [OverallPhaseTypes.Secondary, OverallPhaseTypes.Primary] : [OverallPhaseTypes.Primary];
        Assert.Equal(expectedPhases.ToJson(Formatting.None), dataPhases);

        // benchmarking tools
        var toolsSection = page.GetElementById("benchmarking-and-planning-tools"); //NB: No RAG therefore section not shown
        DocumentAssert.Heading2(toolsSection, "Benchmarking and planning tools");

        var toolsLinks = toolsSection?.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(bfrItSpendFeatureEnabled ? 5 : 4, toolsLinks?.Count);

        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(0), "View pupil and workforce data", Paths.TrustCensus(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(1), "Curriculum and financial planning", Paths.TrustFinancialPlanning(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(2), "Trust to trust benchmarking", Paths.TrustComparators(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(bfrItSpendFeatureEnabled ? 4 : 3), "Forecast and risk", Paths.TrustForecast(trust.CompanyNumber).ToAbsolute());
        if (bfrItSpendFeatureEnabled)
        {
            DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(3), "Benchmark IT spending", Paths.TrustComparisonItSpend(trust.CompanyNumber).ToAbsolute());
        }
    }
}