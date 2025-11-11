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
        if (ks4ProgressBandingEnabled && hasProgressIndicators)
        {
            var insetElement = dataSourceElement.QuerySelector(".govuk-inset-text");
            Assert.NotNull(insetElement);
            var dataSourceText = insetElement.QuerySelectorAll("p");
            Assert.NotNull(dataSourceText);
            if (school.IsPartOfTrust)
            {
                Assert.Equal(3, dataSourceText.Length);
                DocumentAssert.TextEqual(dataSourceText.ElementAt(0), "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
                DocumentAssert.TextEqual(dataSourceText.ElementAt(1), "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
                DocumentAssert.TextEqual(dataSourceText.ElementAt(2), "Progress 8 data uses the latest data available from the academic year 2023 to 2024 and is taken from Compare school and college performance in England");
                
                var compareSchoolsLink = dataSourceText.ElementAt(2).QuerySelector("a");
                DocumentAssert.Link(compareSchoolsLink, "Compare school and college performance in England", $"https://www.compare-school-performance.service.gov.uk/school/{school.URN}");
            }
            else
            {
                Assert.Equal(2, dataSourceText.Length);
                DocumentAssert.TextEqual(dataSourceText.ElementAt(0), "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
                DocumentAssert.TextEqual(dataSourceText.ElementAt(1), "Progress 8 data uses the latest data available from the academic year 2023 to 2024 and is taken from Compare school and college performance in England");
                
                var compareSchoolsLink = dataSourceText.ElementAt(1).QuerySelector("a");
                DocumentAssert.Link(compareSchoolsLink, "Compare school and college performance in England", $"https://www.compare-school-performance.service.gov.uk/school/{school.URN}");
            }
        }
        else if (ks4ProgressBandingEnabled && !hasProgressIndicators)
        {
            var insetElement = dataSourceElement.QuerySelector(".govuk-inset-text");
            Assert.NotNull(insetElement);
            var dataSourceText = insetElement.QuerySelectorAll("p");
            Assert.NotNull(dataSourceText);
            if (school.IsPartOfTrust)
            {
                Assert.Equal(2, dataSourceText.Length);
                DocumentAssert.TextEqual(dataSourceText.ElementAt(0), "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
                DocumentAssert.TextEqual(dataSourceText.ElementAt(1), "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
            }
            else
            {
                Assert.Equal(1, dataSourceText.Length);
                DocumentAssert.TextEqual(dataSourceText.ElementAt(0), "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
            }
        }
        else
        {
            var dataSourceText = dataSourceElement.QuerySelectorAll("p");
            Assert.NotNull(dataSourceText);
            if (school.IsPartOfTrust)
            {
                Assert.Equal(3, dataSourceText.Length);
                DocumentAssert.TextEqual(dataSourceText.ElementAt(0), "Benchmark your spending against similar schools.");
                DocumentAssert.TextEqual(dataSourceText.ElementAt(1), "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
                DocumentAssert.TextEqual(dataSourceText.ElementAt(2), "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
            }
            else
            {
                Assert.Equal(2, dataSourceText.Length);
                DocumentAssert.TextEqual(dataSourceText.ElementAt(0), "Benchmark your spending against similar schools.");
                DocumentAssert.TextEqual(dataSourceText.ElementAt(1), "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
            }
        }
    }
}