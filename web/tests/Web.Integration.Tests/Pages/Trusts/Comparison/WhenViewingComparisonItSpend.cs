using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Comparison;

public class WhenViewingComparisonItSpend(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust, userDefinedSet) = await SetupNavigateInitPage(true);

        AssertPageLayout(page, trust, userDefinedSet);
    }

    [Fact]
    public async Task CanStartCreateComparatorsJourneyWhenComparatorsMissing()
    {
        var (page, trust, _) = await SetupNavigateInitPage(false);

        var redirectUri = WebUtility.UrlEncode(Paths.TrustComparisonItSpend(trust.CompanyNumber));
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreateBy(trust.CompanyNumber, redirectUri).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToTrustComparators()
    {
        var (page, trust, _) = await SetupNavigateInitPage(true);

        var anchor = page.QuerySelector("a[data-test-id='comparators-link']");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);
        var redirectUri = WebUtility.UrlEncode(Paths.TrustComparisonItSpend(trust.CompanyNumber));
        DocumentAssert.AssertPageUrl(newPage, Paths.TrustComparatorsUserDefined(trust.CompanyNumber, null, redirectUri).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyNumber = "12345678";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustComparison(companyNumber));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparison(companyNumber).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyNumber = "12345678";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustComparison(companyNumber));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparison(companyNumber).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Trust trust, UserDefinedSchoolComparatorSet userDefinedSet)> SetupNavigateInitPage(bool withComparatorSet)
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345678")
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

        var userDefinedSet = new UserDefinedSchoolComparatorSet
        {
            Set = ["00000001", "00000002", "00000003"]
        };

        var page = await Client
            .SetupEstablishment(trust)
            .SetupInsights()
            .SetupUserData(withComparatorSet ? comparatorSet : null)
            .SetupComparatorSet(trust, userDefinedSet)
            .Navigate(Paths.TrustComparisonItSpend(trust.CompanyNumber));

        return (page, trust, userDefinedSet);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, UserDefinedSchoolComparatorSet userDefinedSet)
    {
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparisonItSpend(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Benchmark your IT spending - Financial Benchmarking and Insights Tool - GOV.UK", "Benchmark your IT spending");

        var comparatorsLink = page.QuerySelector("a[data-test-id='comparators-link']");
        Assert.NotNull(comparatorsLink);
        DocumentAssert.TextEqual(comparatorsLink, $"You have chosen {userDefinedSet.Set.Length - 1} similar trusts");
    }
}