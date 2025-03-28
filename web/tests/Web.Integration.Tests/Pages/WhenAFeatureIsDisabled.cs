using System.Net;
using Web.App;
using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenAFeatureIsDisabled(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private const string Urn = "12345";
    private const string CompanyNumber = "54321";

    [Fact]
    public async Task SchoolFinancialPlanningRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.CurriculumFinancialPlanning)
            .Navigate(Paths.SchoolFinancialPlanning(Urn));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanning(Urn).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task TrustFinancialPlanningRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.CurriculumFinancialPlanning)
            .Navigate(Paths.TrustFinancialPlanning(CompanyNumber));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustFinancialPlanning(CompanyNumber).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CustomDataRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.CustomData)
            .Navigate(Paths.SchoolCustomData(Urn));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomData(Urn).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task TrustHomeRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.Trusts)
            .Navigate(Paths.TrustHome(CompanyNumber));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(CompanyNumber).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task LocalAuthorityHomeRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.LocalAuthorities)
            .Navigate(Paths.LocalAuthorityHome("123"));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome("123").ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task SchoolComparatorsRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.UserDefinedComparators)
            .Navigate(Paths.SchoolComparatorsCreate(Urn));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparatorsCreate(Urn).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ForecastRiskRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.ForecastRisk)
            .Navigate(Paths.TrustForecast(CompanyNumber));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(CompanyNumber).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task TrustComparisonRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.TrustComparison)
            .Navigate(Paths.TrustComparison(CompanyNumber));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparison(CompanyNumber).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task SchoolFinancialBenchmarkingInsightsSummaryRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.FinancialBenchmarkingInsightsSummary)
            .Navigate(Paths.SchoolFinancialBenchmarkingInsightsSummary(Urn));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialBenchmarkingInsightsSummary(Urn).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task LocalAuthorityHighNeedsRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.HighNeeds)
            .Navigate(Paths.LocalAuthorityHighNeedsDashboard("123"));

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsDashboard("123").ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task SchoolFacetedSearchRedirectsToFeatureDisabled()
    {
        var page = await Client.SetupDisableFeatureFlags(FeatureFlags.FacetedSearch)
            .Navigate(Paths.FindSchool);

        PageAssert.IsFeatureDisabledPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.FindSchool.ToAbsolute(), HttpStatusCode.Forbidden);
    }
}