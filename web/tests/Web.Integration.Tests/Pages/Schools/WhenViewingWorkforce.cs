﻿using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingWorkforce(BenchmarkingWebAppClient client) : PageBase(client)
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
    public async Task CanNavigateToCurriculumPlanning(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolFinancialPlanning(school.Urn).ToAbsolute());
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

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparison(school.Urn).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolWorkforce(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolWorkforce(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolWorkforce(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolWorkforce(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, finances)
            .SetupBenchmark(schools)
            .Navigate(Paths.SchoolWorkforce(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute()),
            ("Benchmark workforce data", Paths.SchoolWorkforce(school.Urn).ToAbsolute()),
        };

        DocumentAssert.AssertPageUrl(page, Paths.SchoolWorkforce(school.Urn).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Benchmark workforce data - Financial Benchmarking and Insights Tool - GOV.UK", "Benchmark workforce data");

        var workforceComponent = page.GetElementById("compare-your-workforce");
        Assert.NotNull(workforceComponent);

        var toolsSection = page.Body.SelectSingleNode("//main/div/div[5]");
        DocumentAssert.Heading2(toolsSection, "Finance tools");

        var toolsLinks = toolsSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(2, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "Curriculum and financial planning",
            Paths.SchoolFinancialPlanning(school.Urn).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Compare your costs", Paths.SchoolComparison(school.Urn).ToAbsolute());
    }
}