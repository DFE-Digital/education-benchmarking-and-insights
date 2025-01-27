﻿using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingComparison(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
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
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolComparison(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolComparison(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Building, ["building"])
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights()
            .SetupExpenditure(school)
            .SetupUserData()
            .SetupComparatorSet(school, comparatorSet)
            .Navigate(Paths.SchoolComparison(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
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

        var comparisonComponent = page.GetElementById("compare-your-costs");
        Assert.NotNull(comparisonComponent);

        var toolsListSection = page.Body.SelectSingleNode("//main/div/div[5]");
        DocumentAssert.Heading2(toolsListSection, "Benchmarking and planning tools");

        var toolsLinks = toolsListSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(2, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "Curriculum and financial planning",
            Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Benchmark pupil and workforce data", Paths.SchoolCensus(school.URN).ToAbsolute());
    }
}