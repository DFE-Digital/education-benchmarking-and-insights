using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHome(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.Nursery)]
    [InlineData(OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough)]
    [InlineData(OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(OverallPhaseTypes.Primary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.AllThrough, OverallPhaseTypes.Nursery)]
    [InlineData(OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Special, OverallPhaseTypes.PupilReferralUnit)]
    public async Task CanDisplay(params string[] phaseTypes)
    {
        var (page, authority, schools) = await SetupNavigateInitPage(phaseTypes);

        AssertPageLayout(page, authority, schools);
    }

    [Fact]
    public async Task CanNavigateToChangeAuthority()
    {
        var (page, _, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change local authority");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, $"{Paths.FindOrganisation.ToAbsolute()}?method=local-authority");
    }

    [Fact]
    public async Task CanNavigateToResources()
    {
        var (page, authority, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Find ways to spend less");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityResources(authority.Code).ToAbsolute());
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
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, LocalAuthority authority, School[] schools)> SetupNavigateInitPage(params string[] phaseTypes)
    {
        var authority = Fixture.Build<LocalAuthority>()
            .Create();

        Assert.NotNull(authority.Name);
        var schools = phaseTypes.SelectMany(phaseType => GenerateSchools(authority.Name, phaseType)).ToArray();

        var page = await Client.SetupEstablishment(authority, schools)
            .SetupInsights()
            .Navigate(Paths.LocalAuthorityHome(authority.Code));

        return (page, authority, schools);
    }

    private School[] GenerateSchools(string localAuthorityName, string phaseType)
    {
        return Fixture.Build<School>()
            .With(x => x.LAName, localAuthorityName)
            .With(x => x.OverallPhase, phaseType)
            .CreateMany(10)
            .ToArray();
    }

    private static void AssertPageLayout(IHtmlDocument page, LocalAuthority authority, School[] schools)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Your local authority - Financial Benchmarking and Insights Tool - GOV.UK", authority.Name);

        var dataSourceElement = page.QuerySelector("main > div > div:nth-child(2) > div > p");
        Assert.NotNull(dataSourceElement);

        DocumentAssert.TextEqual(dataSourceElement, "This data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");

        var accordion = page.QuerySelector("#accordion-schools");
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

            AssertAccordionContent(contentElement, authority, schools, headingText);
        }
    }

    private static void AssertAccordionContent(IElement element, LocalAuthority authority, School[] schools, string expectedPhaseType)
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
            Assert.Equal(authority.Name, school.LAName);
            Assert.Equal(school.OverallPhase, expectedPhaseType);
        }
    }
}
