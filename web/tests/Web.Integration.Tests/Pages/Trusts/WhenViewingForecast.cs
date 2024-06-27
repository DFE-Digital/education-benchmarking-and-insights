using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingForecast(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust, metrics) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust, metrics);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyNumber = "54321";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustForecast(companyNumber));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(companyNumber).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayNotAuthorised()
    {
        const string companyNumber = "121212";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustForecast(companyNumber));

        PageAssert.IsAccessDeniedPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(companyNumber).ToAbsolute(), HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyNumber = "54321";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustForecast(companyNumber));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(companyNumber).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Trust trust, BudgetForecastReturnMetric[] metrics)> SetupNavigateInitPage()
    {
        var trust = Fixture.Build<Trust>()
            .With(t => t.CompanyNumber, "54321")
            .Create();

        var returns = Fixture.Build<BudgetForecastReturn>()
            .With(m => m.Year, 2022)
            .CreateMany(5)
            .ToArray();

        var metrics = Fixture.Build<BudgetForecastReturnMetric>()
            .With(m => m.Year, 2022)
            .CreateMany(5)
            .ToArray();

        var page = await Client.SetupEstablishment(trust)
            .SetupBudgetForecast(trust, returns, metrics)
            .Navigate(Paths.TrustForecast(trust.CompanyNumber));

        return (page, trust, metrics);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, BudgetForecastReturnMetric[] metrics)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your trust", Paths.TrustHome(trust.CompanyNumber).ToAbsolute()),
            ("Forecast and risks", Paths.TrustForecast(trust.CompanyNumber).ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Forecast and risks - Financial Benchmarking and Insights Tool - GOV.UK", "Forecast and risks");

        var metricsTable = page.QuerySelector("#bfr-metrics tbody");
        Assert.NotNull(metricsTable);
        var metricsRows = metricsTable.GetElementsByTagName("tr");
        Assert.Equal(metrics.Length, metricsRows.Length);
    }
}