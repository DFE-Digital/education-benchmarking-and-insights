using System.Net;
using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenAUserWithoutValidClaims(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private const string Urn = "54321";
    private const string CompanyNumber = "12345";

    [Fact]
    public async Task SchoolFinancialPlanningRedirectsToForbidden()
    {
        var page = await Client
            .Navigate(Paths.SchoolFinancialPlanning(Urn));

        PageAssert.IsForbiddenPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanning(Urn).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task TrustFinancialPlanningRedirectsToForbidden()
    {
        var page = await Client
            .Navigate(Paths.TrustFinancialPlanning(CompanyNumber));

        PageAssert.IsForbiddenPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustFinancialPlanning(CompanyNumber).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CustomDataRedirectsToForbidden()
    {
        var page = await Client
            .Navigate(Paths.SchoolCustomDataFinancialData(Urn));

        PageAssert.IsForbiddenPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataFinancialData(Urn).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ForecastRiskRedirectsToForbidden()
    {
        var page = await Client
            .Navigate(Paths.TrustForecast(CompanyNumber));

        PageAssert.IsForbiddenPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(CompanyNumber).ToAbsolute(), HttpStatusCode.Forbidden);
    }
}
