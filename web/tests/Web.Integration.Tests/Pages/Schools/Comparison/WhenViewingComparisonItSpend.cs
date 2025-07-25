using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Comparison;

public class WhenViewingComparisonItSpend(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplayForMaintainedSchool()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanDisplayChartWarningForMaintainedSchoolWhenChartApiFails()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, true);

        AssertPageLayout(page, school, true);
    }

    [Fact]
    public async Task CanDisplayNotFoundForAcademy()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparisonItSpend(school.URN).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanNavigateToSchoolComparators()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

        var anchor = page.QuerySelector("a[data-test-id='comparators-link']");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolComparators(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolComparisonItSpend(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparisonItSpend(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolComparisonItSpend(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparisonItSpend(urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool chartApiException = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        var spend = Fixture.Build<SchoolItSpend>()
            .CreateMany()
            .ToArray();
        spend.ElementAt(0).URN = school.URN;

        var horizontalBarChart = new ChartResponse { Html = "<svg />" };

        var client = Client
            .SetupEstablishment(school)
            .SetupComparatorSet(school, comparatorSet)
            .SetupItSpend(spend)
            .SetupChartRendering<SchoolComparisonDatum>(horizontalBarChart);

        if (chartApiException)
        {
            Client.SetupChartRenderingWithException<SchoolComparisonDatum>();
        }

        var page = await client.Navigate(Paths.SchoolComparisonItSpend(school.URN));
        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, bool chartError = false)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparisonItSpend(school.URN).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Benchmark your IT spending - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark your IT spending");

        var subCategorySections = page.QuerySelectorAll("section");
        Assert.Equal(7, subCategorySections.Length);

        for (var i = 0; i < subCategorySections.Length; i++)
        {
            var section = subCategorySections.ElementAt(i);
            var sectionHeading = section.QuerySelector("h2")?.TextContent;
            Assert.NotNull(sectionHeading);

            switch (i)
            {
                case 0:
                    Assert.Equal("Administration software and systems E20D", sectionHeading);
                    break;
                case 1:
                    Assert.Equal("Connectivity E20A", sectionHeading);
                    break;
                case 2:
                    Assert.Equal("IT learning resources E20C", sectionHeading);
                    break;
                case 3:
                    Assert.Equal("IT support E20G", sectionHeading);
                    break;
                case 4:
                    Assert.Equal("Laptops, desktops and tablets E20E ", sectionHeading);
                    break;
                case 5:
                    Assert.Equal("Onsite servers E20B", sectionHeading);
                    break;
                case 6:
                    Assert.Equal("Other hardware E20F", sectionHeading);
                    break;
            }

            var chartSvg = section.QuerySelector(".ssr-chart");
            var chartWarning = section.QuerySelector(".ssr-chart-warning");
            var chartContainer = section.QuerySelector(".composed-container");

            if (chartError)
            {
                Assert.NotNull(chartWarning);
                Assert.Null(chartSvg);
            }
            else
            {
                Assert.NotNull(chartSvg);
                Assert.Null(chartWarning);
            }

            Assert.Null(chartContainer);
        }
    }
}