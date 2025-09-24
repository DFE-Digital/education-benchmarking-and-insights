using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparatorsCreateBy(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(null)]
    [InlineData("redirect-uri")]
    public async Task CanDisplay(string? redirectUri)
    {
        var (page, trust) = await SetupNavigateInitPage(redirectUri);
        AssertPageLayout(page, trust, redirectUri);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("redirect-uri")]
    public async Task CanNavigateToComparatorsByName(string? redirectUri)
    {
        var (page, trust) = await SetupNavigateInitPage(redirectUri);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "by", "name" }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreateByName(trust.CompanyNumber, redirectUri).ToAbsolute());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("redirect-uri")]
    public async Task CanNavigateToComparatorsByCharacteristic(string? redirectUri)
    {
        var (page, trust) = await SetupNavigateInitPage(redirectUri);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "by", "characteristic" }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreateByCharacteristic(trust.CompanyNumber, redirectUri).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, Trust Trust)> SetupNavigateInitPage(string? redirectUri = null)
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345678")
            .Create();

        var page = await Client.SetupEstablishment(trust)
            .Navigate(Paths.TrustComparatorsCreateBy(trust.CompanyNumber, redirectUri));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, string? redirectUri = null)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your trust", Paths.TrustHome(trust.CompanyNumber).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page,
            "How do you want to choose your own set of trusts? - Financial Benchmarking and Insights Tool - GOV.UK",
            "How do you want to choose your own set of trusts?");

        var cta = page.QuerySelector("main .govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.TrustComparatorsCreateBy(trust.CompanyNumber, redirectUri));
    }
}