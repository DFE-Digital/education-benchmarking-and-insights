using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Comparison;

public class WhenViewingComparison(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies, true, false)]
    [InlineData(EstablishmentTypes.Maintained, true, false)]
    [InlineData(EstablishmentTypes.Maintained, true, true)]
    [InlineData(EstablishmentTypes.Academies, false, false)]
    [InlineData(EstablishmentTypes.Maintained, false, false)]
    [InlineData(EstablishmentTypes.Maintained, false, true)]
    public async Task CanDisplay(string financeType, bool ks4ProgressBandingEnabled, bool hasProgressIndicators)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, ks4ProgressBandingEnabled, hasProgressIndicators);

        AssertPageLayout(page, school, ks4ProgressBandingEnabled, hasProgressIndicators);
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
    public async Task CanNavigateToCensusBenchmark(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCensus(school.URN).ToAbsolute());
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
            .Navigate(Paths.SchoolComparison(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolComparison(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool ks4ProgressBandingEnabled = true, bool hasProgressIndicators = true)
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
            .With(x => x.Building, ["building"])
            .Create();

        string[] features = ks4ProgressBandingEnabled ? [] : [FeatureFlags.KS4ProgressBanding];
        var page = await Client
            .SetupDisableFeatureFlags(features)
            .SetupEstablishment(school)
            .SetupInsights()
            .SetupSchoolInsight(characteristics)
            .SetupExpenditure(school)
            .SetupUserData()
            .SetupComparatorSet(school, comparatorSet)
            .Navigate(Paths.SchoolComparison(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, bool ks4ProgressBandingEnabled = true, bool hasProgressIndicators = true)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(school.URN).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Benchmark spending - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark spending");

        var dataSourceElement = page.QuerySelector("main > div > div:nth-child(3) > div");
        Assert.NotNull(dataSourceElement);

        AssertDataSource(dataSourceElement, school, ks4ProgressBandingEnabled, hasProgressIndicators);

        var comparisonComponent = page.GetElementById(ks4ProgressBandingEnabled ? "compare-your-costs-2" : "compare-your-costs");
        Assert.NotNull(comparisonComponent);

        var toolsListSection = page.Body.SelectSingleNode("//main/div/div[6]");
        DocumentAssert.Heading2(toolsListSection, "Benchmarking and planning tools");

        var toolsLinks = toolsListSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(2, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "Curriculum and financial planning",
            Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Benchmark pupil and workforce data", Paths.SchoolCensus(school.URN).ToAbsolute());
    }

    private static void AssertDataSource(IElement dataSourceElement, School school, bool ks4ProgressBandingEnabled, bool hasProgressIndicators)
    {
        switch (ks4ProgressBandingEnabled)
        {
            case true when hasProgressIndicators:
                {
                    var text = GetInsetText(dataSourceElement);
                    if (school.IsPartOfTrust)
                        AssertTrustWithIndicators(text, school);
                    else
                        AssertNonTrustWithIndicators(text, school);
                    break;
                }
            case true:
                {
                    var text = GetInsetText(dataSourceElement);
                    if (school.IsPartOfTrust)
                        AssertTrustNoIndicators(text);
                    else
                        AssertNonTrustNoIndicators(text);
                    break;
                }
            default:
                {
                    var text = GetPlainText(dataSourceElement);
                    if (school.IsPartOfTrust)
                        AssertTrustNoBanding(text);
                    else
                        AssertNonTrustNoBanding(text);
                    break;
                }
        }
    }

    private static IHtmlCollection<IElement> GetInsetText(IElement element)
    {
        var inset = element.QuerySelector(".govuk-inset-text");
        Assert.NotNull(inset);
        var text = inset.QuerySelectorAll("p");
        Assert.NotNull(text);
        return text;
    }

    private static IHtmlCollection<IElement> GetPlainText(IElement element)
    {
        var text = element.QuerySelectorAll("p");
        Assert.NotNull(text);
        return text;
    }

    private static void AssertTrustWithIndicators(IHtmlCollection<IElement> text, School school)
    {
        Assert.Equal(3, text.Length);
        DocumentAssert.TextEqual(text[0], "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
        DocumentAssert.TextEqual(text[1], "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
        DocumentAssert.TextEqual(text[2], "Progress 8 data uses the latest data available from the academic year 2023 to 2024 and is taken from Compare school and college performance in England", true);

        var link = text[2].QuerySelector("a");
        DocumentAssert.Link(link, "Compare school and college performance in England", $"https://www.compare-school-performance.service.gov.uk/school/{school.URN}");
    }

    private static void AssertNonTrustWithIndicators(IHtmlCollection<IElement> text, School school)
    {
        Assert.Equal(2, text.Length);
        DocumentAssert.TextEqual(text[0], "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
        DocumentAssert.TextEqual(text[1], "Progress 8 data uses the latest data available from the academic year 2023 to 2024 and is taken from Compare school and college performance in England");

        var link = text[1].QuerySelector("a");
        DocumentAssert.Link(link, "Compare school and college performance in England", $"https://www.compare-school-performance.service.gov.uk/school/{school.URN}");
    }

    private static void AssertTrustNoIndicators(IHtmlCollection<IElement> text)
    {
        Assert.Equal(2, text.Length);
        DocumentAssert.TextEqual(text[0], "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
        DocumentAssert.TextEqual(text[1], "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
    }

    private static void AssertNonTrustNoIndicators(IHtmlCollection<IElement> text)
    {
        Assert.Equal(1, text.Length);
        DocumentAssert.TextEqual(text[0], "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
    }

    private static void AssertTrustNoBanding(IHtmlCollection<IElement> text)
    {
        Assert.Equal(3, text.Length);
        DocumentAssert.TextEqual(text[0], "Benchmark your spending against similar schools.");
        DocumentAssert.TextEqual(text[1], "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
        DocumentAssert.TextEqual(text[2], "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
    }

    private static void AssertNonTrustNoBanding(IHtmlCollection<IElement> text)
    {
        Assert.Equal(2, text.Length);
        DocumentAssert.TextEqual(text[0], "Benchmark your spending against similar schools.");
        DocumentAssert.TextEqual(text[1], "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
    }
}