using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Census;

public class WhenViewingCensus : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly App.Domain.Census _census;

    public WhenViewingCensus(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _census = Fixture.Build<App.Domain.Census>()
            .Create();
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, false, false)]
    [InlineData(EstablishmentTypes.Academies, false, true)]
    [InlineData(EstablishmentTypes.Academies, true, false)]
    [InlineData(EstablishmentTypes.Academies, true, true)]
    [InlineData(EstablishmentTypes.Maintained, false, false)]
    [InlineData(EstablishmentTypes.Maintained, true, true)]
    public async Task CanDisplay(string financeType, bool ks4ProgressBandingEnabled, bool hasProgressIndicators)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, ks4ProgressBandingEnabled, hasProgressIndicators);

        AssertPageLayout(page, school, ks4ProgressBandingEnabled);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCurriculumPlanning(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCompareCosts(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCustomData(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelector("#custom-data-link");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCustomData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolCensus(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCensus(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolCensus(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCensus(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(
        string financeType,
        bool ks4ProgressBandingEnabled = true,
        bool hasProgressIndicators = true)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .Create();

        var characteristics = Fixture.Build<SchoolCharacteristic>()
            .With(x => x.URN, "123456")
            .With(x => x.KS4ProgressBanding, hasProgressIndicators ? "Well above average" : "Below average")
            .CreateMany();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Pupil, ["pupil"])
            .Create();

        string[] features = ks4ProgressBandingEnabled ? [] : [FeatureFlags.KS4ProgressBanding];
        var page = await Client
            .SetupDisableFeatureFlags(features)
            .SetupEstablishment(school)
            .SetupInsights()
            .SetupSchoolInsight(characteristics)
            .SetupExpenditure(school)
            .SetupUserData()
            .SetupCensus(school, _census)
            .SetupComparatorSet(school, comparatorSet)
            .Navigate(Paths.SchoolCensus(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        bool ks4ProgressBandingEnabled = true)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCensus(school.URN).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Benchmark pupil and workforce data - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark pupil and workforce data");

        var component = page.GetElementById(ks4ProgressBandingEnabled ? "compare-your-census-2" : "compare-your-census");
        Assert.NotNull(component);

        var toolsSection = page.GetElementById("benchmarking-and-planning-tools");
        DocumentAssert.Heading2(toolsSection, "Benchmarking and planning tools");

        var toolsLinks = toolsSection?.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(2, toolsLinks?.Count);

        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(0), "Curriculum and financial planning",
            Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(1), "Benchmark spending", Paths.SchoolComparison(school.URN).ToAbsolute());
    }
}