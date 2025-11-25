using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
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
    [InlineData(EstablishmentTypes.Academies, true, true)]
    [InlineData(EstablishmentTypes.Academies, false, false)]
    [InlineData(EstablishmentTypes.Maintained, false, false)]
    [InlineData(EstablishmentTypes.Maintained, false, true)]
    [InlineData(EstablishmentTypes.Academies, false, true)]
    public async Task CanDisplay(string financeType, bool ks4ProgressBandingEnabled, bool hasProgressIndicators)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, ks4ProgressBandingEnabled, hasProgressIndicators);

        AssertPageLayout(page, school, ks4ProgressBandingEnabled, hasProgressIndicators);
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, false, false)]
    [InlineData(true, true, false)]
    [InlineData(false, true, false)]
    [InlineData(true, false, true)]
    [InlineData(false, false, true)]
    public async Task CanDisplayCorrectComparatorSetDetails(
        bool ks4ProgressBandingEnabled,
        bool withCustomUserData,
        bool withUserDefinedUserData)
    {
        var (page, school) = await SetupNavigateInitPage(
            EstablishmentTypes.Maintained,
            ks4ProgressBandingEnabled,
            true,
            withCustomUserData,
            withUserDefinedUserData);

        AssertPageLayout(
            page,
            school,
            ks4ProgressBandingEnabled,
            true,
            withCustomUserData,
            withUserDefinedUserData);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplayCorrectComparatorSetDetailsWhenMissingComparatorSet(bool ks4ProgressBandingEnabled)
    {
        var (page, school) = await SetupNavigateInitPage(
            EstablishmentTypes.Maintained,
            ks4ProgressBandingEnabled,
            withEmptyComparatorSet: true);

        var comparatorSetDetailsElement = page.QuerySelector("[data-testid=comparator-set-details]"); ;
        Assert.NotNull(comparatorSetDetailsElement);

        AssertMissingComparatorSetCreatedComparatorSetDetailsContent(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled);
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

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(
        string financeType,
        bool ks4ProgressBandingEnabled = true,
        bool hasProgressIndicators = true,
        bool withCustomUserData = false,
        bool withUserDefinedUserData = false,
        bool withEmptyComparatorSet = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .With(x => x.TrustCompanyNumber, financeType == EstablishmentTypes.Academies ? "87654321" : string.Empty)
            .Create();

        var characteristics = Fixture.Build<SchoolCharacteristic>()
            .With(x => x.URN, "123456")
            .With(x => x.KS4ProgressBanding, hasProgressIndicators ? "Well above average" : "Below average")
            .CreateMany()
            .ToArray();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Building, ["building"])
            .Create();

        var emptyComparatorSet = Fixture.Build<SchoolComparatorSet>()
            .Without(x => x.Building)
            .Without(x => x.Pupil)
            .Create();

        var customUserData = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = "123",
                Status = "complete"
            }
        };

        var userDefinedSetUserData = new[]
        {
            new UserData
            {
                Type = "comparator-set",
                Id = "456"
            }
        };

        string[] features = ks4ProgressBandingEnabled ? [] : [FeatureFlags.KS4ProgressBanding];
        var page = await Client
            .SetupDisableFeatureFlags(features)
            .SetupEstablishment(school)
            .SetupInsights()
            .SetupSchoolInsight(characteristics)
            .SetupExpenditure(school)
            .SetupUserData(withUserDefinedUserData
                ? userDefinedSetUserData
                : withCustomUserData
                    ? customUserData : null)
            .SetupComparatorSet(school, withEmptyComparatorSet ? emptyComparatorSet : comparatorSet)
            .Navigate(Paths.SchoolComparison(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        bool ks4ProgressBandingEnabled = true,
        bool hasProgressIndicators = true,
        bool withCustomUserData = false,
        bool withUserDefinedUserData = false)
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

        var dataSourceElement = page.QuerySelector("div[data-test-id='data-source-wrapper']");
        Assert.NotNull(dataSourceElement);
        AssertDataSource(dataSourceElement, school, ks4ProgressBandingEnabled, hasProgressIndicators);

        var comparatorSetDetailsElement = page.QuerySelector("[data-testid=comparator-set-details]");
        Assert.NotNull(comparatorSetDetailsElement);
        AssertComparatorSetDetails(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled, withCustomUserData, withUserDefinedUserData, false);

        var comparisonComponent = page.GetElementById(ks4ProgressBandingEnabled ? "compare-your-costs-2" : "compare-your-costs");
        Assert.NotNull(comparisonComponent);

        var toolsListSection = page.GetElementById("benchmarking-and-planning-tools");
        Assert.NotNull(toolsListSection);
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
                    if (school.IsPartOfTrust)
                        SchoolDocumentAssert.AssertAcademyWithIndicators(dataSourceElement, school.URN!);
                    else
                        SchoolDocumentAssert.AssertMaintainedSchoolWithIndicators(dataSourceElement, school.URN!);
                    break;
                }
            case true:
                {
                    if (school.IsPartOfTrust)
                        SchoolDocumentAssert.AssertAcademyNoIndicators(dataSourceElement);
                    else
                        SchoolDocumentAssert.AssertMaintainedSchoolNoIndicators(dataSourceElement);
                    break;
                }
            default:
                {
                    const string additionalText = "Benchmark your spending against similar schools.";
                    if (school.IsPartOfTrust)
                        SchoolDocumentAssert.AssertAcademyNoBanding(dataSourceElement, additionalText);
                    else
                        SchoolDocumentAssert.AssertMaintainedSchoolNoBanding(dataSourceElement, additionalText);
                    break;
                }
        }
    }

    private static void AssertComparatorSetDetails(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled, bool withCustomUserData, bool withUserDefinedUserData, bool emptyComparatorSet)
    {
        if (withCustomUserData)
        {
            AssertCustomDataCreatedComparatorSetDetailsContent(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled);
        }
        else if (withUserDefinedUserData)
        {
            AssertUserDefinedSetCreatedComparatorSetDetailsContent(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled);
        }
        else
        {
            AssertComparatorSetDetailsDefaultContent(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled);
        }
    }

    private static void AssertComparatorSetDetailsDefaultContent(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled)
    {
        if (ks4ProgressBandingEnabled)
        {
            var introParagraph = comparatorSetDetailsElement.QuerySelector("p.govuk-body");
            Assert.NotNull(introParagraph);
            Assert.Equal("You can:", introParagraph.TextContent.Trim());

            var list = comparatorSetDetailsElement.QuerySelector("ul");
            Assert.NotNull(list);
            var listItems = list.QuerySelectorAll("li");
            Assert.Equal(3, listItems.Length);

            DocumentAssert.TextEqual(listItems[0], "view the 2 sets of similar schools we've chosen to benchmark this school's spending against", true);
            DocumentAssert.Link(listItems[0].QuerySelector("a"), "view the 2 sets of similar schools we've chosen", Paths.SchoolComparators(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[1], "create or save your own set of schools to benchmark against", true);
            DocumentAssert.Link(listItems[1].QuerySelector("a"), "create or save your own set of schools", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[2], "change the data for this school", true);
            DocumentAssert.Link(listItems[2].QuerySelector("a"), "change the data", Paths.SchoolCustomData(school.URN).ToAbsolute());
        }
        else
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.Equal(3, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "We've chosen 2 sets of similar schools to benchmark this school's spending against.", true);
            DocumentAssert.Link(paragraphs[0].QuerySelector("a"), "We've chosen 2 sets of similar schools", Paths.SchoolComparators(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(paragraphs[1], "Create or save your own set of schools to benchmark against");
            DocumentAssert.Link(paragraphs[1].QuerySelector("a"), "Create or save your own set of schools to benchmark against", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(paragraphs[2], "Change the data for this school");
            DocumentAssert.Link(paragraphs[2].QuerySelector("a"), "Change the data for this school", Paths.SchoolCustomData(school.URN).ToAbsolute());
        }
    }

    private static void AssertMissingComparatorSetCreatedComparatorSetDetailsContent(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled)
    {
        if (ks4ProgressBandingEnabled)
        {
            var introParagraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.Equal(2, introParagraphs.Length);
            DocumentAssert.TextEqual(introParagraphs[0], "There is not enough information available to create a set of similar schools.", true);
            DocumentAssert.TextEqual(introParagraphs[1], "You can:");

            var list = comparatorSetDetailsElement.QuerySelector("ul.govuk-list.govuk-list--bullet");
            Assert.NotNull(list);
            var listItems = list.QuerySelectorAll("li");
            Assert.Equal(2, listItems.Length);

            DocumentAssert.TextEqual(listItems[0], "create or save your own set of schools to benchmark against", true);
            DocumentAssert.Link(listItems[0].QuerySelector("a"), "create or save your own set of schools", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[1], "change the data for this school", true);
            DocumentAssert.Link(listItems[1].QuerySelector("a"), "change the data", Paths.SchoolCustomData(school.URN).ToAbsolute());
        }
        else
        {
            var warning = comparatorSetDetailsElement.QuerySelector("div.govuk-warning-text strong.govuk-warning-text__text");
            Assert.NotNull(warning);
            DocumentAssert.TextEqual(warning, "Warning There isn't enough information available to create a set of similar schools.", true);

            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(2, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "Create or save your own set of schools to benchmark against");
            DocumentAssert.Link(paragraphs[0].QuerySelector("a"), "Create or save your own set of schools to benchmark against", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(paragraphs[1], "Change the data for this school");
            DocumentAssert.Link(paragraphs[1].QuerySelector("a"), "Change the data for this school", Paths.SchoolCustomData(school.URN).ToAbsolute());
        }
    }

    private static void AssertUserDefinedSetCreatedComparatorSetDetailsContent(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled)
    {
        if (ks4ProgressBandingEnabled)
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll(":scope > p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(2, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "You are now comparing with your chosen schools.");
            DocumentAssert.TextEqual(paragraphs[1], "Change your similar schools.");
            DocumentAssert.Link(paragraphs[1].QuerySelector("a"), "Change your similar schools.", Paths.SchoolComparatorsUserDefined(school.URN).ToAbsolute());
        }
        else
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(2, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "You are now comparing with your chosen schools.");
            DocumentAssert.TextEqual(paragraphs[1], "Change your similar schools");
            DocumentAssert.Link(paragraphs[1].QuerySelector("a"), "Change your similar schools", Paths.SchoolComparatorsUserDefined(school.URN).ToAbsolute());
        }
    }

    private static void AssertCustomDataCreatedComparatorSetDetailsContent(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled)
    {
        if (ks4ProgressBandingEnabled)
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll(":scope > p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(2, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "The information displayed is the originally reported data, not the customised data that was provided.");
            DocumentAssert.TextEqual(paragraphs[1], "You can:");

            var list = comparatorSetDetailsElement.QuerySelector("ul.govuk-list.govuk-list--bullet");
            Assert.NotNull(list);
            var listItems = list.QuerySelectorAll("li");
            Assert.Equal(2, listItems.Length);

            DocumentAssert.TextEqual(listItems[0], "view the 2 sets of similar schools we've chosen to benchmark this school's spending against", true);
            DocumentAssert.Link(listItems[0].QuerySelector("a"), "view the 2 sets of similar schools we've chosen", Paths.SchoolComparators(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[1], "view custom data set");
            DocumentAssert.Link(listItems[1].QuerySelector("a"), "view custom data set", Paths.SchoolCustomisedData(school.URN).ToAbsolute());
        }
        else
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(3, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "We've chosen 2 sets of similar schools to benchmark this school's spending against.", true);
            DocumentAssert.Link(paragraphs[0].QuerySelector("a"), "We've chosen 2 sets of similar schools", Paths.SchoolComparators(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(paragraphs[1], "The information displayed is the originally reported data, not the customised data that was provided.");

            DocumentAssert.TextEqual(paragraphs[2], "View custom data set");
            DocumentAssert.Link(paragraphs[2].QuerySelector("a"), "View custom data set", Paths.SchoolCustomisedData(school.URN).ToAbsolute());
        }
    }
}