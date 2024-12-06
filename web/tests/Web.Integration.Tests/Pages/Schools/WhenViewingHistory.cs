using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingHistory(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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
    public async Task CanDisplayFeatureFlagEnabled(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, true);

        AssertPageLayout(page, school, true);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool historicalTrendsFeatureEnabled = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .With(x => x.TrustCompanyNumber, financeType == EstablishmentTypes.Academies ? "12345678" : null)
            .Create();

        string[] disabledFlags = [];
        if (!historicalTrendsFeatureEnabled)
        {
            disabledFlags = [FeatureFlags.HistoricalTrends];
        }

        var page = await Client
            .SetupDisableFeatureFlags(disabledFlags)
            .SetupEstablishment(school)
            .Navigate(Paths.SchoolHistory(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, bool historicalTrendsFeatureEnabled = false)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHistory(school.URN).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.SchoolHome(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Historic data - Financial Benchmarking and Insights Tool - GOV.UK", "Historic data");

        var historicDataPlaceholder = page.QuerySelector($"#historic-data{(historicalTrendsFeatureEnabled ? "-2" : string.Empty)}");
        Assert.NotNull(historicDataPlaceholder);
        Assert.Equal(school.URN, historicDataPlaceholder.GetAttribute("data-id"));
        Assert.Equal("school", historicDataPlaceholder.GetAttribute("data-type"));

        if (historicalTrendsFeatureEnabled)
        {
            Assert.Equal(school.OverallPhase, historicDataPlaceholder.GetAttribute("data-phase"));
            Assert.Equal(school.FinanceType, historicDataPlaceholder.GetAttribute("data-finance-type"));
        }
    }
}