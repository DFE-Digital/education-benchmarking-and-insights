using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Domain.Content;
using Web.App.Extensions;
using Web.App.ViewModels;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHome(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(false, false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.Nursery)]
    [InlineData(false, false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough)]
    [InlineData(false, false, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, false, OverallPhaseTypes.Primary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit)]
    [InlineData(true, false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit)]
    [InlineData(true, true, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit)]
    public async Task CanDisplay(bool showBanner, bool hasMissingRag, params string[] phaseTypes)
    {
        var (page, authority, schools, ratings, banner) = await SetupNavigateInitPage(showBanner, true, hasMissingRag, null, phaseTypes);

        AssertPageLayout(page, authority, schools, ratings, banner, true);
    }

    [Theory]
    [InlineData(false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.Nursery)]
    [InlineData(false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough)]
    [InlineData(false, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, OverallPhaseTypes.Primary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(false, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit)]
    [InlineData(true, OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit)]
    public async Task CanDisplayWhenAuthorityHomepageV2Disabled(bool showBanner, params string[] phaseTypes)
    {
        var (page, authority, schools, _, banner) = await SetupNavigateInitPage(showBanner, false, false, null, phaseTypes);

        AssertPageLayout(page, authority, schools, [], banner, false);
    }

    [Fact]
    public async Task CanNavigateToChangeAuthority()
    {
        var (page, _, _, _, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change local authority");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearch.ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToResources()
    {
        var (page, authority, _, _, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Find ways to spend less");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityResources(authority.Code).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToHighNeedsBenchmarking()
    {
        var (page, authority, _, _, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Benchmark high needs");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToHighNeedsHistory()
    {
        var (page, authority, _, _, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "View high needs historical data");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsHistoricData(authority.Code).ToAbsolute());
    }

    // TODO: review for public beta
    //[Fact]
    //public async Task CanNavigateToServiceHelp()
    //{
    //    var (page, _, _) = await SetupNavigateInitPage();

    //    var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Help with this service");
    //    Assert.NotNull(anchor);

    //    page = await Client.Follow(anchor);
    //    DocumentAssert.AssertPageUrl(page, Paths.ServiceHelp.ToAbsolute());
    //}

    //[Fact]
    //public async Task CanNavigateToAskForHelp()
    //{
    //    var (page, _, _) = await SetupNavigateInitPage();

    //    var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Ask for help from a school resource management advisor (SRMA)");
    //    Assert.NotNull(anchor);

    //    page = await Client.Follow(anchor);
    //    DocumentAssert.AssertPageUrl(page, Paths.AskForHelp.ToAbsolute());
    //}

    //[Fact]
    //public async Task CanNavigateToSubmitAnEnquiry()
    //{
    //    var (page, _, _) = await SetupNavigateInitPage();

    //    var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Submit an enquiry");
    //    Assert.NotNull(anchor);

    //    page = await Client.Follow(anchor);
    //    DocumentAssert.AssertPageUrl(page, Paths.SubmitEnquiry.ToAbsolute());
    //}

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayNotFoundForBadIdentifier()
    {
        const string code = "1234";
        var page = await Client
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.NotFound);
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
    public async Task CanSubmitFinancialFilters()
    {
        var (page, authority, _, _, _) = await SetupNavigateInitPage(false, true, false, null, OverallPhaseTypes.Primary);

        var tab = AssertFinancialsTab(page);

        var form = page.QuerySelector("form[role='search']");
        Assert.NotNull(form);

        var phaseInputs = tab.QuerySelectorAll("input[name='f.phase'][type='checkbox']");
        Assert.Equal(9, phaseInputs.Length);

        var nurseryInputs = tab.QuerySelectorAll("input[name='f.nursery'][type='checkbox']");
        Assert.Equal(4, nurseryInputs.Length);

        var specialInputs = tab.QuerySelectorAll("input[name='f.special'][type='checkbox']");
        Assert.Equal(4, specialInputs.Length);

        var sixthFormInputs = tab.QuerySelectorAll("input[name='f.sixth'][type='checkbox']");
        Assert.Equal(4, sixthFormInputs.Length);

        var dimensionInputs = tab.QuerySelectorAll("input[name='f.as'][type='radio']");
        Assert.Equal(4, dimensionInputs.Length);

        var submitButton = tab.QuerySelector("button[data-testid='apply-financial-filters']");
        Assert.NotNull(submitButton);

        page = await Client.SubmitForm(form, submitButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { phaseInputs.ElementAt(0).Attributes["name"]!.Value, phaseInputs.ElementAt(0).Attributes["value"]!.Value },
                { nurseryInputs.ElementAt(0).Attributes["name"]!.Value, nurseryInputs.ElementAt(0).Attributes["value"]!.Value },
                { specialInputs.ElementAt(0).Attributes["name"]!.Value, specialInputs.ElementAt(0).Attributes["value"]!.Value },
                { sixthFormInputs.ElementAt(0).Attributes["name"]!.Value, sixthFormInputs.ElementAt(0).Attributes["value"]!.Value },
                { dimensionInputs.ElementAt(0).Attributes["name"]!.Value, dimensionInputs.ElementAt(0).Attributes["value"]!.Value }
            });
        });

        const string expectedQuery = "?f.phase=0&f.nursery=0&f.special=0&f.sixth=0&f.as=0";
        DocumentAssert.AssertPageUrl(page, $"{Paths.LocalAuthorityHome(authority.Code).ToAbsolute()}{expectedQuery}");
    }

    [Fact]
    public async Task CanSetFinancialFilters()
    {
        const string queryString = "?f.phase=0&f.nursery=0&f.special=0&f.sixth=0&f.as=0";
        var (page, authority, _, _, _) = await SetupNavigateInitPage(false, true, false, queryString, OverallPhaseTypes.Primary);

        var tab = AssertFinancialsTab(page);
        var phaseInputs = tab.QuerySelectorAll("input[name='f.phase'][type='checkbox']");
        var nurseryInputs = tab.QuerySelectorAll("input[name='f.nursery'][type='checkbox']");
        var specialInputs = tab.QuerySelectorAll("input[name='f.special'][type='checkbox']");
        var sixthFormInputs = tab.QuerySelectorAll("input[name='f.sixth'][type='checkbox']");
        var dimensionInputs = tab.QuerySelectorAll("input[name='f.as'][type='radio']");

        for (var i = 0; i < phaseInputs.Length; i++)
        {
            var input = phaseInputs[i];
            Assert.Equal(i == 0, input.HasAttribute("checked"));
        }

        for (var i = 0; i < nurseryInputs.Length; i++)
        {
            var input = nurseryInputs[i];
            Assert.Equal(i == 0, input.HasAttribute("checked"));
        }

        for (var i = 0; i < specialInputs.Length; i++)
        {
            var input = specialInputs[i];
            Assert.Equal(i == 0, input.HasAttribute("checked"));
        }

        for (var i = 0; i < sixthFormInputs.Length; i++)
        {
            var input = sixthFormInputs[i];
            Assert.Equal(i == 0, input.HasAttribute("checked"));
        }

        for (var i = 0; i < dimensionInputs.Length; i++)
        {
            var input = dimensionInputs[i];
            Assert.Equal(i == 0, input.HasAttribute("checked"));
        }
    }

    [Theory]
    [InlineData(null, true, "?f.filter=hide&f.as=0")]
    [InlineData("?f.filter=hide", false, "?f.filter=show&f.as=0")]
    public async Task CanToggleFinancialFilters(string? queryString, bool expectedVisible, string expectedQuery)
    {
        var (page, authority, _, _, _) = await SetupNavigateInitPage(false, true, false, queryString, OverallPhaseTypes.Primary);

        var tab = AssertFinancialsTab(page);

        var form = page.QuerySelector("form[role='search']");
        Assert.NotNull(form);

        var toggleButton = tab.QuerySelector("button[data-testid='toggle-financial-filters']");
        Assert.NotNull(toggleButton);
        Assert.Equal(expectedVisible ? "Hide filters" : "Show filters", toggleButton.TextContent.Trim());

        page = await Client.SubmitForm(form, toggleButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { toggleButton.Attributes["name"]!.Value, toggleButton.Attributes["value"]!.Value },
            });
        });

        DocumentAssert.AssertPageUrl(page, $"{Paths.LocalAuthorityHome(authority.Code).ToAbsolute()}{expectedQuery}");
    }

    private async Task<(IHtmlDocument page, LocalAuthority authority, LocalAuthoritySchool[] schools, RagRatingSummary[] ratings, Banner? banner)> SetupNavigateInitPage(
        bool showBanner = false,
        bool localAuthorityHomepageV2Enabled = false,
        bool hasMissingRag = false,
        string? queryString = null,
        params string[] phaseTypes)
    {
        var authority = Fixture.Build<LocalAuthority>()
            .With(a => a.Code, "123")
            .Create();

        Assert.NotNull(authority.Name);
        var schools = phaseTypes.SelectMany(GenerateSchools).ToArray();
        authority.Schools = schools;

        var banner = showBanner
            ? Fixture.Create<Banner>()
            : null;

        var statisticalNeighbours = Fixture.Build<LocalAuthorityStatisticalNeighbour>()
            .CreateMany()
            .ToArray();

        var authorityWithNeighbours = Fixture.Build<LocalAuthorityStatisticalNeighbours>()
            .With(a => a.Code, authority.Code)
            .With(a => a.Name, authority.Name)
            .Create();
        authorityWithNeighbours.StatisticalNeighbours = statisticalNeighbours;

        var random = new Random();
        var ratings = schools
            .Select(s => Fixture
                .Build<RagRatingSummary>()
                .With(r => r.URN, s.URN)
                .With(r => r.SchoolName, s.SchoolName)
                .With(r => r.OverallPhase, s.OverallPhase)
                .With(r => r.RedCount, random.Next(0, hasMissingRag ? 0 : 8))
                .With(r => r.AmberCount, random.Next(0, hasMissingRag ? 0 : 8))
                .With(r => r.GreenCount, random.Next(0, hasMissingRag ? 0 : 8))
                .Create())
            .ToArray();

        var page = await Client
            .SetupDisableFeatureFlags(localAuthorityHomepageV2Enabled ? [] : [FeatureFlags.LocalAuthorityHomepageV2])
            .SetupEstablishment(authorityWithNeighbours, [authority])
            .SetupInsights()
            .SetupLocalAuthoritiesComparators(authority.Code!, [])
            .SetupBanner(banner)
            .SetupMetricRagRatingSummary(localAuthorityHomepageV2Enabled ? ratings : [])
            .Navigate($"{Paths.LocalAuthorityHome(authority.Code)}{queryString}");

        return (page, authority, schools, ratings, banner);
    }

    private LocalAuthoritySchool[] GenerateSchools(string phaseType)
    {
        return Fixture.Build<LocalAuthoritySchool>()
            .With(x => x.OverallPhase, phaseType)
            .CreateMany(10)
            .ToArray();
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        LocalAuthority authority,
        LocalAuthoritySchool[] schools,
        RagRatingSummary[] ratings,
        Banner? banner,
        bool localAuthorityHomepageV2Enabled)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Your local authority - Financial Benchmarking and Insights Tool - GOV.UK", authority.Name);

        var dataSourceElement = page.QuerySelector($"main > div > div:nth-child({(banner == null ? "2" : "3")}) > div > p");
        Assert.NotNull(dataSourceElement);

        DocumentAssert.TextEqual(dataSourceElement, "This data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");

        var accordion = page.QuerySelector("#accordion-schools");
        if (localAuthorityHomepageV2Enabled)
        {
            Assert.Null(accordion);
            AssertFinancialsTab(page);
            AssertPrioritySchoolsSection(page, ratings);
        }
        else
        {
            AssertAccordionSection(accordion, schools);
        }

        DocumentAssert.Banner(page, banner);
        AssertToolsSection(page);
        AssertHighNeedsSection(page);
        AssertResourcesSection(page);
    }

    private static void AssertAccordionSection(IElement? accordion, LocalAuthoritySchool[] schools)
    {
        Assert.NotNull(accordion);
        var accordionSections = accordion.QuerySelectorAll(".govuk-accordion__section");
        Assert.NotEmpty(accordionSections);

        foreach (var section in accordionSections)
        {
            var headingElement = section.QuerySelector(".govuk-accordion__section-heading");
            Assert.NotNull(headingElement);
            var headingText = headingElement.TextContent.Trim();

            var contentElement = section.QuerySelector(".govuk-accordion__section-content");
            Assert.NotNull(contentElement);

            AssertAccordionContent(contentElement, schools, headingText);
        }
    }

    private static void AssertAccordionContent(IElement element, LocalAuthoritySchool[] schools, string expectedPhaseType)
    {
        var expectedSchools = schools.Where(x => x.OverallPhase == expectedPhaseType);

        var schoolList = element.QuerySelector(".govuk-list");
        Assert.NotNull(schoolList);
        Assert.Equal(expectedSchools.Count(), schoolList.Children.Length);

        foreach (var schoolElement in schoolList.Children)
        {
            var schoolName = schoolElement.QuerySelector("a")?.TextContent;
            var school = schools.FirstOrDefault(s => s.SchoolName == schoolName);
            Assert.NotNull(school);
            Assert.Equal(school.OverallPhase, expectedPhaseType);
        }
    }

    private static void AssertToolsSection(IHtmlDocument page)
    {
        var links = page.QuerySelectorAll("#finance-tools .app-links > li a");
        Assert.Equal(2, links.Length);

        Assert.Equal("View school spending", links.ElementAt(0).TextContent.Trim());
        Assert.Equal("View pupil and workforce data", links.ElementAt(1).TextContent.Trim());
    }

    private static void AssertHighNeedsSection(IHtmlDocument page)
    {
        var links = page.QuerySelectorAll("#high-needs .app-links > li a");
        Assert.Equal(2, links.Length);

        Assert.Equal("Benchmark high needs", links.ElementAt(0).TextContent.Trim());
        Assert.Equal("View high needs historical data", links.ElementAt(1).TextContent.Trim());
    }

    private static void AssertResourcesSection(IHtmlDocument page)
    {
        var links = page.QuerySelectorAll("#establishment-resources .app-links > li a");
        Assert.Equal(2, links.Length);

        Assert.Equal("Find ways to spend less", links.ElementAt(0).TextContent.Trim());
        Assert.Equal("Data sources and interpretation", links.ElementAt(1).TextContent.Trim());
    }

    private static void AssertPrioritySchoolsSection(IHtmlDocument page, RagRatingSummary[] ratings)
    {
        var phases = ratings.Select(x => x.OverallPhase).Distinct().ToArray();
        foreach (var overallPhase in phases)
        {
            var heading = overallPhase switch
            {
                OverallPhaseTypes.Primary => "Primary schools",
                OverallPhaseTypes.Secondary => "Secondary schools",
                OverallPhaseTypes.AlternativeProvision => "Alternative provision",
                OverallPhaseTypes.PostSixteen => "Post 16",
                OverallPhaseTypes.UniversityTechnicalCollege => "University technical colleges",
                _ => overallPhase
            };

            var section = page.QuerySelector($"#school-rag-{heading?.ToSlug()}");
            Assert.NotNull(section);

            var expectedRows = ratings
                .GroupBy(x => x.OverallPhase)
                .Select(x => (
                    OverallPhase: x.Key,
                    Schools: x
                        .Select(s => new RagSchoolViewModel(
                            s.URN,
                            s.SchoolName,
                            s.RedCount ?? 0,
                            s.AmberCount ?? 0,
                            s.GreenCount ?? 0
                        )).OrderByDescending(o => o.RedRatio)
                        .ThenByDescending(o => o.AmberRatio)
                        .ThenBy(o => o.Name)
                        .Take(5)))
                .Where(x => x.OverallPhase == overallPhase)
                .SelectMany(x => x.Schools)
                .ToArray();

            var actualRows = section.QuerySelectorAll(".govuk-grid-row");
            Assert.Equal(expectedRows.Length + 1, actualRows.Length);

            for (var i = 1; i < actualRows.Length; i++)
            {
                var actual = actualRows.ElementAt(i);
                var expected = expectedRows.ElementAt(i - 1);

                var urn = actual.Attributes["data-key"]?.Value;
                Assert.Equal(expected.Urn, urn);

                var ragStack = actual.QuerySelector(".rag-stack");
                if (expected.Total > 0)
                {
                    Assert.NotNull(ragStack);
                    Assert.Equal($"{expected.Red} high, {expected.Amber} medium and {expected.Green} low priorities for {expected.Name}", ragStack.TextContent.Trim());
                }
                else
                {
                    Assert.Null(ragStack);
                    Assert.Equal($"{expected.Name} Status unavailable", actual.TextContent.Replace(StringExtensions.WhitespaceRegex(), " ").Trim());
                }
            }
        }
    }

    private static IElement AssertFinancialsTab(IHtmlDocument page)
    {
        var tab = page.QuerySelector(".govuk-tabs > #financial");
        Assert.NotNull(tab);
        return tab;
    }
}