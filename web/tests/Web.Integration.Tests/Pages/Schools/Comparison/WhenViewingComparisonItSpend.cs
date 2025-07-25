using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
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

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .Create();

        const int comparators = 3;
        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>(comparators).ToArray())
            .With(c => c.Building, Fixture.CreateMany<string>(comparators).ToArray())
            .Create();
        var characteristics = Fixture.Build<SchoolCharacteristic>()
            .CreateMany(comparators)
            .ToArray();
        characteristics.First().URN = school.URN;

        var page = await Client
            .SetupEstablishment(school)
            .SetupSchoolInsight(characteristics)
            .SetupComparatorSet(school, comparatorSet)
            .Navigate(Paths.SchoolComparisonItSpend(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
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
    }
}