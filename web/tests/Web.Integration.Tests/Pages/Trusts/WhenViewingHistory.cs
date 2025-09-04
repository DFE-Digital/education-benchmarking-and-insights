using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingHistory(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage()
    {
        var random = new Random();

        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, random.Next(10000000, 99999999).ToString())
            .Create();

        var page = await Client
            .SetupEstablishment(trust)
            .Navigate(Paths.TrustHistory(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        DocumentAssert.AssertPageUrl(page, Paths.TrustHistory(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.TrustHome(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Historic data - Financial Benchmarking and Insights Tool - GOV.UK", "Historic data");

        var historicDataPlaceholder = page.QuerySelector($"#historic-data");
        Assert.NotNull(historicDataPlaceholder);
        Assert.Equal(trust.CompanyNumber, historicDataPlaceholder.GetAttribute("data-id"));
        Assert.Equal("trust", historicDataPlaceholder.GetAttribute("data-type"));
    }
}