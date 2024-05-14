using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingHome(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        AssertPageLayout(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCompareYourCosts(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.Urn).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCurriculumPlanning(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolFinancialPlanning(school.Urn).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCensusBenchmark(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[2].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCensus(school.Urn).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToChangeSchool()
    {
        var (page, _) = await SetupNavigateInitPage(EstablishmentTypes.Academies);

        var anchor = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");
        Assert.NotNull(anchor);

        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, $"{Paths.FindOrganisation.ToAbsolute()}?method=school");
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolHome(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolHome(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.Urn, "12345")
            .With(x => x.FinanceType, financeType)
            .With(x => x.OfstedRating, "0")
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools)
            .Navigate(Paths.SchoolHome(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.Urn).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(school.Name);
        DocumentAssert.TitleAndH1(page, "Your school - Financial Benchmarking and Insights Tool - GOV.UK", school.Name);
        if (school.IsPartOfTrust)
        {
            DocumentAssert.Heading2(page, $"Part of {school.TrustOrCompanyName}");
        }

        var changeLinkElement = page.QuerySelectorAll("a").FirstOrDefault(x => x.TextContent.Trim() == "Change school");
        DocumentAssert.Link(changeLinkElement, "Change school", $"{Paths.FindOrganisation.ToAbsolute()}?method=school");

        var toolsSection = page.Body.SelectSingleNode("//main/div/div[5]");
        DocumentAssert.Heading2(toolsSection, "Finance tools");

        var toolsLinks = toolsSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(3, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "Compare your costs", Paths.SchoolComparison(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Curriculum and financial planning", Paths.SchoolFinancialPlanning(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[2], "Benchmark census data", Paths.SchoolCensus(school.Urn).ToAbsolute());
    }
}