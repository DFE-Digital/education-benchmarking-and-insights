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
        bool hasProgressIndicators = true,
        bool withCustomUserData = false,
        bool withUserDefinedUserData = false,
        bool withEmptyComparatorSet = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
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
            .SetupCensus(school, _census)
            .SetupComparatorSet(school, withEmptyComparatorSet ? emptyComparatorSet : comparatorSet)
            .Navigate(Paths.SchoolCensus(school.URN));

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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCensus(school.URN).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Benchmark pupil and workforce data - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark pupil and workforce data");

        var dataSourceElement = page.QuerySelector("div[data-test-id='data-source-wrapper']");
        Assert.NotNull(dataSourceElement);
        AssertDataSource(dataSourceElement, school, ks4ProgressBandingEnabled, hasProgressIndicators);

        var comparatorSetDetailsElement = page.QuerySelector("[data-testid=comparator-set-details]");
        Assert.NotNull(comparatorSetDetailsElement);
        AssertComparatorSetDetails(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled, withCustomUserData, withUserDefinedUserData, false);

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

    private static void AssertDataSource(IElement dataSourceElement, School school, bool ks4ProgressBandingEnabled, bool hasProgressIndicators)
    {
        switch (ks4ProgressBandingEnabled)
        {
            case true when hasProgressIndicators:
                {
                    var text = GetInsetText(dataSourceElement);
                    AssertWithIndicators(text, school);
                    break;
                }
            case true:
                {
                    var text = GetInsetText(dataSourceElement);
                    AssertNoIndicators(text);
                    break;
                }
            default:
                {
                    var text = GetPlainText(dataSourceElement);
                    AssertNoBanding(text);
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

            DocumentAssert.TextEqual(listItems[0], "view the set of similar schools we've chosen to benchmark this school's pupil and workforce data against", true);
            DocumentAssert.Link(listItems[0].QuerySelector("a"), "view the set of similar schools we've chosen", Paths.SchoolComparatorsWorkforce(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[1], "create or save your own set of schools to benchmark against", true);
            DocumentAssert.Link(listItems[1].QuerySelector("a"), "create or save your own set of schools", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[2], "change the data for this school", true);
            DocumentAssert.Link(listItems[2].QuerySelector("a"), "change the data", Paths.SchoolCustomData(school.URN).ToAbsolute());
        }
        else
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.Equal(3, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "We've chosen this set of similar schools to benchmark this school's pupil and workforce data against.", true);
            DocumentAssert.Link(paragraphs[0].QuerySelector("a"), "We've chosen this set of similar schools", Paths.SchoolComparatorsWorkforce(school.URN).ToAbsolute());

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
            var warning = comparatorSetDetailsElement.QuerySelector("div.govuk-warning-text strong.govuk-warning-text__text");
            Assert.NotNull(warning);
            DocumentAssert.TextEqual(warning, "Warning There isn't enough information available to create a set of similar schools.", true);

            var introParagraph = comparatorSetDetailsElement.QuerySelector("p.govuk-body");
            Assert.NotNull(introParagraph);
            DocumentAssert.TextEqual(introParagraph, "You can:");

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

            DocumentAssert.TextEqual(listItems[0], "view the set of similar schools we've chosen to benchmark this school's pupil and workforce data against", true);
            DocumentAssert.Link(listItems[0].QuerySelector("a"), "view the set of similar schools we've chosen", Paths.SchoolComparatorsWorkforce(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[1], "view custom data set");
            DocumentAssert.Link(listItems[1].QuerySelector("a"), "view custom data set", Paths.SchoolCustomisedData(school.URN).ToAbsolute());
        }
        else
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(3, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "We've chosen this set of similar schools to benchmark this school's pupil and workforce data against.", true);
            DocumentAssert.Link(paragraphs[0].QuerySelector("a"), "We've chosen this set of similar schools", Paths.SchoolComparatorsWorkforce(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(paragraphs[1], "The information displayed is the originally reported data, not the customised data that was provided.");

            DocumentAssert.TextEqual(paragraphs[2], "View custom data set");
            DocumentAssert.Link(paragraphs[2].QuerySelector("a"), "View custom data set", Paths.SchoolCustomisedData(school.URN).ToAbsolute());

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

    private static void AssertWithIndicators(IHtmlCollection<IElement> text, School school)
    {
        Assert.Equal(2, text.Length);
        DocumentAssert.TextEqual(text[0], "Workforce data is taken from the workforce census. Pupil data is taken from the school census data set in January.");
        DocumentAssert.TextEqual(text[1], "Progress 8 data uses the latest data available from the academic year 2023 to 2024 and is taken from Compare school and college performance in England", true);

        var link = text[1].QuerySelector("a");
        DocumentAssert.Link(link, "Compare school and college performance in England", $"https://www.compare-school-performance.service.gov.uk/school/{school.URN}");
    }

    private static void AssertNoIndicators(IHtmlCollection<IElement> text)
    {
        Assert.Equal(1, text.Length);
        DocumentAssert.TextEqual(text[0], "Workforce data is taken from the workforce census. Pupil data is taken from the school census data set in January.");
    }

    private static void AssertNoBanding(IHtmlCollection<IElement> text)
    {
        Assert.Equal(3, text.Length);
        DocumentAssert.TextEqual(text[0], "Benchmark your pupil and workforce data against similar schools.");
        DocumentAssert.TextEqual(text[1], "Workforce data is taken from the workforce census.");
        DocumentAssert.TextEqual(text[2], "Pupil data is taken from the school census data set in January.");
    }
}