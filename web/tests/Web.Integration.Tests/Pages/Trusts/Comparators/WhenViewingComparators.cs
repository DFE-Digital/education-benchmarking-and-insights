using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparators(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust) = await SetupNavigateInitPage(true);
        AssertPageLayout(page, trust);
    }

    [Fact]
    public async Task DisplaysCreateByIfNoSetDefined()
    {
        var (page, trust) = await SetupNavigateInitPage(false);
        DocumentAssert.TitleAndH1(page,
            "How do you want to choose your own set of trusts? - Financial Benchmarking and Insights Tool - GOV.UK",
            "How do you want to choose your own set of trusts?");
    }

    private async Task<(IHtmlDocument page, Trust Trust)> SetupNavigateInitPage(bool withComparatorSet)
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345")
            .Create();

        var comparatorSet = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = "123"
            },
            new UserData
            {
                Type = "comparator-set",
                Id = "456"
            }
        };

        var page = await Client.SetupEstablishment(trust)
            .SetupInsights()
            .SetupUserData(withComparatorSet ? comparatorSet : null)
            .Navigate(Paths.TrustComparators(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your trust", Paths.TrustHome(trust.CompanyNumber).ToAbsolute()),
            ("Comparator sets", Paths.TrustComparators(trust.CompanyNumber).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page,
            "Benchmark spending for this trust - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark spending for this trust");
    }
}