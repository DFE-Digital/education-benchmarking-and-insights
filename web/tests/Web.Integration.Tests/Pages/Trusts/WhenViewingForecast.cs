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
        var (page, trust) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
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

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage()
    {
        var trust = Fixture.Build<Trust>()
            .With(t => t.CompanyNumber, "54321")
            .Create();

        var page = await Client.SetupEstablishment(trust)
            .Navigate(Paths.TrustForecast(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
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
    }
}